using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesSkysung : MonoBehaviour
{
    public TrailRenderer[] trial;
    public ParticleSystem particles;

    public void activate()
    {
        StartCoroutine("activateParticles");
    }

    public IEnumerator activateParticles()
    {
        particles.Play();
        for (int i = 0; i < trial.Length; i++)
            trial[i].enabled = true;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < trial.Length; i++)
            trial[i].enabled = false;
    }

}
