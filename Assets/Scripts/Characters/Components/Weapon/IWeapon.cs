using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IWeapon
{
    public void StartAttack(Vector2 target);
    public void StopAttack();
    public bool IsAttacking { get; }
    public UnityEvent<Vector2> StartAttackEvent { get; }
    public UnityEvent<Vector2> ReleaseAttackEvent { get; }
    public UnityEvent StopAttackEvent { get; }
}
