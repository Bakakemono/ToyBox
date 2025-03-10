using UnityEngine;

public class RobotBehavior : BaseBehaviorTree {

    Sequence _tree;

    private void Awake() {
        _tree = new Sequence(this);
    }

    private void Update() {
        
    }
}
