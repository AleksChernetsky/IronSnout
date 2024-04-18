using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private readonly string IdleAnim = "Idle", Punch1Anim = "Punch1", Punch2Anim = "Punch2", PunchComboAnim = "PunchCombo";

    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed = 0.16f;
    [SerializeField] private float _noInputTime = 0.5f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private AudioClip[] _hookSound;
    [SerializeField] private AudioClip[] _hitSound;

    private Animator _animator;
    private VitalitySystem _vitalitySystem;
    private AudioSource _audioSource;
    private List<Collider2D> _enemyColliders = new List<Collider2D>();

    private int _hitCount;
    private float _timer;
    private bool _hitOnEnemy;

    private bool RightArrowClick => Input.GetKeyDown(KeyCode.RightArrow);
    private bool LeftArrowClick => Input.GetKeyDown(KeyCode.LeftArrow);
    private bool CanAttack => CheckInput() && _timer >= _attackSpeed;
    private bool NoInput => (!RightArrowClick || !LeftArrowClick) && _timer >= _noInputTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _vitalitySystem = GetComponent<VitalitySystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (CanAttack)
        {
            StartCoroutine(Attack());
        }
        if (NoInput)
        {
            ResetCombo();
        }

    }
    private void FixedUpdate()
    {
        CheckEnemy();
    }
    public void TakeDamage(int damage)
    {
        _vitalitySystem.TakeDamage(damage);
    }
    private bool CheckInput()
    {
        if (RightArrowClick)
        {
            transform.rotation = Quaternion.identity;
            return true;
        }
        if (LeftArrowClick)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            return true;
        }
        return false;
    }
    private IEnumerator Attack()
    {
        foreach (var enemy in _enemyColliders)
        {
            if (enemy.TryGetComponent(out VitalitySystem vitalitySystem))
            {
                vitalitySystem.TakeDamage(_damage);
                _hitOnEnemy = true;
            }
        }
        switch (_hitCount)
        {
            case 0:
                _animator.SetTrigger(Punch1Anim);
                break;
            case 1:
                _animator.SetTrigger(Punch2Anim);
                break;
            case 2:
                _animator.SetTrigger(PunchComboAnim);
                break;
            case > 2:
                _hitCount = 0;
                goto case 0;
        }

        HitSound(_hitCount, _hitOnEnemy);
        _hitCount++;
        _timer = 0;
        _hitOnEnemy = false;
        yield break;
    }
    private void ResetCombo()
    {
        _animator.Play(IdleAnim);
        _hitCount = 0;
    }
    private void CheckEnemy()
    {
        _enemyColliders.Clear();
        RaycastHit2D[] _hits = Physics2D.LinecastAll(transform.position, _hitPoint.position, _enemyLayer);
        for (int i = 0; i < _hits.Length; i++)
        {
            _enemyColliders.Add(_hits[i].collider);
        }
    }
    private void HitSound(int HitCount, bool HitOnEnemy)
    {
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        if (HitOnEnemy)
        {
            _audioSource.PlayOneShot(_hitSound[HitCount]);
        }
        else
        {
            _audioSource.PlayOneShot(_hookSound[HitCount]);
        }
    }
}