using UnityEngine;

public class PlateformBehavior : MonoBehaviour {
    [SerializeField] Transform _plateform;
    [SerializeField] Lever _lever;

    [SerializeField] Vector3 _localLeft;
    [SerializeField] Vector3 _localRight;

    Vector3 _left;
    Vector3 _right;

    float _length;

    bool _inverted = false;

    public Vector3 _leverpos;
    public float _progression;

    private void Start() {
        _left = transform.TransformPoint(_localLeft);
        _right = transform.TransformPoint(_localRight);

        _length = (_left - _right).magnitude;
    }

    private void FixedUpdate() {
        _leverpos = _lever.transform.position;
        _progression = (_lever._leverData._leftLimit - _lever.transform.position).magnitude / _lever._leverData._Length * _length;
        _plateform.position =
            _inverted ? _right : _left +
            (_inverted ? _left - _right : _right - _left).normalized *
            Mathf.Clamp((_lever._leverData._leftLimit - _lever.transform.position).magnitude / _lever._leverData._Length, 0f, 1f) * _length;
    }

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(_left, 0.1f);
        Gizmos.DrawSphere(_right, 0.1f);
    }
}
