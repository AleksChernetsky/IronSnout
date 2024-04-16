using System;

using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public readonly string WalkAnim = "Walk", RunAnim = "Run", AttackAnim = "Attack", HurtAnim = "Hurt", DeadAnim = "Dead";

    private VitalitySystem _vitalitySystem;

    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public bool FastEnemy { get; set; }

    public float DistanceToAttack { get => DistanceToAttack = 1f; set { } }
    public int MovementSpeed { get => MovementSpeed = 1; set { } }
    public float AttackSpeed { get => AttackSpeed = 1f; set { } }
    public Transform Target { get; private set; }
    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; set; }
    public StateCheckDirection StateCheckDirection { get; set; }
    public StateMove StateMove { get; set; }
    public StateAttack StateAttack { get; set; }
    public StateTakeHit StateTakeHit { get; set; }
    public StateDie StateDie { get; set; }

    [Header("Effects")]
    [SerializeField] private BloodEffectHandler _bloodEffectHandler;
    [SerializeField] private Transform _bloodSplash;
    [SerializeField] private Transform _bloodPuddle;

    public event Action OnEnemyDie;

    public bool CanAttack => Vector2.Distance(transform.position, Target.position) <= DistanceToAttack;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Target = FindObjectOfType<PlayerAction>().transform;
        _vitalitySystem = GetComponent<VitalitySystem>();

        StateMachine = new StateMachine();
        StateCheckDirection = new StateCheckDirection(StateMachine, this, _vitalitySystem);
        StateMove = new StateMove(StateMachine, this, _vitalitySystem);
        StateAttack = new StateAttack(StateMachine, this, _vitalitySystem);
        StateTakeHit = new StateTakeHit(StateMachine, this, _vitalitySystem);
        StateDie = new StateDie(StateMachine, this, _vitalitySystem);
    }
    private void Start()
    {
        StateMachine.Initialize(StateCheckDirection);

        _vitalitySystem.OnTakeDamage += TakeHit;
        _vitalitySystem.OnDeath += Die;
    }
    private void Update()
    {
        if (StateMachine.CurrentState != null)
        {
            StateMachine.CurrentState.UpdateState();
        }
    }
    private void Attack() // call in attack animation, animation start triggered in attack state
    {
        if (Target.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(Damage);
        }
    }
    private void TakeHit()
    {
        if (StateMachine.CurrentState != StateDie)
        {
            //_bloodEffectHandler.PerformBloodSplash(new Vector3(0, 180, 0), transform.localRotation);
            StateMachine.SwitchState(StateTakeHit);
        }
    }
    private void Die()
    {
        if (StateMachine.CurrentState != StateDie)
        {
            //_bloodEffectHandler.PerformBloodPuddle(transform.localPosition, transform.localRotation);
            OnEnemyDie?.Invoke();
            StateMachine.SwitchState(StateDie);
        }
    }
}