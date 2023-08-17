using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CommonWinManager : IWinManager
{
    public bool IsWinner { get; private set; }

    public UnityEvent WinEvent { get; } = new();

    public void Win()
    {
        if (IsWinner)
        {
            return;
        }

        IsWinner = true;

        WinEvent.Invoke();
    }
}
