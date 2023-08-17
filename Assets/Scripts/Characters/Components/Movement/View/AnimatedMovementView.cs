using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedMovementView
{
    protected IMovement movement;
    protected SkeletonAnimation skeletonAnimation;

    protected string animationName;
    protected float animationSpeedMultiplier;

    public AnimatedMovementView(IMovement movement, SkeletonAnimation skeletonAnimation, string animationName, float animationSpeedMultiplier)
    {
        this.movement = movement;
        this.skeletonAnimation = skeletonAnimation;

        this.animationName = animationName;
        this.animationSpeedMultiplier = animationSpeedMultiplier;

        movement.StartMoveEvent.AddListener(OnStartMove);
        movement.StopMoveEvent.AddListener(OnStopMove);
    }

    protected virtual void OnStartMove()
    {
        skeletonAnimation.AnimationName = animationName;
        skeletonAnimation.timeScale = Mathf.Abs(movement.Velocity.x) * animationSpeedMultiplier;
    }

    protected virtual void OnStopMove()
    {
        
    }
}
