using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherSystem : MonoBehaviour
{
    protected ParticleSystem particles;

    [Header("Particle Details")]
    public int minEmissions;
    public int maxEmissions;
    [Range(1, 100)]
    public int emissionChange;
    [Range(0.01f, 0.5f)]
    public float changeDelay;

    [Header("State")]
    public bool isTransitioning;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public virtual void TransitionFromSystem()
    {

    }

    public virtual void TransitionToSystem()
    {

    }
}
