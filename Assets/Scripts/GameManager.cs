using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    public AudioManager audioManager;

    [Header("спрайты:")]
    public Sprite blackWorldSprt, whiteWorldSprt;
    public Image bgImg;
    public Image zoomImg;

    [Header("для UI:")]    
    public LeanButton backToBGBtn;
    public LeanButton switchWorldBtn;
    public LeanWindow zoomWind;
    public LeanWindow loseGameWind;
    public LeanWindow winGameWind;
    public ActionButton prefabActBtn;
    public Transform parentForActBtns;

    public List<Button> objectsBtn;

    [Header("логика игры:")]
    public float moves=2; // на втором уровне 3 хода
    private float _tempMoves;
    public int attempts;
    private int _limitAttempts=15;

    [Header("другое:")]
    public DeathScreen deathScreen;


    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        backToBGBtn.OnClick.AddListener(() =>
        {
            SetImageBack();
            backToBGBtn.gameObject.SetActive(false);
        });
        switchWorldBtn.OnClick.AddListener(() => SwitchWorld());
        _tempMoves = moves;
    }

    public void SetImageBack()
    {
        //zoomImg.gameObject.SetActive(false);
        zoomWind.TurnOff();
        switchWorldBtn.gameObject.SetActive(true);

        foreach (RectTransform child in parentForActBtns)
        {
            Destroy(child.gameObject);
        }

        foreach (var item in objectsBtn)
        {
            item.interactable = true;
        }

    }

    public void SetZoomImageTo(Sprite sprite)
    {
        backToBGBtn.gameObject.SetActive(true);
        switchWorldBtn.gameObject.SetActive(false);
        zoomImg.sprite = sprite;
        zoomWind.TurnOn();
        foreach (var item in objectsBtn)
        {
            item.interactable = false;
        }
    }

    public void CreateButtons(List<string> textOptions, List<UnityEvent> events)
    {
        for (int i = 0; i < textOptions.Count; i++)
        {
            var btn = Instantiate(prefabActBtn, parentForActBtns);
            btn.SetaValues(textOptions[i], events[i]);
        }
    }

    public void SwitchWorld()
    {
        if (bgImg.sprite== blackWorldSprt)
        {
            bgImg.sprite = whiteWorldSprt;
            InteractebleObject.isDarkWorld = false;
            StartTimer();
            audioManager.ChangeMusic(true);
        }
        else
        {
            bgImg.sprite = blackWorldSprt;
            InteractebleObject.isDarkWorld = true;            
            StopTimer();
            audioManager.ChangeMusic(false);
        }
    }

    public void SetBlackWorld()
    {
        backToBGBtn.gameObject.SetActive(false);
        InteractebleObject.isDarkWorld = true;
        bgImg.sprite = blackWorldSprt;
        audioManager.ChangeMusic(false); // set blackWorldMusic
    }

    public bool CanMove()
    {
        if (moves <= 0)
        {
            EndMove();
            return false;
        }

        return !(moves <= 0);
    }

    private void EndMove()
    {        
        moves = _tempMoves;
        SetImageBack();
        attempts++;
        if (attempts> _limitAttempts)
        {
            SetEndGameScreen(false);
            return ;
        }
        deathScreen.gameObject.SetActive(true);
        deathScreen.SetTrigger(_limitAttempts-attempts);
        

    }

    public void Move()
    {       
        moves -= 1f;
    }

    public void StartTimer()
    {
        StartCoroutine(DecrMovesCrt());
    }

    public void StopTimer()
    {
        StopAllCoroutines();
    }

    public void SetEndGameScreen(bool value)
    {
        if (value==false)
        {
            loseGameWind.TurnOn();
            //выйти в меню кнопка
        }
        else
        {
            winGameWind.TurnOn();
        }
    }


    public IEnumerator DecrMovesCrt()
    {
        while (moves>0)
        {
            //Debug.Log(moves);
            moves -= 0.1f;
            yield return new WaitForSeconds(1f);
        }
        EndMove();
    }
}
