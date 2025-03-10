using NUnit.Framework;
using UnityEngine;

public abstract class Node {
    public enum Status {
        SUCCESS,
        FAILURE,
        RUNNING
    }

    protected Status status = Status.RUNNING;
    public Status _status {
        get { return status; }
    }

    protected Node[] _childNodes;
    protected int _nextIndexUse = 0;
    BaseBehaviorTree _root;

    //public Node(int successNodeRequired = 0, params Node[] nodes) {
    //    _successNodeRequired = successNodeRequired;
    //    _childrenNode = nodes;
    //}

    public Node(BaseBehaviorTree root) {
        _childNodes = new Node[char.MaxValue];
        _root = root;
    }

    public virtual void RegisterComponent() { }

    public void AddNode(Node newNode) {
        _childNodes[_nextIndexUse] = newNode;
    }

    public abstract void Execute();

    public virtual void ResetNode() {
        status = Status.RUNNING;
    }
    public void ResetChildNodes() {
        foreach(Node resetingNode in _childNodes) {
            resetingNode.ResetNode();
        }
    }
}
