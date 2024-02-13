using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faction : IFaction
{
    public eFaction CharacterFaction { get; set; }

    public Faction(eFaction initialFaction)
    {
        CharacterFaction = initialFaction;
    }

    public eFactionalAttitude DetermineAttitude(IFaction faction)
    {
        eFactionalAttitude attitude;

        if (faction.CharacterFaction == CharacterFaction)
        {
            attitude = eFactionalAttitude.Ally;
        }
        else
        {
            attitude = eFactionalAttitude.Enemy;
        }

        return attitude;
    }

    public eFactionalAttitude DetermineAttitude(Component character)
    {
        eFactionalAttitude attitude = eFactionalAttitude.NoFaction;

        if (character.TryGetComponent(out IFactionMember factionMember))
        {
            return (DetermineAttitude(factionMember.Faction));
        }

        return attitude;
    }
}
