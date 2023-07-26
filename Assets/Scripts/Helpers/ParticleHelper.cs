using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHelper : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;

    public void Play()
    {
        if (particle != null) particle.Play();
    }

    public void Stop()
    {
        if (particle != null) particle.Stop();
    }
}
