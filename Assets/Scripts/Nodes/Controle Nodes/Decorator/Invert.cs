using UnityEngine;

public class Invert : Node {
    public Invert(BaseBehaviorTree root) : base(root) { }

    public override void Execute() {
        if(_childNodes.Length > 0) {
            if(_childNodes[0]._status == Status.SUCCESS) {
                _localStatus = Status.FAILURE;
            }
            else if(_childNodes[0]._status == Status.FAILURE) {
                _localStatus = Status.SUCCESS;
            }
            else if(_childNodes[0]._status == Status.RUNNING) {
                _childNodes[0].Execute();
            }
        }
    }
}
