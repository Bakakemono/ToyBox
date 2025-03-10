using UnityEngine;

// Execute all child nodes until one return success or all return failure.
public class Fallback : Node {
    public Fallback(BaseBehaviorTree root) : base(root) { }

    public override void Execute() {
        foreach(Node node in _childNodes) {
            if(node._status == Status.SUCCESS) {
                status = Status.SUCCESS;
                ResetChildNodes();
                return;
            }
            if(node._status == Status.FAILURE) {
                continue;
            }
            if(node._status == Status.RUNNING) {
                node.Execute();
                return;
            }
        }
        status = Status.FAILURE;
        ResetChildNodes();
    }
}
