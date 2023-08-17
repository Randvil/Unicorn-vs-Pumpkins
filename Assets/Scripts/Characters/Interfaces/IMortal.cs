using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMortal
{
    public IDeathManager DeathManager { get; }
}
