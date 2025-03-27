using System.IO;
using UnityEngine;

public class Full360 : Node {
    Transform _transform;
    public Full360(BaseBehaviorTree root) : base(root) {
        _transform = root.transform;
    }
    float _rotation = 0f;

    // Degree per seconds
    float _rotationSpeed = 120f;

    public override void Execute() {
        _rotation += _rotationSpeed * Time.deltaTime;
        _transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, _rotation));

        if(_rotation >= 360f) {
            _transform.rotation = Quaternion.identity;
            _rotation = 0f;
            _localStatus = Status.SUCCESS;
        }
    }
}