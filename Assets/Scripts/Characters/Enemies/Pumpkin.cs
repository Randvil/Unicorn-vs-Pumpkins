using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour, IFactionMember, IMovable, IMortal, IWinnable, IDefeatable
{
    [Header("Components")]
    [SerializeField] private GameObject mesh;
    [SerializeField] private new CapsuleCollider2D collider;
    [SerializeField] private new Rigidbody2D rigidbody;
    [SerializeField] private SkeletonAnimation skeletonAnimation;
    [SerializeField] private AudioSource sharedAudioSource;
    [SerializeField] private ParticleSystem headExplosion;
    [SerializeField] private ParticleSystem droplets;

    [Header("Data")]
    [SerializeField] private LayerMask attackableLayers;
    [SerializeField] private float poisonTouchDelay;
    [SerializeField] private float deathDelay;

    [Header("View")]
    [SerializeField] private string movementAnimationName;
    [SerializeField] private float movementAnimationSpeedMultiplier;
    [SerializeField] private string deathAnimationName;
    [SerializeField] private float deathAnimationSpeed;
    [SerializeField] private AudioClip deathAudioClip;
    [SerializeField] private string winAnimationName;
    [SerializeField] private float winAnimationSpeed;
    [SerializeField] private float winAnimationSpread;
    [SerializeField] private string defeateAnimationName;
    [SerializeField] private float defeateAnimationSpeed;
    [SerializeField] private float defeateAnimationSpeedSpread;

    public PumpkinData PumpkinData { get; private set; }
    public CapsuleCollider2D Collider => collider;
    public Rigidbody2D Rigidbody => rigidbody;
    public SkeletonAnimation SkeletonAnimation => skeletonAnimation;
    public IFaction Faction { get; private set; }
    public IMovement Movement { get; private set; }
    public IAbility PoisonTouch { get; private set; }
    public IDeathManager DeathManager { get; private set; }
    public IWinManager WinManager { get; private set; }
    public IDefeateManager DefeateManager { get; private set; }

    private AnimatedMovementView movementView;
    private PumpkinDeathView deathView;
    private CommonWinView winView;
    private CommonDefeateView defeateView;
    private PumpkinController pumpkinController;

    public void Initialize(PumpkinData pumpkinData)
    {
        PumpkinData = pumpkinData;

        Faction = new Faction(eFaction.Pumpkins);

        Movement = new Movement(rigidbody);
        movementView = new AnimatedMovementView(Movement, skeletonAnimation, movementAnimationName, movementAnimationSpeedMultiplier);

        PoisonTouch = new PoisonTouch(this, Collider, Faction, attackableLayers, poisonTouchDelay);
        PoisonTouch.Activate();

        DeathManager = new DeathManager(this);
        deathView = new PumpkinDeathView(PumpkinData, DeathManager, mesh, sharedAudioSource, deathAudioClip, headExplosion, droplets);

        WinManager = new CommonWinManager();
        winView = new PumpkinWinView(WinManager, skeletonAnimation, winAnimationName, winAnimationSpeed, winAnimationSpread);

        DefeateManager = new CommonDefeateManager();
        defeateView = new PumpkinDefeateView(DefeateManager, skeletonAnimation, defeateAnimationName, defeateAnimationSpeed, defeateAnimationSpeedSpread);

        pumpkinController = new PumpkinController(this, Movement, PoisonTouch, DeathManager, WinManager, DefeateManager, deathDelay);
    }
}
