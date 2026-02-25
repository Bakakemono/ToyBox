using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BezierCurve : MonoBehaviour
{

    [SerializeField] List<Transform> points;

    [SerializeField] int steps = 10;

    private void OnDrawGizmos() {
        Gizmos.color = Color.white;

        for(int i = 0; i < points.Count - 1; i++) {
            Gizmos.DrawLine(points[i].position, points[i + 1].position);
        }

        Vector3 preiousPos = points[0].position;
        Gizmos.color = Color.red;
        for(int i = 0; i <= steps; i++) {
            Vector3 currentTip = Vector3.Lerp(points[points.Count - 2].position, points[points.Count - 1].position, (float)i / (float)steps);
            for(int x = points.Count - 3; x >= 0; x--) {
                Vector3 newPos = Vector3.Lerp(points[x].position, currentTip, (float)i / (float)steps);
                //Gizmos.DrawLine(newPos, currentTip);
                currentTip = newPos;
            }
            Gizmos.DrawLine(preiousPos, currentTip);
            preiousPos = currentTip;
        }
    }
}
