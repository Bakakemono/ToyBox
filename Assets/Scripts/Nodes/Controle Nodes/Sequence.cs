using UnityEngine;
using UnityEngine.EventSystems;


// Execute all child nodes until they all return success or one return failure.
public class Sequence : Node {
    public Sequence(BaseBehaviorTree root) : base(root) {}

    public override void Execute() {
        for(int i = 0; i < _numberOfNode; i++) {
            if(_childNodes[i]._status == Status.FAILURE) {
                _localStatus = Status.FAILURE;
                ResetChildNodes();
                return;
            }
            if(_childNodes[i]._status == Status.SUCCESS) {
                continue;
            }
            if(_childNodes[i]._status == Status.RUNNING) {
                _childNodes[i].Execute();
                return;
            }
        }
        _localStatus = Status.SUCCESS;
        ResetChildNodes();
    }
}
