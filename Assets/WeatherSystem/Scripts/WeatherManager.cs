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

    [Header("Weather Controls")]
    public WeatherType wType;
    public float changeTime;
    public float timer;

    [Header("Weather Objects")]
    public GameObject rainSystem;
    public GameObject stormSystem;

    // Start is called before the first frame update
    void Start()
    {
        wType = WeatherType.SUNNY;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
