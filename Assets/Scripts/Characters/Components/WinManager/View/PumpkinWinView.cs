using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinWinView : CommonWinView
{
    protected float winAnimationSpeedSpread;

    public PumpkinWinView(IWinManager winManager, SkeletonAnimation skeletonAnimation, string winAnimationName, float winAnimationSpeed, float winAnimationSpeedSpread) : base(winManager, skeletonAnimation, winAnimationName, winAnimationSpeed)
    {
        this.winAnimationSpeedSpread = winAnimationSpeedSpread;
    }

    protected override void OnWin()
    {
        skeletonAnimation.loop = true;
        skeletonAnimation.timeScale = Random.Range(winAnimationSpeed - winAnimationSpeedSpread, winAnimationSpeed + winAnimationSpeedSpread);
        skeletonAnimation.AnimationName = winAnimationName;
    }
}
