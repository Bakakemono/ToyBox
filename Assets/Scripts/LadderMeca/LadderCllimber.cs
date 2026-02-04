using UnityEngine;

public class LadderCllimber : MonoBehaviour {
    Rigidbody2D _rb;

    [SerializeField] float _speed = 3f;
    
    // Ladder Params
    [SerializeField] ContactFilter2D _contactFilter;
    bool _climbingLadder = false;
    LadderData _data;

    Vector2 _ladderPos;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0f);
        _rb.linearVelocity = new Vector2(move.x * _speed, _rb.linearVelocity.y);

        if(_climbingLadder) {
            ClimbLadder();
        }

        if(Input.GetKeyDown(KeyCode.E)) {
            DetectLadder();
        }
    }

    void DetectLadder() {
        Collider2D[] result = new Collider2D[1];

        if(Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * 0.6f, Vector2.one * 0.2f, 0f, _contactFilter, result) > 0) {
            _data = result[0].GetComponent<Ladder>().GetData();
            _climbingLadder = true;
        }
    }

    void ClimbLadder() {
        Vector3 projPos = Vector3.Project(
            transform.position - _data._ladderTransform.TransformPoint(_data._localBottomPos),
            (_data._topPos - _data._bottomPos).normalized
            );

        _ladderPos = _data._ladderTransform.TransformPoint(_data._bottomPos) + projPos;
    }

    private void OnDrawGizmos() {
        if(!_climbingLadder)
            return;

        Gizmos.color = Color.yellow;

        Vector2 _worldBottomPos = _data._ladderTransform.TransformPoint(_data._localBottomPos);
        Vector2 _worldTopPos = _data._ladderTransform.TransformPoint(_data._localTopPos);


        Gizmos.DrawSphere(_ladderPos, 0.2f);
        Gizmos.DrawSphere(new Vector2(Mathf.Clamp(_ladderPos.x, _worldBottomPos.x, _worldTopPos.x), Mathf.Clamp(_ladderPos.y, _worldBottomPos.y, _worldTopPos.y)), 0.2f);
    }
}