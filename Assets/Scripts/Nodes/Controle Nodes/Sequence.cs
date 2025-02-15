using UnityEngine;
using UnityEngine.EventSystems;

public class Sequence : Node {
    public override void Execute() {
        foreach(Node _childnode in _childrenNode) {
            if(_childnode._status == Status.FAILURE) {
                status = Status.FAILURE;
                return;
            }
            if(_childnode._status == Status.SUCCESS) {
                continue;
            }
            if(_childnode._status == Status.RUNNING) {
                _childnode.Execute();
                return;
            }
        }
        status = Status.SUCCESS;
    }
}
