using TMPro;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public Animator animator;
    public TextMeshProUGUI textDisplay;

    public void SetTrigger(int num)
    {
        textDisplay.text = $"-Ты потратил всё время...хехе \n" +
            $"осталось циклов {num}";
        animator.SetTrigger("FadeTrig");
    }

    public void SetBlackWorld()
    {       
        GameManager.I.SetBlackWorld();
    }

    public void SetActiveFalseObj()
    {       
        gameObject.SetActive(false);
    }

}