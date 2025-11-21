using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CustomMask : MonoBehaviour {
    [SerializeField] Transform _objectTransform;
    SpriteRenderer _spriteRenderer;
    Material _material;

    float _maxWidth;

    bool _burnEnabled = false;

    float _startTime = 0;
    float _burningTime = 0;
    bool _skipFirstFrame = false;

    private void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;

        _maxWidth = _spriteRenderer.bounds.extents.magnitude;

        _burningTime = _maxWidth * 2 / _spriteRenderer.material.GetFloat("_BurningSpeed");
    }
    private void FixedUpdate() {
        if(!_burnEnabled && _spriteRenderer.bounds.SqrDistance(_objectTransform.position) <= 0f) {
            _material.SetFloat("_EnableBurn", 0f);
            _startTime = Time.time;
            _material.SetFloat("_StoppedTime", _startTime);
            _material.SetVector("_StartPos", _objectTransform.position);
            _burnEnabled = true;
        }
        else if(!_burnEnabled) {
            _material.SetVector("_StartPos", _objectTransform.position);
        }

        if(_burnEnabled && (_startTime + _burningTime) < Time.time) {
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, GetComponent<SpriteRenderer>().bounds.extents.magnitude);
    }
}
