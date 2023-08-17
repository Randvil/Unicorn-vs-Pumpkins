using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IWinManager
{
    public void Win();
    public bool IsWinner { get; }
    public UnityEvent WinEvent { get; }
}
