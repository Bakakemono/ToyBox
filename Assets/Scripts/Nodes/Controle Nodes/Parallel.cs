using UnityEngine;

// Execute all child nodes in paralelle unitl N node return success or they all return failure.
public class Parallel : Node {
    public Parallel(BaseBehaviorTree root) : base(root) { }


    protected int _successNodeRequired = 0;

    public void SetSuccessfulNodeRequired(char number) {
        _successNodeRequired = number;
    }

    public override void Execute() {
        int successCount = 0;
        int failureCount = 0;

        foreach(Node node in _childNodes) {
            if(node._status == Status.SUCCESS) {
                successCount++;
                return;
            }
            if(node._status == Status.FAILURE) {
                failureCount++;
                continue;
            }
            if(node._status == Status.RUNNING) {
                node.Execute();
            }
        }
        if(successCount >= _successNodeRequired) {
            status = Status.SUCCESS;
            ResetChildNodes();
        }
        else if(failureCount >= _childNodes.Length) {
            status = Status.FAILURE;
            ResetChildNodes();
        }
    }
}