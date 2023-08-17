using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPGunView
{
    protected EMPGun eMPGun;
    protected SkeletonAnimation skeletonAnimation;
    protected AudioSource audioSource;

    protected string animationName;
    protected float animationSpeed;
    protected float soundSpeedMultiplier;
    protected ParticleSystem gunShotVFX;

    public EMPGunView(EMPGun eMPGun, SkeletonAnimation skeletonAnimation, AudioSource audioSource, string animationName, float animationSpeed, float soundSpeedMultiplier, ParticleSystem gunShotVFX)
    {
        this.eMPGun = eMPGun;
        this.skeletonAnimation = skeletonAnimation;
        this.audioSource = audioSource;

        this.animationName = animationName;
        this.animationSpeed = animationSpeed;
        this.soundSpeedMultiplier = soundSpeedMultiplier;
        this.gunShotVFX = gunShotVFX;

        eMPGun.StartAttackEvent.AddListener(OnStartAttack);
        eMPGun.ReleaseAttackEvent.AddListener(OnReleaseAttack);
        eMPGun.StopAttackEvent.AddListener(OnStopAttack);
    }

    protected virtual void OnStartAttack(Vector2 target)
    {
        skeletonAnimation.timeScale = 1f / eMPGun.PreAttackDelay * animationSpeed;
        skeletonAnimation.AnimationName = animationName;

        audioSource.pitch = 1f / eMPGun.PreAttackDelay * soundSpeedMultiplier;
        audioSource.Play();
    }

    protected virtual void OnReleaseAttack(Vector2 target)
    {
        gunShotVFX.Play();
    }

    protected virtual void OnStopAttack()
    {
        audioSource.Stop();
    }
}
