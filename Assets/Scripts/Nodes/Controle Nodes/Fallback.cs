using UnityEditor.Experimental.GraphView;
using UnityEngine;

// Execute all child nodes until one return success or all return failure.
public class Fallback : Node {
    public Fallback(BaseBehaviorTree root) : base(root) { }

    public override void Execute() {
        for(int i = 0; i < _numberOfNode; i++) {
            if(_childNodes[i]._status == Status.SUCCESS) {
                _localStatus = Status.SUCCESS;
                ResetChildNodes();
                return;
            }
            if(_childNodes[i]._status == Status.FAILURE) {
                continue;
            }
            if(_childNodes[i]._status == Status.RUNNING) {
                _childNodes[i].Execute();
                return;
            }
        }
        _localStatus = Status.FAILURE;
        ResetChildNodes();
    }
}
