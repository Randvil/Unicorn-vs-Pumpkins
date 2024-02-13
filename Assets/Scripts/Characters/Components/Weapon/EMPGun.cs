using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPGun : BaseWeapon
{
    public float PreAttackDelay { get; }
    public float PostAttackDelay { get; }
    public float ExplosionRadius { get; }

    public EMPGun(MonoBehaviour owner, float preAttackDelay, float postAttackDelay, float explosionRadius, LayerMask attackableLayers, IFaction faction) : base(owner, attackableLayers, faction)
    {
        PreAttackDelay = preAttackDelay;
        PostAttackDelay = postAttackDelay;
        ExplosionRadius = explosionRadius;
    }

    protected override IEnumerator ReleaseAttackCoroutine(Vector2 target)
    {
        yield return new WaitForSeconds(PreAttackDelay);

        Collider2D[] attackableObjects = Physics2D.OverlapCircleAll(target, ExplosionRadius, attackableLayers);

        foreach (Collider2D attackableObject in attackableObjects)
        {
            if (faction.DetermineAttitude(attackableObject) != eFactionalAttitude.Enemy)
            {
                continue;
            }

            if (attackableObject.TryGetComponent(out IMortal mortal))
            {
                mortal.DeathManager.Die();
            }
        }

        ReleaseAttackEvent.Invoke(target);

        yield return new WaitForSeconds(PostAttackDelay);
    }
}
