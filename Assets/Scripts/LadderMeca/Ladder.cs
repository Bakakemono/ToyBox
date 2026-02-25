using System.Net.Http.Headers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public struct LadderData {
    public LadderData(Transform ladderTransform, Vector2 bottomPos, Vector2 topPos, Vector2 bottomLandingPos, Vector2 topLandingPos) {
        _ladderTransform = ladderTransform;

        _bottomPos = bottomPos;
        _topPos = topPos;

        _bottomLandingPos = bottomLandingPos;
        _topLandingPos = topLandingPos;

        _localBottomPos = Vector2.zero;
        _localTopPos = Vector2.zero;

        _localBottomLandingPos = Vector2.zero;
        _localTopLandingPos = Vector2.zero;
    }

    public Transform _ladderTransform;

    public Vector2 _bottomPos;
    public Vector2 _topPos;

    public Vector2 _bottomLandingPos;
    public Vector2 _topLandingPos;

    public Vector2 _localBottomPos;
    public Vector2 _localTopPos;

    public Vector2 _localBottomLandingPos;
    public Vector2 _localTopLandingPos;
}

public class Ladder : MonoBehaviour {
#if UNITY_EDITOR
    [SerializeField] bool _DEBUG_DRAWGIRMOS;
#endif

    LadderData _data;

    [SerializeField] Vector2 _relativeBottomPos;
    [SerializeField] Vector2 _relativeTopPos;

    [SerializeField] Vector2 _relativeBottomLandingPos;
    [SerializeField] Vector2 _relativeTopLandingPos;

    private void Start() {
        _data = new LadderData(transform, _relativeBottomPos, _relativeTopPos, _relativeBottomLandingPos, _relativeTopLandingPos);

        Quaternion rotation = transform.localRotation;
        transform.localRotation = Quaternion.identity;

        _data._localBottomPos = transform.InverseTransformPoint(transform.position + (Vector3)_relativeBottomPos);
        _data._localTopPos = transform.InverseTransformPoint(transform.position + (Vector3)_relativeTopPos);
        _data._localBottomLandingPos = transform.InverseTransformPoint(transform.position + (Vector3)_relativeBottomLandingPos);
        _data._localTopLandingPos = transform.InverseTransformPoint(transform.position + (Vector3)_relativeTopLandingPos);

        transform.localRotation = rotation;
    }

    public LadderData GetData() {
        return _data;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(!_DEBUG_DRAWGIRMOS)
            return;

        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(transform.position + (Vector3)_bottomPos, transform.position + (Vector3)_topPos);
        
        if(_data._ladderTransform != null) {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.TransformPoint((Vector3)_data._localBottomPos), transform.TransformPoint((Vector3)_data._localTopPos));
        }

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (Vector3)_relativeBottomLandingPos, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + (Vector3)_relativeTopLandingPos, 0.1f);
    }
#endif
}