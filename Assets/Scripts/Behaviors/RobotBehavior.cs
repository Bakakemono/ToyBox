using UnityEditor;
using UnityEngine;

public class RobotBehavior : BaseBehaviorTree {
    Transform _transform;
    Rigidbody2D _rigidbody;
    Sequence _tree;

    [SerializeField] public Vector3[] _checkPoints = new Vector3[10];
    [SerializeField] private int _nmbCheckPoint = 10;
    public int _currentCheckPointIndex = -1;

    private void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        SetupBehaviorTree();

        _checkPoints = new Vector3[_nmbCheckPoint];

        for(int i = 0; i < _nmbCheckPoint; i++) {
            _checkPoints[i] = new Vector3(Random.Range(-20f, 20f), Random.Range(-20f, 20f), 0f);
        }
    }

    private void Update() {
        _tree.Execute();
    }

    void SetupBehaviorTree() {
        _tree = new Sequence(this);

        Sequence searchEnemy = new Sequence(this);
        _tree.AddNode(searchEnemy);

        SelectNextPoint selectNextPoint = new SelectNextPoint(this);
        searchEnemy.AddNode(selectNextPoint);

        GoToPoint goToPoint = new GoToPoint(this);
        searchEnemy.AddNode(goToPoint);

        Full360 full360 = new Full360(this);
        searchEnemy.AddNode(full360);
    }

    private void OnDrawGizmos() {
        foreach(Vector3 pos in _checkPoints) {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pos, 0.1f);
        }
    }
}
