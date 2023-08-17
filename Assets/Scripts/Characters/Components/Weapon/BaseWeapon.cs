using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseWeapon : IWeapon
{
    protected MonoBehaviour owner;
    protected LayerMask attackableLayers;
    protected IFaction faction;

    protected Coroutine attackCoroutine;

    public bool IsAttacking { get; protected set; }

    public UnityEvent<Vector2> StartAttackEvent { get; } = new();
    public UnityEvent<Vector2> ReleaseAttackEvent { get; } = new();
    public UnityEvent StopAttackEvent { get; } = new();

    public BaseWeapon(MonoBehaviour owner, LayerMask attackableLayers, IFaction faction)
    {
        this.owner = owner;
        this.attackableLayers = attackableLayers;
        this.faction = faction;
    }

    public void StartAttack(Vector2 target)
    {
        if (attackCoroutine != null)
        {
            return;
        }

        attackCoroutine = owner.StartCoroutine(AttackCoroutine(target));

        IsAttacking = true;

        StartAttackEvent.Invoke(target);
    }

    public void StopAttack()
    {
        if (attackCoroutine == null)
        {
            return;
        }

        owner.StopCoroutine(attackCoroutine);
        attackCoroutine = null;

        IsAttacking = false;

        StopAttackEvent.Invoke();
    }

    protected virtual IEnumerator AttackCoroutine(Vector2 target)
    {
        yield return ReleaseAttackCoroutine(target);

        StopAttack();
    }

    protected abstract IEnumerator ReleaseAttackCoroutine(Vector2 target);
}
