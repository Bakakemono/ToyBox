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

    protected Node[] _childrenNode;
    protected int _nextIndexUse = 0;

    //public Node(int successNodeRequired = 0, params Node[] nodes) {
    //    _successNodeRequired = successNodeRequired;
    //    _childrenNode = nodes;
    //}

    public Node() {
        _childrenNode = new Node[char.MaxValue];
    }

    public void AddNode(Node newNode) {
        _childrenNode[_nextIndexUse] = newNode;
    }

    public abstract void Execute();
}
