using UnityEditor;
using UnityEngine;

public class RobotBehavior : BaseBehaviorTree {
    Transform _transform;
    Rigidbody _rigidbody;
    Sequence _tree;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        SetupBehaviorTree();
    }

    private void Update() {
        
    }

    void SetupBehaviorTree() {
        _tree = new Sequence(this);
        

        Sequence searchEnemy = new Sequence(this);
        
        
    }
}
