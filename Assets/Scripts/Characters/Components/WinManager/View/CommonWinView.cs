using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonWinView
{
    protected IWinManager winManager;
    protected SkeletonAnimation skeletonAnimation;

    protected string winAnimationName;
    protected float winAnimationSpeed;

    public CommonWinView(IWinManager winManager, SkeletonAnimation skeletonAnimation, string winAnimationName, float winAnimationSpeed)
    {
        this.skeletonAnimation = skeletonAnimation;

        this.winAnimationName = winAnimationName;
        this.winAnimationSpeed = winAnimationSpeed;

        winManager.WinEvent.AddListener(OnWin);
    }

    protected virtual void OnWin()
    {
        skeletonAnimation.loop = true;
        skeletonAnimation.timeScale = winAnimationSpeed;
        skeletonAnimation.AnimationName = winAnimationName;
    }
}
