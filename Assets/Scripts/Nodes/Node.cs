using NUnit.Framework;
using UnityEngine;

public abstract class Node {
    public enum Status {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    protected Status _localStatus = Status.RUNNING;
    public Status _status {
        get { return _localStatus; }
    }

    protected Node[] _childNodes;
    protected int _numberOfNode = 0;
    protected BaseBehaviorTree _root;

    //public Node(int successNodeRequired = 0, params Node[] nodes) {
    //    _successNodeRequired = successNodeRequired;
    //    _childrenNode = nodes;
    //}

    public Node(BaseBehaviorTree root) {
        _childNodes = new Node[char.MaxValue];
        _root = root;
    }

    // To remember
    //public virtual void RegisterComponent(params Component[] components) { }

    public void AddNode(Node newNode) {
        _childNodes[_numberOfNode] = newNode;
        _numberOfNode++;
    }

    public abstract void Execute();

    public virtual void ResetNode() {
        _localStatus = Status.RUNNING;
    }
    public void ResetChildNodes() {
        for(int i = 0; i < _numberOfNode; i++) {
            _childNodes[i].ResetNode();
        }
    }
}
