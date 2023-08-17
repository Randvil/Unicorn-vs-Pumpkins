using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinDeathView
{
    private IDeathManager deathManager;
    private GameObject model;
    private AudioSource audioSource;
    private ParticleSystem headExplosion;
    private ParticleSystem droplets;

    private PumpkinData pumpkinData;
    private AudioClip deathAudioClip;

    public PumpkinDeathView(PumpkinData pumpkinData, IDeathManager deathManager, GameObject model, AudioSource audioSource, AudioClip deathAudioClip, ParticleSystem headExplosion, ParticleSystem droplets)
    {
        this.pumpkinData = pumpkinData;

        this.deathManager = deathManager;
        this.model = model;
        this.audioSource = audioSource;
        this.headExplosion = headExplosion;
        this.droplets = droplets;

        this.deathAudioClip = deathAudioClip;

        deathManager.DeathEvent.AddListener(OnDeath);
    }

    private void OnDeath(MonoBehaviour pumpkin)
    {
        deathManager.DeathEvent.RemoveListener(OnDeath);

        model.SetActive(false);

        ParticleSystem.MainModule explosionSettings = headExplosion.main;
        ParticleSystem.MainModule droplestSettings = droplets.main;
        explosionSettings.startColor = pumpkinData.explosionColor;
        droplestSettings.startColor = pumpkinData.dropletsColor;
        headExplosion.Play();

        if (deathAudioClip != null)
        {
            audioSource.PlayOneShot(deathAudioClip);
        }
    }
}
