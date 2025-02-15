using UnityEngine;

public class Invert : Node {
    public override void Execute() {
        if(_childrenNode.Length > 0) {
            if(_childrenNode[0]._status == Status.SUCCESS) {
                status = Status.FAILURE;
            }
            else if(_childrenNode[0]._status == Status.FAILURE) {
                status = Status.SUCCESS;
            }
            else if(_childrenNode[0]._status == Status.RUNNING) {
                _childrenNode[0].Execute();
            }
        }
    }
}
