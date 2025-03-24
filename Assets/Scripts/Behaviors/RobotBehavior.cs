using UnityEditor;
using UnityEngine;

public class RobotBehavior : BaseBehaviorTree {
    Transform _transform;
    Rigidbody _rigidbody;
    Sequence _tree;

    public Vector3[] _checkPoints;
    private int _nmbCheckPoint = 10;
    public int _currentCheckPointIndex = -1;

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
        _tree.AddNode(searchEnemy);

        SelectNextPoint selectNextPoint = new SelectNextPoint(this);
    }
}
