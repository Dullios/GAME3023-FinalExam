using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : WeatherSystem
{
    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public override void TransitionFromSystem()
    {
        var em = particles.emission;
        isTransitioning = true;
        StartCoroutine(FromSystemRoutine(em));
    }
    IEnumerator FromSystemRoutine(ParticleSystem.EmissionModule em)
    {
        float rate = em.rateOverTime.constant;

        while (em.rateOverTime.constant > 0)
        {
            rate -= emissionChange;
            em.rateOverTime = rate;
            yield return new WaitForSeconds(changeDelay);
        }

        em.enabled = false;
        isTransitioning = false;
    }


    public override void TransitionToSystem()
    {
        var em = particles.emission;
        isTransitioning = true;
        em.enabled = true;
        em.rateOverTime = 0;
        StartCoroutine(ToSystemRoutine(em));
    }
    IEnumerator ToSystemRoutine(ParticleSystem.EmissionModule em)
    {
        float rate = em.rateOverTime.constant;

        while (em.rateOverTime.constant < minEmissions)
        {
            rate += emissionChange;
            em.rateOverTime = rate;
            yield return new WaitForSeconds(changeDelay);
        }

        isTransitioning = false;
    }


    public void TransitionFromStorm()
    {
        isTransitioning = true;
    }


    public void TransitionToStorm()
    {
        var em = particles.emission;
        isTransitioning = true;
    }
    IEnumerator ToStormRoutine(ParticleSystem.EmissionModule em)
    {
        float rate = em.rateOverTime.constant;

        while (em.rateOverTime.constant < maxEmissions)
        {
            rate += emissionChange;
            em.rateOverTime = rate;
            yield return new WaitForSeconds(changeDelay);
        }

        isTransitioning = false;
    }
}
