using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinDefeateView : CommonDefeateView
{
    protected float defeateAnimationSpeedSpread;

    public PumpkinDefeateView(IDefeateManager defeateManager, SkeletonAnimation skeletonAnimation, string defeateAnimationName, float defeateAnimationSpeed, float defeateAnimationSpeedSpread) : base(defeateManager, skeletonAnimation, defeateAnimationName, defeateAnimationSpeed)
    {
        this.defeateAnimationSpeedSpread = defeateAnimationSpeedSpread;
    }

    protected override void OnLose()
    {
        skeletonAnimation.loop = true;
        skeletonAnimation.timeScale = Random.Range(defeateAnimationSpeed - defeateAnimationSpeedSpread, defeateAnimationSpeed + defeateAnimationSpeedSpread);
        skeletonAnimation.AnimationName = defeateAnimationName;
    }
}
