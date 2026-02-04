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

    [SerializeField] Vector2 _bottomPos;
    [SerializeField] Vector2 _topPos;

    [SerializeField] Vector2 _bottomLandingPos;
    [SerializeField] Vector2 _topLandingPos;

    private void Start() {
        _data = new LadderData(transform, _bottomPos, _topPos, _bottomLandingPos, _topLandingPos);

        _data._localBottomPos = transform.InverseTransformPoint(new Vector3(_bottomPos.x / transform.localScale.x, _bottomPos.y / transform.localScale.y));
        _data._localTopPos = transform.InverseTransformPoint(new Vector3(_topPos.x / transform.localScale.x, _topPos.y / transform.localScale.y));
        _data._localBottomLandingPos = transform.InverseTransformPoint(new Vector3(_bottomLandingPos.x / transform.localScale.x, _bottomLandingPos.y / transform.localScale.y));
        _data._localTopLandingPos = transform.InverseTransformPoint(new Vector3(_topLandingPos.x / transform.localScale.x, _topLandingPos.y / transform.localScale.y));
    }

    public LadderData GetData() {
        return _data;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos() {
        if(!_DEBUG_DRAWGIRMOS)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + (Vector3)_bottomPos, transform.position + (Vector3)_topPos);

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position + (Vector3)_bottomLandingPos, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position + (Vector3)_topLandingPos, 0.1f);
    }
#endif
}