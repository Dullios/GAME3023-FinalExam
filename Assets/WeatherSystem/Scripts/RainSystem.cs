using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSystem : WeatherSystem
{
    [Header("Rain Details")]
    public int rainSpeed;

    [Header("Storm Details")]
    public int stormSpeed;
    [Range(1, 100)]
    public int stormEmissionChange;

    // Start is called before the first frame update
    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }

    public override void TransitionFromSystem()
    {
        isTransitioning = true;
        var em = particles.emission;
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
        isTransitioning = true;

        var m = particles.main;
        m.startSpeed = rainSpeed;

        var em = particles.emission;
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

        var m = particles.main;
        m.startSpeed = rainSpeed;

        var em = particles.emission;
        StartCoroutine(FromStormRoutine(em));
    }
    IEnumerator FromStormRoutine(ParticleSystem.EmissionModule em)
    {
        float rate = em.rateOverTime.constant;

        while (em.rateOverTime.constant > minEmissions)
        {
            rate -= stormEmissionChange;
            em.rateOverTime = rate;
            yield return new WaitForSeconds(changeDelay);
        }

        isTransitioning = false;
    }


    public void TransitionToStorm()
    {
        isTransitioning = true;

        var m = particles.main;
        m.startSpeed = stormSpeed;

        var em = particles.emission;
        StartCoroutine(ToStormRoutine(em));
    }
    IEnumerator ToStormRoutine(ParticleSystem.EmissionModule em)
    {
        float rate = em.rateOverTime.constant;

        while (em.rateOverTime.constant < maxEmissions)
        {
            rate += stormEmissionChange;
            em.rateOverTime = rate;
            yield return new WaitForSeconds(changeDelay);
        }

        isTransitioning = false;
    }
}
