using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    private readonly string IdleAnim = "Idle", Punch1Anim = "Punch1", Punch2Anim = "Punch2", PunchComboAnim = "PunchCombo";

    [SerializeField] private int _damage;
    [SerializeField] private float _attackSpeed = 0.16f;
    [SerializeField] private float _noInputTime = 0.5f;
    [SerializeField] private Transform _hitPoint;

    private Animator _animator;
    private VitalitySystem _vitalitySystem;
    private List<Collider2D> _enemyColliders = new List<Collider2D>();
    public LayerMask _enemyLayer;

    private int _hitCount;
    private float _timer;

    private bool RightArrowClick => Input.GetKeyDown(KeyCode.RightArrow);
    private bool LeftArrowClick => Input.GetKeyDown(KeyCode.LeftArrow);
    private bool CanAttack => CheckInput() && _timer >= _attackSpeed;
    private bool NoInput => (!RightArrowClick || !LeftArrowClick) && _timer >= _noInputTime;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _vitalitySystem = GetComponent<VitalitySystem>();
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
            }
        }
        if (_hitCount == 0)
        {
            _animator.SetTrigger(Punch1Anim);
        }
        if (_hitCount == 1)
        {
            _animator.SetTrigger(Punch2Anim);
        }
        if (_hitCount == 2)
        {
            _animator.SetTrigger(PunchComboAnim);
            _hitCount = 0;
            _timer = 0;
            yield break;
        }
        _hitCount++;
        _timer = 0;
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
}