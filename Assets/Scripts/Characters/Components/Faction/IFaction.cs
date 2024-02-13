using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFaction
{
    public eFaction CharacterFaction { get; set; }
    public eFactionalAttitude DetermineAttitude(IFaction faction);
    public eFactionalAttitude DetermineAttitude(Component character);
}
