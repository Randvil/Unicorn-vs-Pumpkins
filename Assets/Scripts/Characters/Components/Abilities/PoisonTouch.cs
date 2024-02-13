using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PoisonTouch : IAbility
{
    private MonoBehaviour owner;
    private Collider2D collider;
    private IFaction faction;
    private LayerMask touchableLayers;
    private float touchDelay;

    private Coroutine touchCoroutine;

    public bool IsActivated => touchCoroutine != null;

    public UnityEvent ActivationEvent { get; } = new();
    public UnityEvent DeactivationEvent { get; } = new();

    public PoisonTouch(MonoBehaviour owner, Collider2D collider, IFaction faction, LayerMask touchableLayers, float touchDelay)
    {
        this.owner = owner;
        this.collider = collider;
        this.faction = faction;
        this.touchableLayers = touchableLayers;
        this.touchDelay = touchDelay;
    }

    public void Activate()
    {
        if (IsActivated)
        {
            return;
        }

        touchCoroutine = owner.StartCoroutine(TouchCoroutine());

        ActivationEvent.Invoke();
    }

    public void Deactivate()
    {
        if (IsActivated == false)
        {
            return;
        }

        owner.StopCoroutine(touchCoroutine);

        DeactivationEvent.Invoke();
    }

    private IEnumerator TouchCoroutine()
    {
        ContactFilter2D filter = new();
        filter.SetLayerMask(touchableLayers);
        Collider2D[] touchableObjects = new Collider2D[100];

        while (true)
        {
            collider.OverlapCollider(filter, touchableObjects);

            foreach (Collider2D touchableObject in touchableObjects)
            {
                if (touchableObject == null)
                {
                    break;
                }

                if (faction.DetermineAttitude(touchableObject) != eFactionalAttitude.Enemy)
                {
                    continue;
                }

                if (touchableObject.TryGetComponent(out IMortal mortalEnemy))
                {
                    mortalEnemy.DeathManager.Die();
                }
            }

            yield return new WaitForSeconds(touchDelay);
        }
    }
}
