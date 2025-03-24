using System;
using Unity.Mathematics;
using UnityEngine;

public class SelectNextPoint : Node {
    public SelectNextPoint(BaseBehaviorTree root) : base(root) {}

    public override void Execute() {
        RobotBehavior robotBehavior = (RobotBehavior)_root;
        robotBehavior._currentCheckPointIndex = (robotBehavior._currentCheckPointIndex + 1) % robotBehavior._checkPoints.Length;
    }
}