using UnityEngine;

public class CustomMask : MonoBehaviour {
    [SerializeField] Transform _objectTransform;
    SpriteRenderer _spriteRenderer;
    Material _material;

    float _maxWidth;

    bool _burnEnabled = false;


    private void Update() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _material = _spriteRenderer.material;

        _maxWidth = _spriteRenderer.bounds.extents.magnitude;
    }
    private void FixedUpdate() {
        if(!_burnEnabled && Vector2.SqrMagnitude(_objectTransform.position - transform.position) < Mathf.Pow(_material.GetFloat("_MinTransitionDist") + _maxWidth, 2f)) {
            _material.SetFloat("_EnableBurn", 0f);
            _material.SetFloat("_StoppedTime", Time.time);
            _material.SetVector("_StartPos", _objectTransform.position);
            _burnEnabled = true;
        }
        else if(!_burnEnabled) {
            _material.SetVector("_StartPos", _objectTransform.position);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, GetComponent<SpriteRenderer>().bounds.extents.magnitude);
    }
}
