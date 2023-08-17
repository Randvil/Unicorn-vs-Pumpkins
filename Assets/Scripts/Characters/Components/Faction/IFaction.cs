using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFaction
{
    public eFaction CharacterFaction { get; set; }
    public bool IsAlly(IFaction faction);
    public bool IsAlly(Component character);
    public static bool TryGetFaction(Component character, out IFaction faction)
    {
        faction = null;

        if (character.TryGetComponent(out IFactionMember factionMember))
        {
            faction = factionMember.Faction;
            return true;
        }

        return false;
    }
}
