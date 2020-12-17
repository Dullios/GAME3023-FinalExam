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
    [Range(0, 0.5f)]
    public float changeSpeed;

    [Header("Weather Controls")]
    public WeatherType wType;
    public float changeDelay;
    public float timer;
    [Range(0, 100)]
    public float changeChance;
    public bool isTransitioning;
    
    [Header("Weather Objects")]
    public GameObject rainSystem;
    public GameObject stormSystem;

    // Start is called before the first frame update
    void Start()
    {
        wType = WeatherType.SUNNY;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTransitioning)
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
                            TransitionToOvercast();
                        }
                        break;
                    case WeatherType.OVERCAST:
                        // Transition to sunny
                        if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            TransitionToSunny();
                        }
                        // Transition to raining
                        //else if (Random.Range(0.0f, 100.0f) <= changeChance)
                        //{

                        //}
                        break;
                    case WeatherType.RAINING:
                        // Transition to overcast
                        if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            // TransitionFrom
                            TransitionToOvercast();
                        }
                        // Transition to raining
                        else if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            // TransitionFrom
                        }
                        break;
                    case WeatherType.THUNDERSTORM:
                        // Transition to raining
                        if (Random.Range(0.0f, 100.0f) <= changeChance)
                        {
                            // TransitionFrom
                            // TransitionTO
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
        wType = WeatherType.SUNNY;
        StartCoroutine(ToSunnyRoutine());
    }

    IEnumerator ToSunnyRoutine()
    {
        while (globalLight.intensity < 1.0f)
        {
            globalLight.intensity += intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }

        isTransitioning = false;
    }

    private void TransitionToOvercast()
    {
        isTransitioning = true;
        wType = WeatherType.OVERCAST;
        StartCoroutine(ToOvercastRoutine());
    }

    IEnumerator ToOvercastRoutine()
    {
        while(globalLight.intensity > 0.66f)
        {
            globalLight.intensity -= intensityChange;
            yield return new WaitForSeconds(changeSpeed);
        }

        isTransitioning = false;
    }
}
