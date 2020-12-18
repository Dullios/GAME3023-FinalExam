using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[System.Serializable]
public enum WeatherType
{
    SUNNY,
    OVERCAST,
    RAINING,
    THUNDERSTORM
}

public class WeatherManager : MonoBehaviour
{
    [Header("Light Sources")]
    public Light2D globalLight;
    public Light2D playerLight;

    [Header("Light Settings")]
    [Range(0.01f, 0.1f)]
    public float intensityChange;
    [Range(0.01f, 0.5f)]
    public float changeSpeed;

    [Header("Weather Controls")]
    public WeatherType wType;
    [Range(0, 100)]
    public float changeChance;
    public float changeDelay;
    public float timer;
    public bool isTransitioning;
    
    [Header("Weather Objects")]
    public GameObject rainSystem;
    private RainSystem rSystem;

    [Header("Sound")]
    public AmbienceSystem ambience;

    // Start is called before the first frame update
    void Start()
    {
        rSystem = rainSystem.GetComponent<RainSystem>();

        wType = WeatherType.SUNNY;
        timer = 0.0f;
        ambience.FadeIn(WeatherType.SUNNY);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning && !rSystem.isTransitioning)
        {
            timer += Time.deltaTime;
            if (timer >= changeDelay)
            {
                switch (wType)
                {
                    case WeatherType.SUNNY:
                        // Transition to overcast
                        if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.OVERCAST;
                            TransitionToOvercast();

                            ambience.FadeOut(WeatherType.SUNNY);
                        }
                        break;
                    case WeatherType.OVERCAST:
                        // Transition to sunny
                        /*if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.SUNNY;
                            TransitionToSunny();

                            ambience.FadeIn(WeatherType.SUNNY);
                        }
                        // Transition to raining
                        else*/ if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.RAINING;
                            rSystem.TransitionToSystem();

                            ambience.FadeIn(WeatherType.RAINING);
                        }
                        break;
                    case WeatherType.RAINING:
                        // Transition to overcast
                        /*if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.OVERCAST;
                            rSystem.TransitionFromSystem();

                            ambience.FadeOut(WeatherType.RAINING);
                        }
                        // Transition to storming
                        else*/ if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.THUNDERSTORM;
                            rSystem.TransitionToStorm();
                            StartCoroutine(ToStormRoutine());

                            ambience.FadeOut(WeatherType.RAINING);
                            ambience.FadeIn(WeatherType.THUNDERSTORM);
                        }
                        break;
                    case WeatherType.THUNDERSTORM:
                        // Transition to raining
                        if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            wType = WeatherType.RAINING;
                            rSystem.TransitionFromStorm();
                            StartCoroutine(FromStormRoutine());

                            ambience.FadeOut(WeatherType.THUNDERSTORM);
                            ambience.FadeIn(WeatherType.RAINING);
                        }
                        break;
                }

                timer = 0;
            }
        }
    }

    private void TransitionToSunny()
    {
        isTransitioning = true;
        StartCoroutine(ToSunnyRoutine());
    }

    IEnumerator ToSunnyRoutine()
    {
        while (globalLight.intensity < 1.0f)
        {
            globalLight.intensity += intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }

        yield return new WaitForSeconds(0.2f);
        playerLight.enabled = false;

        isTransitioning = false;
    }

    private void TransitionToOvercast()
    {
        isTransitioning = true;
        StartCoroutine(ToOvercastRoutine());
    }

    IEnumerator ToOvercastRoutine()
    {
        while(globalLight.intensity > 0.66f)
        {
            globalLight.intensity -= intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }

        yield return new WaitForSeconds(0.2f);
        playerLight.enabled = true;

        isTransitioning = false;
    }

    IEnumerator FromStormRoutine()
    {
        while (globalLight.intensity < 0.66f)
        {
            globalLight.intensity += intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }
    }

    IEnumerator ToStormRoutine()
    {
        while (globalLight.intensity > 0.33f)
        {
            globalLight.intensity -= intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }
    }

    IEnumerator LightFlash()
    {
        yield return new WaitForSeconds(2);
        globalLight.intensity = 1.5f;
        yield return new WaitForSeconds(0.2f);
        globalLight.intensity = 0.33f;
    }
}
