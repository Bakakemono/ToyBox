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

        for(int i = 0; i < _numberOfNode; i++) {
            if(_childNodes[i]._status == Status.SUCCESS) {
                successCount++;
                return;
            }
            if(_childNodes[i]._status == Status.FAILURE) {
                failureCount++;
                continue;
            }
            if(_childNodes[i]._status == Status.RUNNING) {
                _childNodes[i].Execute();
            }
        }

        if(successCount >= _successNodeRequired) {
            _localStatus = Status.SUCCESS;
            ResetChildNodes();
        }
        else if(failureCount >= _childNodes.Length) {
            _localStatus = Status.FAILURE;
            ResetChildNodes();
        }
    }
}