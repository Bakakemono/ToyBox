using Unity.VisualScripting;
using UnityEngine;

public struct LeverData {
    public LeverData(Vector3 leftLimit, Vector3 rightLimit, float length, Transform leverTransform) {
        _leftLimit = leftLimit;
        _rightLimit = rightLimit;
        _Length = length;
        _leverTransform = leverTransform;
    }

    public Vector3 _leftLimit;
    public Vector3 _rightLimit;
    public float _Length;

    public Transform _leverTransform;
}

public class Lever : MonoBehaviour {
    [SerializeField] Vector3 _localLeftLimit;
    [SerializeField] Vector3 _localRightLimit;

    public LeverData _leverData;

    private void Start() {
        _leverData =
            new LeverData(
                transform.TransformPoint(_localLeftLimit),
                transform.TransformPoint(_localRightLimit),
                (transform.TransformPoint(_localLeftLimit) - transform.TransformPoint(_localRightLimit)).magnitude,
                transform
            );
    }

    private void Update() {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, _leverData._leftLimit.x, _leverData._rightLimit.x), _leverData._rightLimit.y, 0f);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawSphere(_leverData._leftLimit, 0.1f);
        Gizmos.DrawSphere(_leverData._leftLimit, 0.1f);
    }
}
