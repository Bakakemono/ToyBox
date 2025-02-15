using UnityEngine;

public class Parallel : Node {

    protected int _successNodeRequired = 0;

    public void SetSuccessfulNodeRequired(char number) {
        _successNodeRequired = number;
    }

    public override void Execute() {
        int successCount = 0;
        int failureCount = 0;

        foreach(Node _childnode in _childrenNode) {
            if(_childnode._status == Status.SUCCESS) {
                successCount++;
                return;
            }
            if(_childnode._status == Status.FAILURE) {
                failureCount++;
                continue;
            }
            if(_childnode._status == Status.RUNNING) {
                _childnode.Execute();
            }
        }
        if(successCount >= _successNodeRequired) {
            status = Status.SUCCESS;
        }
        else if(failureCount >= _childrenNode.Length) {
            status = Status.FAILURE;
        }
    }
}