using UnityEngine;

public class GoToPoint : Node {
    Transform _transform;
    Rigidbody _rigidbody;
    RobotBehavior _behavior;

    float _speed = 2f;

    public GoToPoint(BaseBehaviorTree root) : base(root) {
        _behavior = (RobotBehavior)root;
        _transform = root.transform;
        _rigidbody = root.GetComponent<Rigidbody>();
    }

    public override void Execute() {
        _rigidbody.linearVelocity = (_behavior._checkPoints[_behavior._currentCheckPointIndex] - _transform.position).normalized * _speed;

        if((_transform.position - _behavior._checkPoints[_behavior._currentCheckPointIndex]).sqrMagnitude < 0.1f * 0.1f) {
            _rigidbody.linearVelocity = Vector3.zero;
            status = Status.SUCCESS;
        }
    }
}
