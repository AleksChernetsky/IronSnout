using UnityEngine;

public class EnemyActions : MonoBehaviour
{
    public readonly string WalkAnim = "Walk", RunAnim = "Run", AttackAnim = "Attack", HurtAnim = "Hurt", DeadAnim = "Dead";

    private VitalitySystem _vitalitySystem;
    private EffectHandler _effectHandler;
    private AudioSource _audioSource;

    [field: SerializeField] public int Damage { get; set; }
    [field: SerializeField] public bool FastEnemy { get; set; }
    [field: SerializeField] public float MovementSpeed { get; set; }
    [field: SerializeField] public AudioClip[] HitSound { get; private set; }
    [field: SerializeField] public Transform[] HitPositions { get; set; }

    public float DistanceToAttack { get => DistanceToAttack = 1f; set { } }
    public float AttackSpeed { get => AttackSpeed = 1f; set { } }
    public Transform Target { get; private set; }
    public Animator Animator { get; private set; }

    public StateMachine StateMachine { get; set; }
    public StateCheckDirection StateCheckDirection { get; set; }
    public StateMove StateMove { get; set; }
    public StateAttack StateAttack { get; set; }
    public StateTakeHit StateTakeHit { get; set; }
    public StateDie StateDie { get; set; }

    public bool CanAttack => Vector2.Distance(transform.position, Target.position) <= DistanceToAttack;

    private void Awake()
    {
        Animator = GetComponent<Animator>();
        Target = FindObjectOfType<PlayerAction>().transform;
        _vitalitySystem = GetComponent<VitalitySystem>();
        _effectHandler = FindObjectOfType<EffectHandler>();
        _audioSource = GetComponent<AudioSource>();

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
        _vitalitySystem.OnTakeDamage += TakeDamage;
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
        int randomClip = Random.Range(0, HitSound.Length);
        if (Target.TryGetComponent(out VitalitySystem vitalitySystem))
        {
            vitalitySystem.TakeDamage(Damage);
            _audioSource.pitch = Random.Range(0.8f, 1.2f);
            _audioSource.PlayOneShot(HitSound[randomClip]);
        }
    }
    public void PerformEffects()
    {
        int randomPosition = Random.Range(0, HitPositions.Length);
        _effectHandler.PerformBloodSplash(HitPositions[randomPosition]);
        _effectHandler.PerformBoom(HitPositions[randomPosition]);
    }
    private void TakeDamage()
    {
        if (StateMachine.CurrentState != StateTakeHit || StateMachine.CurrentState != StateDie)
        {
            PerformEffects();
            StateMachine.SwitchState(StateTakeHit);
        }
    }
    private void Die()
    {
        if (StateMachine.CurrentState != StateDie)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
            StateMachine.SwitchState(StateDie);
        }
    }
    public void ResetCharacter()
    {
        _vitalitySystem.ResetCharacter();
        gameObject.layer = LayerMask.NameToLayer("Enemy");
    }
}