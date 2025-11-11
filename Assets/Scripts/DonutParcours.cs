using System;
using UnityEngine;

public class DonutParcours : MonoBehaviour
{
    [SerializeField] GameObject _prefab;
    [SerializeField] int _sphereNmb = 1;
    private GameObject[] _spheres;
    [SerializeField] float _ringRadius = 5f;
    [SerializeField] float _rotationPerSecond = 0.1f;

    [SerializeField] float _subRingRadius = 1.5f;
    [SerializeField] float _subRingRotationPerSecond = 0.8f;

    private void Start() {
        _spheres = new GameObject[_sphereNmb];

        for(int i = 0; i < _sphereNmb; i++) {
            _spheres[i] = Instantiate(_prefab);
        }
    }

    private void Update() {
        for(int i = 0; i < _sphereNmb; i++) {
            float alteredRingRadius = _ringRadius + _subRingRadius * Mathf.Sin(Mathf.PI * 2 * (Time.time + _subRingRotationPerSecond * ((float)i / (float)_sphereNmb - 1f)) * _subRingRotationPerSecond);
            Vector3 ringPos = new Vector3(
                alteredRingRadius * Mathf.Sin(Mathf.PI * 2 * (Time.time + _rotationPerSecond * ((float)i / (float)_sphereNmb - 1f)) * _rotationPerSecond),
                alteredRingRadius * Mathf.Cos(Mathf.PI * 2 * (Time.time + _rotationPerSecond * ((float)i / (float)_sphereNmb - 1f)) * _rotationPerSecond),
                _subRingRadius * Mathf.Cos(Mathf.PI * 2 * (Time.time + _subRingRotationPerSecond * ((float)i / (float)_sphereNmb - 1f)) * _subRingRotationPerSecond)
            );

            //transform.position = ringPos;
            _spheres[i].transform.position = ringPos;
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(Vector3.zero, _ringRadius);
    }
}
