using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedAndVoicedMovementView : AnimatedMovementView
{
    protected AudioSource audioSource;

    protected float soundSpeedMultiplier;

    public AnimatedAndVoicedMovementView(IMovement movement, SkeletonAnimation skeletonAnimation, AudioSource audioSource, string animationName, float animationSpeedMultiplier, float soundSpeedMultiplier) : base(movement, skeletonAnimation, animationName, animationSpeedMultiplier)
    {
        this.audioSource = audioSource;

        this.soundSpeedMultiplier = soundSpeedMultiplier;
    }

    protected override void OnStartMove()
    {
        base.OnStartMove();

        audioSource.pitch = Mathf.Abs(movement.Velocity.x) * soundSpeedMultiplier;
        audioSource.Play();
    }

    protected override void OnStopMove()
    {
        audioSource.Stop();
    }
}
