using UnityEngine;
using UnityEngine.UIElements;

public class LadderCllimber : MonoBehaviour {
    Rigidbody2D _rb;

    [SerializeField] float _speed = 3f;
    
    // Ladder Params
    [SerializeField] ContactFilter2D _contactFilter;
    bool _climbingLadder = false;
    LadderData _data;

    Vector2 _ladderPos;

    Vector3 _aimPos;
    bool _playerInPlace = false;
    bool _ladderPosCalculated = false;

    private void Start() {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        if(!_climbingLadder) {
            Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0f);
            _rb.linearVelocity = new Vector2(move.x * _speed, _rb.linearVelocity.y);
        }
        else {
            Climbing();
        }


        if(Input.GetKeyDown(KeyCode.E)) {
            DetectLadder();
        }
    }

    private void LateUpdate() {
        if(_climbingLadder) {
            CorrectLadderPosition();
        }
    }

    // Detect if there is a ladder in front of the player to grab.
    void DetectLadder() {
        Collider2D[] result = new Collider2D[1];
        
        if(Physics2D.OverlapBox((Vector2)transform.position + Vector2.right * 0.6f, Vector2.one * 0.2f, 0f, _contactFilter, result) > 0) {
            _data = result[0].GetComponent<Ladder>().GetData();
            _rb.gravityScale = 0;
            _climbingLadder = true;
            
        }
    }

    void Climbing() {
        float HInput = Input.GetAxis("Vertical");

        _rb.linearVelocity = new Vector2(0f, HInput * _speed);

        bool landing = false;

        if (HInput > 0 && transform.position == _data._ladderTransform.TransformPoint(_data._localTopPos)) {
            transform.position = _data._ladderTransform.TransformPoint(_data._localTopLandingPos);
            landing = true;
        }
        else if (HInput < 0 && transform.position == _data._ladderTransform.TransformPoint(_data._localBottomPos)) {
            transform.position = _data._ladderTransform.TransformPoint(_data._localBottomLandingPos);
            landing = true;
        }

        if(landing) {
            _climbingLadder = false;
            _rb.gravityScale = 1;
            _rb.linearVelocity = Vector2.zero;
        }
    }

    void CorrectLadderPosition() {
        // Projected Player position on the ladder track.
        Vector3 projPos = Vector3.Project(
            transform.position - _data._ladderTransform.TransformPoint(_data._localBottomPos),
            (_data._localTopPos - _data._localBottomPos).normalized
            );

        _ladderPos = _data._ladderTransform.TransformPoint(_data._localBottomPos) + projPos;

        // Bottom and top player position on the ladder converted to world.
        Vector2 _worldBottomPos = _data._ladderTransform.TransformPoint(_data._localBottomPos);
        Vector2 _worldTopPos = _data._ladderTransform.TransformPoint(_data._localTopPos);

        // Corrected Clamp position to be sur player stay between in the right area.
        transform.position = new Vector2(Mathf.Clamp(_ladderPos.x, _worldBottomPos.x, _worldTopPos.x), Mathf.Clamp(_ladderPos.y, _worldBottomPos.y, _worldTopPos.y));
    }

    private void OnDrawGizmos() {
        if(!_climbingLadder)
            return;

        Gizmos.color = Color.yellow;

        Vector2 _worldBottomPos = _data._ladderTransform.TransformPoint(_data._localBottomPos);
        Vector2 _worldTopPos = _data._ladderTransform.TransformPoint(_data._localTopPos);


        //Gizmos.DrawSphere(_ladderPos, 0.2f);
        Gizmos.DrawSphere(new Vector2(Mathf.Clamp(_ladderPos.x, _worldBottomPos.x, _worldTopPos.x), Mathf.Clamp(_ladderPos.y, _worldBottomPos.y, _worldTopPos.y)), 0.2f);
    }
}