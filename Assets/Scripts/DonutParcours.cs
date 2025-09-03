using System;
using UnityEngine;

public class DonutParcours : MonoBehaviour
{
    float _ringRadius = 5f;
    float _rotationPerSecond = 0.1f;

    float _subRingRadius = 1.5f;
    float _subRingRotationPerSecond = 0.8f;

    private void Update() {
        float alteredRingRadius = _ringRadius + _subRingRadius * Mathf.Sin(Mathf.PI * 2 * Time.time * _subRingRotationPerSecond);
        Vector3 ringPos = new Vector3(
            alteredRingRadius * Mathf.Sin(Mathf.PI * 2 * Time.time * _rotationPerSecond),
            alteredRingRadius * Mathf.Cos(Mathf.PI * 2 * Time.time * _rotationPerSecond),
            _subRingRadius * Mathf.Cos(Mathf.PI * 2 * Time.time * _subRingRotationPerSecond)
        );

        transform.position = ringPos;
    }
}
