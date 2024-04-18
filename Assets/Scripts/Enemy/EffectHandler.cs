using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectHandler : MonoBehaviour
{
    public readonly string LevelAnim = "Base Layer.LevelHitTrigger";

    [Header("EffectsOnEnemy")]
    [SerializeField] private GameObject _bloodSplashPrefab;
    [SerializeField] private GameObject[] _boomPrefabs;

    [Header("EffectsOnScene")]
    [SerializeField] private Animator _levelAnimator;

    private void Start()
    {
        GlobalEvents.OnHitEvent.AddListener(EnvironmentReactionOnHit);
    }
    public GameObject PerformBloodSplash(Transform position)
    {
        GameObject splash = Instantiate(_bloodSplashPrefab, position);
        splash.transform.SetParent(transform);
        Destroy(splash, 1f);
        return splash;
    }
    public GameObject PerformBoom(Transform transform)
    {
        int random = Random.Range(0, _boomPrefabs.Length);
        GameObject boom = Instantiate(_boomPrefabs[random], transform.position, Quaternion.identity);
        boom.transform.SetParent(transform);
        Destroy(boom, 0.2f);
        return boom;
    }
    private void EnvironmentReactionOnHit()
    {
        _levelAnimator.Play(LevelAnim);
    }
}