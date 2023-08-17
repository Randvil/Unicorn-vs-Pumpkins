using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnicornDeathView
{
    private IDeathManager deathManager;
    private SkeletonAnimation skeletonAnimation;
    private AudioSource audioSource;

    private string deathAnimationName;
    private float deathAnimationSpeed;
    private AudioClip deathAudioClip;

    public UnicornDeathView(IDeathManager deathManager, SkeletonAnimation skeletonAnimation, AudioSource audioSource, string deathAnimationName, float deathAnimationSpeed, AudioClip deathAudioClip)
    {
        this.deathManager = deathManager;
        this.skeletonAnimation = skeletonAnimation;
        this.audioSource = audioSource;

        this.deathAnimationName = deathAnimationName;
        this.deathAnimationSpeed = deathAnimationSpeed;
        this.deathAudioClip = deathAudioClip;

        deathManager.DeathEvent.AddListener(OnDeath);
    }

    private void OnDeath(MonoBehaviour unicorn)
    {
        deathManager.DeathEvent.RemoveListener(OnDeath);

        skeletonAnimation.loop = false;
        skeletonAnimation.timeScale = deathAnimationSpeed;
        skeletonAnimation.AnimationName = deathAnimationName;

        if (deathAudioClip != null)
        {
            audioSource.PlayOneShot(deathAudioClip);
        }
    }
}
