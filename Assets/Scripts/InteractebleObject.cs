using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractebleObject : MonoBehaviour
{    
    private Button _objButton;
    public static bool isDarkWorld=true;
    

    [Header("Для темного мира:")]
    public Sprite needImage;
    [SerializeField] string dialogText;
    public bool isZooming = false;

    [SerializeField] List<string> textOptions;
    [SerializeField] List<UnityEvent> events;

    [Header("Для светлого мира:")]
    public Sprite needImage2;
    [SerializeField] string dialogText2;
    [SerializeField] bool isZooming2 = false;

    [SerializeField] List<string> textOptions2;
    [SerializeField] List<UnityEvent> events2;

    

    private void Start()
    {
        _objButton = gameObject.GetComponent<Button>();
        _objButton.onClick.AddListener(() => SetActions());        
    }

    public void SetActions()
    {
        Init();
    }

    private void Init()
    {
        if (GameManager.I.CanMove())
        {
            TryZoomIn();
            GameManager.I.Move();
        }
        else
            GameManager.I.SetImageBack();
    }

    public void TryZoomIn()
    {
        if ((isZooming&&isDarkWorld)||(isZooming2&&!isDarkWorld))
        {
            Zoom();
            ShowDialog();
            if (isDarkWorld)            
                GameManager.I.CreateButtons(textOptions, events);
            else if (textOptions2.Count>0&& events2.Count>0)
                GameManager.I.CreateButtons(textOptions2, events2);
            else
                GameManager.I.CreateButtons(textOptions, events);
        }
        else
        {
            ShowDialog();
        }
    }

    public void Zoom()
    {
        if(isDarkWorld)
            GameManager.I.SetZoomImageTo(needImage);
        else
            GameManager.I.SetZoomImageTo(needImage2);
    }

    public void ShowDialog()
    {
        if (dialogText != null&&isDarkWorld)        
            DialogManager.I.PlayNeedDialog(dialogText);
        else if(dialogText2!=null && dialogText2!="")
            DialogManager.I.PlayNeedDialog(dialogText2);
        else
            DialogManager.I.PlayNeedDialog(dialogText);
    }    

    public void AddBlackWorldAction(string btnText, UnityEvent newEvent)
    {
        if (textOptions.Contains(btnText))
            return;

        textOptions.Add(btnText);
        events.Add(newEvent);
    }

    public void AddWhitekWorldAction(string btnText, UnityEvent newEvent)
    {
        if (textOptions2.Contains(btnText))
            return;

        textOptions2.Add(btnText);
        events2.Add(newEvent);
    }
}
