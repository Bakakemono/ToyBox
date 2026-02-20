using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{

    List<Transform> points;

    int steps = 10;

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;

        for(int i = 0; i < points.Count - 1; i++) {
            Gizmos.DrawLine(points[i - 1].position, points[i].position);
        }

        Vector3 previousPos = points[0].position;
        for(int i = 0; i <= steps; i++) {
            Vector3 currentTip;
            for(int x = points.Count - 2; x >= 0; x--) {
                
            }
        }
    }
}
