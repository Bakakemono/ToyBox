using UnityEngine;

public class Fallback : Node {
    public override void Execute() {
        foreach(Node _childnode in _childrenNode) {
            if(_childnode._status == Status.SUCCESS) {
                status = Status.SUCCESS;
                return;
            }
            if(_childnode._status == Status.FAILURE) {
                continue;
            }
            if(_childnode._status == Status.RUNNING) {
                _childnode.Execute();
                return;
            }
        }
        status = Status.FAILURE;
    }
}
