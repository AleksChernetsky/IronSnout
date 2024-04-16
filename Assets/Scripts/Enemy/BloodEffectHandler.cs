using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectHandler : MonoBehaviour
{
    [SerializeField] private ParticleSystem _bloodSplash;
    [SerializeField] private ParticleSystem _bloodPuddle;

    public void PerformBloodSplash(Vector3 position, Quaternion rotation)
    {
        _bloodSplash.Play();
        _bloodSplash.transform.SetPositionAndRotation(position, rotation);
    }
    public void PerformBloodPuddle(Vector3 position, Quaternion rotation)
    {
        _bloodPuddle.Play();
        _bloodPuddle.transform.SetPositionAndRotation(position, rotation);
    }
}
