using UnityEngine;

public class GoToPoint : Node {
    Transform _transform;
    Rigidbody2D _rigidbody;
    RobotBehavior _behavior;

    float _speed = 10f;

    public GoToPoint(BaseBehaviorTree root) : base(root) {
        _behavior = (RobotBehavior)root;
        _transform = root.transform;
        _rigidbody = root.GetComponent<Rigidbody2D>();
    }

    public override void Execute() {
        Debug.Log("Go to point : Execute");
        _rigidbody.linearVelocity = (_behavior._checkPoints[_behavior._currentCheckPointIndex] - _transform.position).normalized * _speed;

        if((_transform.position - _behavior._checkPoints[_behavior._currentCheckPointIndex]).sqrMagnitude < 0.1f * 0.1f) {
            _rigidbody.linearVelocity = Vector3.zero;
            _localStatus = Status.SUCCESS;
        }
    }
}
