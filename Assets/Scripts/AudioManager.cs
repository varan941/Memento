using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource blackWorldSource, whiteWorldSource;
    public AudioClip blackWorldClip, whiteWorldClip, transitionClip;

#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("fade sound");
            StartCoroutine(StartFade(blackWorldSource, whiteWorldSource, 1.5f));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("fade sound");
            StartCoroutine(StartFade(whiteWorldSource, blackWorldSource, 1.5f));
        }
    }
#endif

    public void ChangeMusic(bool isBlackWorld)
    {
        StopAllCoroutines();
        if (isBlackWorld)
        {
            whiteWorldSource.volume = 0;
            StartCoroutine(StartFade(blackWorldSource, whiteWorldSource, 1.5f));
        }
        else
        {
            blackWorldSource.volume = 0;
            StartCoroutine(StartFade(whiteWorldSource, blackWorldSource, 1.5f));
        }
    }

    public static IEnumerator StartFade(AudioSource soundDownSource, AudioSource soundUpSource, float duration)
    {
        float currentTime = 0;
        float start = soundDownSource.volume;

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            soundDownSource.volume = Mathf.Lerp(1, 0, currentTime / duration);
            yield return null;
        }

        currentTime = 0;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            soundUpSource.volume = Mathf.Lerp(0, 1, currentTime / duration);
            yield return null;
        }
        yield break;
    }

}
