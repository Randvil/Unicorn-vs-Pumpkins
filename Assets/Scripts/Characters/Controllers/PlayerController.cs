using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController
{
    private MonoBehaviour owner;
    private IMovement movement;
    private IWeapon weapon;
    private IDeathManager deathManager;
    private IWinManager winManager;
    private IDefeateManager defeateManager;

    private float deathDelay;

    private Vector2 velocity;
    private Coroutine inputCoroutine;
    private Coroutine deathCoroutine;

    public PlayerController(MonoBehaviour owner, IMovement movement, IWeapon weapon, IDeathManager deathManager, IWinManager winManager, IDefeateManager defeateManager, float deathDelay)
    {
        this.owner = owner;
        this.movement = movement;
        this.weapon = weapon;
        this.deathManager = deathManager;
        this.winManager = winManager;
        this.defeateManager = defeateManager;

        this.deathDelay = deathDelay;

        inputCoroutine = owner.StartCoroutine(InputCoroutine());

        weapon.StartAttackEvent.AddListener(OnStartAttack);
        weapon.StopAttackEvent.AddListener(OnStopAttack);
        deathManager.DeathEvent.AddListener(OnDeath);
        winManager.WinEvent.AddListener(OnWin);
        defeateManager.LoseEvent.AddListener(OnLose);
    }

    private IEnumerator InputCoroutine()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                weapon.StartAttack(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }

            if (Input.GetMouseButtonDown(1))
            {
                weapon.StopAttack();
            }

            yield return null;
        }
    }

    private void OnStartAttack(Vector2 target)
    {
        velocity = movement.Velocity;
        movement.StopMove();
    }

    private void OnStopAttack()
    {
        movement.StartMove(velocity);
    }

    private void OnDeath(MonoBehaviour player)
    {
        if (deathCoroutine != null)
        {
            return;
        }

        deathCoroutine = player.StartCoroutine(DeathCoroutine());
    }

    private void OnWin()
    {
        RemoveListeners();
        StopActions();
    }

    private void OnLose()
    {
        RemoveListeners();
        StopActions();
    }

    private IEnumerator DeathCoroutine()
    {
        RemoveListeners();

        StopActions();

        yield return new WaitForSeconds(deathDelay);

        Object.Destroy(owner.gameObject);
    }

    private void RemoveListeners()
    {
        weapon.StartAttackEvent.RemoveListener(OnStartAttack);
        weapon.StopAttackEvent.RemoveListener(OnStopAttack);
        deathManager.DeathEvent.RemoveListener(OnDeath);
        winManager.WinEvent.RemoveListener(OnWin);
        defeateManager.LoseEvent.RemoveListener(OnLose);
    }

    private void StopActions()
    {
        owner.StopCoroutine(inputCoroutine);
        movement.StopMove();
        weapon.StopAttack();
    }
}
