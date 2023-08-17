using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonDefeateView
{
    protected IDefeateManager defeateManager;
    protected SkeletonAnimation skeletonAnimation;

    protected string defeateAnimationName;
    protected float defeateAnimationSpeed;

    public CommonDefeateView(IDefeateManager defeateManager, SkeletonAnimation skeletonAnimation, string defeateAnimationName, float defeateAnimationSpeed)
    {
        this.skeletonAnimation = skeletonAnimation;

        this.defeateAnimationName = defeateAnimationName;
        this.defeateAnimationSpeed = defeateAnimationSpeed;

        defeateManager.LoseEvent.AddListener(OnLose);
    }

    protected virtual void OnLose()
    {
        skeletonAnimation.loop = true;
        skeletonAnimation.timeScale = defeateAnimationSpeed;
        skeletonAnimation.AnimationName = defeateAnimationName;
    }
}
