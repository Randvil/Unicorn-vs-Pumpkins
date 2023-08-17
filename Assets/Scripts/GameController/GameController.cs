using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [Header("Level")]
    [SerializeField] private GameObject[] levelPrefabs;

    [Header("Player")]
    [SerializeField] private InstantiationData playerInstantiationData;
    [SerializeField] private Vector2 initialPlayerSpeed;
    [SerializeField] private float xWinPosition;

    [Header("Camera")]
    [SerializeField] private GameObject cameraPrefab;

    [Header("Enemies")]
    [SerializeField] private EnemySpawnData[] enemySpawnDatas;
    [SerializeField] private Vector2 enemyRelativePosition;

    private GameObject levelGO;
    private GameObject playerGO;
    private Unicorn player;
    private GameObject cameraGO;
    private ICameraController cameraController;
    private Coroutine spawnEnemyCoroutine;
    private List<Pumpkin> enemies = new();
    private int enemyCounter = 0;
    private Coroutine checkWinCoroutine;

    public int CurrentLevel { get; private set; } = 1;

    private void Awake()
    {
        levelGO = CreateLevel(CurrentLevel);

        player = CreatePlayer(out playerGO);
        SetPlayerSettings(initialPlayerSpeed);

        cameraController = CreateCamera(playerGO);

        spawnEnemyCoroutine = StartCoroutine(SpawnEnemyCoroutine(enemySpawnDatas[CurrentLevel - 1]));

        checkWinCoroutine = StartCoroutine(CheckWinCoroutine(xWinPosition));
    }

    private GameObject CreateLevel(int levelNumber)
    {
        if (levelPrefabs.Length < levelNumber)
        {
            Debug.LogError($"Add more levels to {typeof(GameController)}", gameObject);
            return Instantiate(levelPrefabs[0]);
        }

        return Instantiate(levelPrefabs[levelNumber - 1]);
    }

    private Unicorn CreatePlayer(out GameObject playerGO)
    {
        playerGO = Instantiate(playerInstantiationData.prefab);

        if (playerGO.TryGetComponent(out Unicorn player) == false)
        {
            Destroy(playerGO);
            Debug.LogError($"Add a {typeof(Unicorn)}-associated prefab to the {playerInstantiationData.name} in {typeof(GameController)}", gameObject);
        }

        return player;
    }

    private void SetPlayerSettings(Vector2 speed)
    {
        if (player == null)
        {
            Debug.LogError($"You should create {typeof(Unicorn)} first");
            return;
        }

        if (playerInstantiationData.position != Vector3.zero)
        {
            playerGO.transform.position = playerInstantiationData.position;
        }

        if (playerInstantiationData.rotation != Vector3.zero)
        {
            playerGO.transform.eulerAngles = playerInstantiationData.rotation;
        }

        if (player is IMovable movablePlayer)
        {
            movablePlayer.Movement.StartMove(speed);
        }

        if (player is IMortal mortalPlayer)
        {
            mortalPlayer.DeathManager.DeathEvent.AddListener(OnPlayerDefeate);
        }
    }

    private ICameraController CreateCamera(GameObject target)
    {
        cameraGO = Instantiate(cameraPrefab);

        if (cameraGO.TryGetComponent(out ICameraController cameraController) == false)
        {
            Destroy(cameraGO);
            Debug.LogError($"Add a {typeof(ICameraController)}-associated prefab to the camera field in {typeof(GameController)}", gameObject);
        }

        cameraController.Target = target;

        return cameraController;
    }

    private IEnumerator SpawnEnemyCoroutine(EnemySpawnData enemySpawnData)
    {
        float minSpawnTime = enemySpawnData.minSpawnTime;
        float maxSpawnTime = enemySpawnData.maxSpawnTime;
        PumpkinData[] pumpkins = enemySpawnData.pumpkins;

        float nextSpawnTime = Time.time;

        while (true)
        {
            yield return new WaitUntil(() => Time.time >= nextSpawnTime);

            nextSpawnTime = Time.time + Random.Range(minSpawnTime, maxSpawnTime);

            PumpkinData currentPumpkinData = pumpkins[Random.Range(0, pumpkins.Length)];
            if (TrySpawnPumpkin(currentPumpkinData, out Pumpkin pumpkin) == false)
            {
                break;
            }

            pumpkin.Initialize(currentPumpkinData);

            pumpkin.transform.localScale *= currentPumpkinData.size;
            SkeletonAnimation skeleton = pumpkin.SkeletonAnimation;
            skeleton.initialSkinName = currentPumpkinData.skinName;
            skeleton.Initialize(true);
            skeleton.GetComponent<MeshRenderer>().sortingOrder = enemyCounter;


            if (pumpkin is IMovable movablePumpkin)
            {
                movablePumpkin.Movement.StartMove(Vector2.left * currentPumpkinData.speed);
            }

            if (pumpkin is IMortal mortalPumpkin)
            {
                mortalPumpkin.DeathManager.DeathEvent.AddListener(OnPumpkinDeath);
            }

            enemies.Add(pumpkin);
            enemyCounter++;
        }

        bool TrySpawnPumpkin(PumpkinData pumpkinData, out Pumpkin pumpkin)
        {
            pumpkin = null;

            if (playerGO == null)
            {
                return false;
            }
            
            pumpkin = Instantiate(pumpkinData.pumpkinPrefab, playerGO.transform.position + new Vector3(enemyRelativePosition.x, enemyRelativePosition.y, 0f), Quaternion.identity)
                .GetComponent<Pumpkin>();

            return true;
        }
    }

    private IEnumerator CheckWinCoroutine(float xWinPosition)
    {
        while(true)
        {
            if (playerGO.transform.position.x >= xWinPosition)
            {
                OnPlayerWin();
                yield break;
            }

            yield return new WaitForFixedUpdate();
        }
    }

    private void OnPlayerWin()
    {
        Debug.Log("Player won");

        StopCoroutine(checkWinCoroutine);
        StopCoroutine(spawnEnemyCoroutine);

        if (player is IWinnable winnablePlayer)
        {
            winnablePlayer.WinManager.Win();
        }

        foreach (Pumpkin enemy in enemies)
        {
            if (enemy is IDefeatable defeatablePumpkin)
            {
                defeatablePumpkin.DefeateManager.Lose();
            }
        }
    }

    private void OnPlayerDefeate(MonoBehaviour player)
    {
        Debug.Log("Player was defeated");

        StopCoroutine(checkWinCoroutine);
        StopCoroutine(spawnEnemyCoroutine);

        if (player is IDefeatable defeatablePlayer)
        {
            defeatablePlayer.DefeateManager.Lose();
        }

        foreach (Pumpkin enemy in enemies)
        {
            if (enemy is IWinnable winnablePumpkin)
            {
                winnablePumpkin.WinManager.Win();
            }
        }
    }

    private void OnPumpkinDeath(MonoBehaviour pumpkin)
    {
        enemies.Remove(pumpkin.GetComponent<Pumpkin>());
    }
}
