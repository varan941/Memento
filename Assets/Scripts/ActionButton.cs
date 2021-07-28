using UnityEngine;
using TMPro;
using Lean.Gui;
using UnityEngine.Events;

public class ActionButton : MonoBehaviour
{
    public TextMeshProUGUI textBtn;
    private LeanButton _btn;

 
    public void SetaValues(string text, UnityEvent unityEvent)
    {
        _btn = GetComponent<LeanButton>();
        textBtn.text = text;
        _btn.OnClick.AddListener(()=>
        {
            if (GameManager.I.CanMove())
            {
                unityEvent.Invoke();
                GameManager.I.Move();
            }
            
        });
    }
}
