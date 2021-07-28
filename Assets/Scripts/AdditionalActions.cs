using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AdditionalActions : MonoBehaviour
{
    public InteractebleObject food;
    public string foodStr;
    public UnityEvent foodEvent;
    [Header("")]

    public InteractebleObject piano;
    public string pianoStr;
    public UnityEvent pianoEvent;
    [Header("")]    

    public InteractebleObject pianoBlack;
    public string pianoBlackStr;
    public UnityEvent pianoBlackEvent;

    [Header("")]
    public InteractebleObject bottle;
    public string bottleStr;
    public UnityEvent bottleEvent;

    [Header("")]
    public InteractebleObject bar;
    public string barStr;
    public UnityEvent barEvent;



    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("StepFour");
            StepFour();
        }
#endif
    }

    public void StepOne()
    {
        DialogManager.I.PlayHint();
        Debug.Log("добавилась кнопка");
        food.AddBlackWorldAction(foodStr, foodEvent);
    }


    public void StepTwo()
    {
        DialogManager.I.PlayHint();
        Debug.Log("добавилась ещё кнопка");
        piano.AddWhitekWorldAction(pianoStr, pianoEvent);
    }

    public void StepThree()
    {
        DialogManager.I.PlayHint();
        pianoBlack.isZooming = true;
        pianoBlack.AddBlackWorldAction(pianoBlackStr, pianoBlackEvent);
    }

    public void StepFour()
    {
        DialogManager.I.PlayHint();
        bottle.AddBlackWorldAction(bottleStr, bottleEvent);
        bottle.AddWhitekWorldAction(bottleStr, bottleEvent);
    }

    public void StepFive()
    {
        DialogManager.I.PlayHint();
        bar.AddBlackWorldAction(barStr, barEvent);
    }

    public void StepSix()
    {
        //DialogManager.I.PlayHint();
        GameManager.I.SetEndGameScreen(true);
    }

}
