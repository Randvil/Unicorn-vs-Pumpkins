using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unicorn : MonoBehaviour, IFactionMember, IMovable, IMortal, IWinnable, IDefeatable
{
    [Header("Components")]
    [SerializeField] private new CapsuleCollider2D collider;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private ParticleSystem gunShotVFX;
    [SerializeField] private AudioSource sharedAudioSource;
    [SerializeField] private AudioSource walkAudioSource;
    [SerializeField] private AudioSource attackAudioSource;

    [Header("Data")]
    [SerializeField] private float preAttackDelay;
    [SerializeField] private float postAttackDelay;
    [SerializeField] private float explosionRadius;
    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private float deathDelay;

    [Header("View")]
    [SerializeField] private string movementAnimationName;
    [SerializeField] private float movementAnimationSpeedMultiplier;
    [SerializeField] private float movementSoundSpeedMultiplier;
    [SerializeField] private string attackAnimationName;
    [SerializeField] private float attackAnimationSpeed;
    [SerializeField] private float attackSoundSpeedMultiplier;
    [SerializeField] private string deathAnimationName;
    [SerializeField] private float deathAnimationSpeed;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private string winAnimationName;
    [SerializeField] private float winAnimationSpeed;

    public CapsuleCollider2D Collider => collider;
    public Rigidbody2D Rigidbody => rigidbody;
    public IFaction Faction { get; private set; }
    public IMovement Movement { get; private set; }
    public IWeapon Weapon { get; private set; }
    public IDeathManager DeathManager { get; private set; }
    public IWinManager WinManager { get; private set; }
    public IDefeateManager DefeateManager { get; private set; }

    private PlayerController playerController;
    private AnimatedAndVoicedMovementView movementView;
    private EMPGunView weaponView;
    private UnicornDeathView deathView;
    private CommonWinView winView;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        Faction = new Faction(eFaction.Unicons);

        Movement = new Movement(rigidbody);
        movementView = new AnimatedAndVoicedMovementView(Movement, skeletonAnimation, walkAudioSource, movementAnimationName, movementAnimationSpeedMultiplier, movementSoundSpeedMultiplier);

        Weapon = new EMPGun(this, preAttackDelay, postAttackDelay, explosionRadius, attackableLayers, Faction);
        weaponView = new EMPGunView(Weapon as EMPGun, skeletonAnimation, attackAudioSource, attackAnimationName, attackAnimationSpeed, attackSoundSpeedMultiplier, gunShotVFX);

        DeathManager = new DeathManager(this);
        deathView = new UnicornDeathView(DeathManager, skeletonAnimation, sharedAudioSource, deathAnimationName, deathAnimationSpeed, deathAudioClip);

        WinManager = new CommonWinManager();
        winView = new CommonWinView(WinManager, skeletonAnimation, winAnimationName, winAnimationSpeed);

        DefeateManager = new CommonDefeateManager();

        playerController = new PlayerController(this, Movement, Weapon, DeathManager, WinManager, DefeateManager, deathDelay);
    }
}
