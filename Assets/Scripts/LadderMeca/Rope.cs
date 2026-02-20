using UnityEditor.Tilemaps;
using UnityEngine;


public struct RopeData {
    public RopeData(Vector3 localBottom, Vector3 localTop, Vector3 bottom, Vector3 top, float lenght) {
        _localBottom = localBottom;
        _localTop = localTop;

        _bottom = bottom;
        _top = top;

        _length = lenght;
    }
    [SerializeField] public Vector3 _localBottom;
    [SerializeField] public Vector3 _localTop;

    public Vector3 _bottom;
    public Vector3 _top;
    public float _length;
}
public class Rope : MonoBehaviour {
#if UNITY_EDITOR
    [Header("DEBUG")]
    [SerializeField] bool _enableGizmos = false;
#endif

    [Header("Params")]
    [SerializeField] Vector3 _localBottom;
    [SerializeField] Vector3 _localTop;

    public RopeData _data;

    private void Start() {
        CalculateParams();
    }

    void CalculateParams() {
        // Make sure the lowest point is the bottom one
        if(_localTop.y < _localBottom.y) {
            Vector3 top = _localTop;
            _localTop = _localBottom;
            _localBottom = top;
        }

        _data = new RopeData(
            _localBottom,
            _localTop,
            transform.TransformPoint(_localBottom),
            transform.TransformPoint(_localTop),
            Vector3.Magnitude(transform.TransformPoint(_localBottom) - transform.TransformPoint(_localTop)));
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected() {
        CalculateParams();

        Gizmos.color = Color.cornflowerBlue;

        Gizmos.DrawLine(_data._bottom, _data._top);
        Gizmos.DrawSphere(_data._bottom, 0.1f);
        Gizmos.DrawSphere(_data._top, 0.1f);
    }
#endif
}