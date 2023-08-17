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

    public bool IsAlly(IFaction faction)
    {
        if (faction.CharacterFaction == CharacterFaction)
        {
            return true;
        }

        return false;
    }

    public bool IsAlly(Component character)
    {
        if (character.TryGetComponent(out IFactionMember factionMember))
        {
            return IsAlly(factionMember.Faction);
        }

        return false;
    }
}
