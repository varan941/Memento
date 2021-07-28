using Lean.Gui;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsManager : MonoBehaviour
{
    public LeanButton startBtn;
    public LeanButton exitBtn;
    public LeanButton authorsBtn;
    public LeanWindow escWind;
    public LeanWindow authorsWind;

    public AudioSource source;


    private void Start()
    {
        startBtn?.OnClick.AddListener(() => StartGame());
        exitBtn?.OnClick.AddListener(() => ExitGame());
        authorsBtn?.OnClick.AddListener(() => OpenAuthorsWind());
    }

    public void StartGame()
    {
        if (source!=null)        
            StartCoroutine(StartFade(source,1.5f));        
        else
          SceneManager.LoadScene("Game");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void EscMenu()
    {
        escWind?.TurnOn();
    }

    public void OpenAuthorsWind()
    {
        authorsWind?.TurnOn();
    }


    public static IEnumerator StartFade(AudioSource soundDownSource, float duration)
    {
        float currentTime = 0;
        float start = soundDownSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            soundDownSource.volume = Mathf.Lerp(1, 0, currentTime / duration);
            yield return null;
        }

        SceneManager.LoadScene("Game");
        yield break;
    }



}
