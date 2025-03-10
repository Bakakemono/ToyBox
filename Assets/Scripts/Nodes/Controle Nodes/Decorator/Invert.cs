using UnityEngine;

public class Invert : Node {
    public Invert(BaseBehaviorTree root) : base(root) { }

    public override void Execute() {
        if(_childNodes.Length > 0) {
            if(_childNodes[0]._status == Status.SUCCESS) {
                status = Status.FAILURE;
            }
            else if(_childNodes[0]._status == Status.FAILURE) {
                status = Status.SUCCESS;
            }
            else if(_childNodes[0]._status == Status.RUNNING) {
                _childNodes[0].Execute();
            }
        }
    }
}
