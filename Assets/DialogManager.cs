using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public static DialogManager I;
    

    [SerializeField] TextMeshProUGUI textDisplay, hintText;    
    [SerializeField] float typingSpeed = 0.02f;
    [SerializeField] Button dialogBtn;
    private string _currSentence;
    private bool _isTyping;

    private void Awake()
    {
        I = this;
    }

    private void Start()
    {
        dialogBtn.onClick.AddListener(() =>
        {
            if (_isTyping)
            {
                SetAllText();
            }
            else
            {
                textDisplay.text = "";
            }
        });
    }

    public void PlayNeedDialog(string sentence)
    {       
        if (_currSentence==sentence&&_isTyping==true)
        {
            SetAllText();
        }
        else
        {
            //StopAllCoroutines();
            StopCoroutine("Type");
            textDisplay.text = "";
            StartCoroutine(Type(sentence));
        }
       
    }

    public void PlayHint()
    {
        StartCoroutine(TypeHint("Вы что-то вспомнили..."));
    }

    private void SetAllText()
    {
        StopAllCoroutines();
        textDisplay.text = _currSentence;
        _isTyping = false;
    }

    IEnumerator Type(string sentence)
    {
        _currSentence = sentence;
        _isTyping = true;
        foreach (var letter in sentence.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        _isTyping = false;
        yield return new WaitForSeconds(3f);
        textDisplay.text = "";
    }

    IEnumerator TypeHint(string sentence)
    {
        hintText.text = "";
        foreach (var letter in sentence.ToCharArray())
        {
            hintText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }        
        yield return new WaitForSeconds(3f);
        hintText.text = "";
    }
}
