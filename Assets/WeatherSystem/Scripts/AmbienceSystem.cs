using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbienceSystem : MonoBehaviour
{
    public AudioSource forestSound;
    public AudioSource rainSound;
    public AudioSource stormSound;
    public AudioSource thunderSound;

    public AudioClip[] thunderSFX;

    [Header("Volume Details")]
    public float maxVolume;
    public float increment;
    public float delay;

    public void FadeIn(WeatherType type)
    {
        StartCoroutine(FadeInRoutine(type));
    }
    IEnumerator FadeInRoutine(WeatherType type)
    {
        switch(type)
        {
            case WeatherType.SUNNY:
                forestSound.Play();
                while(forestSound.volume < maxVolume)
                {
                    forestSound.volume += increment;
                    yield return new WaitForSeconds(delay);
                }
                break;
            case WeatherType.RAINING:
                rainSound.Play();
                while (rainSound.volume < maxVolume)
                {
                    rainSound.volume += increment;
                    yield return new WaitForSeconds(delay);
                }
                break;
            case WeatherType.THUNDERSTORM:
                stormSound.Play();
                while (stormSound.volume < maxVolume)
                {
                    stormSound.volume += increment;
                    yield return new WaitForSeconds(delay);
                }
                break;
        }
    }

    public void FadeOut(WeatherType type)
    {
        StartCoroutine(FadeOutRoutine(type));
    }
    IEnumerator FadeOutRoutine(WeatherType type)
    {
        switch (type)
        {
            case WeatherType.SUNNY:
                while (forestSound.volume > 0)
                {
                    forestSound.volume -= increment;
                    yield return new WaitForSeconds(delay);
                }
                forestSound.Stop();
                break;
            case WeatherType.RAINING:
                while (rainSound.volume > 0)
                {
                    rainSound.volume -= increment;
                    yield return new WaitForSeconds(delay);
                }
                rainSound.Stop();
                break;
            case WeatherType.THUNDERSTORM:
                while (stormSound.volume > 0)
                {
                    stormSound.volume -= increment;
                    yield return new WaitForSeconds(delay);
                }
                stormSound.Stop();
                break;
        }
    }

    public void ThunderSound()
    {
        thunderSound.clip = thunderSFX[Random.Range(0, thunderSFX.Length)];
        thunderSound.Play();
    }
}
