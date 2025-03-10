using UnityEngine;
using UnityEngine.EventSystems;


// Execute all child nodes until they all return success or one return failure.
public class Sequence : Node {
    public Sequence(BaseBehaviorTree root) : base(root) {}

    public override void Execute() {
        foreach(Node childnode in _childNodes) {
            if(childnode._status == Status.FAILURE) {
                status = Status.FAILURE;
                ResetChildNodes();
                return;
            }
            if(childnode._status == Status.SUCCESS) {
                continue;
            }
            if(childnode._status == Status.RUNNING) {
                childnode.Execute();
                return;
            }
        }
        status = Status.SUCCESS;
        ResetChildNodes();
    }
}
