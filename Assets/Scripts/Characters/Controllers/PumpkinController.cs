using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinController
{
    private MonoBehaviour owner;
    private IMovement movement;
    private IAbility ability;
    private IDeathManager deathManager;
    private IWinManager winManager;
    private IDefeateManager defeateManager;

    private float deathDelay;

    private Coroutine deathCoroutine;

    public PumpkinController(MonoBehaviour owner, IMovement movement, IAbility ability, IDeathManager deathManager, IWinManager winManager, IDefeateManager defeateManager, float deathDelay)
    {
        this.owner = owner;
        this.movement = movement;
        this.ability = ability;
        this.deathManager = deathManager;
        this.winManager = winManager;
        this.defeateManager = defeateManager;

        this.deathDelay = deathDelay;

        deathManager.DeathEvent.AddListener(OnDeath);
        winManager.WinEvent.AddListener(OnWin);
        defeateManager.LoseEvent.AddListener(OnLose);
    }

    private void OnDeath(MonoBehaviour pumpkin)
    {
        if (deathCoroutine != null)
        {
            return;
        }

        deathCoroutine = pumpkin.StartCoroutine(DeathCoroutine());
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
        deathManager.DeathEvent.RemoveListener(OnDeath);
        winManager.WinEvent.RemoveListener(OnWin);
        defeateManager.LoseEvent.RemoveListener(OnLose);
    }

    private void StopActions()
    {
        movement.StopMove();
        ability.Deactivate();
    }
}
