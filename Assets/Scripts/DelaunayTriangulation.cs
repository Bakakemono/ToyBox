using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;

public struct Circle {
    public Vector2[] _points;
    public Vector2 _position;
    public float _radius;
     
    public Circle(Vector2[] points, Vector2 position, float radius) {
        _points = points;
        _position = position;
        _radius = radius;
    }
}

public class DelaunayTriangulation : MonoBehaviour {
    [SerializeField] float _areaRadius = 10f;

    [SerializeField] int _pointsNumber = 10;

    [SerializeField] List<Vector2> _points;
    

    [SerializeField] float _pointSize = 0.05f;

    List<Circle> _circleList = new List<Circle>();

    [Header("Debug")]
    [SerializeField] bool _regeneratePoints = false;
    [SerializeField] bool _generateCircles = false;
    [SerializeField] float _circlePerSecondGeneration = 10f;

    private void Start() {
        GenereatePoints();
    }

    void Update() {
        if(_regeneratePoints) {
            GenereatePoints();
            _regeneratePoints = false;
        }
        if(_generateCircles) {
            _circleList = new List<Circle>();
            //GenerateTrianglesCircle();
            StartCoroutine(EC_Triangulate());
            _generateCircles = false;
        }
    }

    void GenereatePoints() {
        _points = new List<Vector2>();
        
        // Generate List of points
        for(int i = 0; i < _pointsNumber; i++) {
            _points.Add(new Vector2(UnityEngine.Random.Range(-_areaRadius, _areaRadius), UnityEngine.Random.Range(-_areaRadius, _areaRadius)));
        }

        List<Vector2> sortedPoints = new List<Vector2>();

        // Select top left as point reference point
        Vector2 originPoint = new Vector2(-_areaRadius, _areaRadius);

        // Do not stop until all points have been checks
        while(_points.Count > 0){
            float smallestLengthFound = (_points[0] - originPoint).sqrMagnitude;
            int currentIndex = 0;
            for(int i = 1; i < _points.Count; i++) {
                float currentLength = (_points[i] - originPoint).sqrMagnitude;
                if(currentLength < smallestLengthFound) {
                    smallestLengthFound = currentIndex;
                    currentIndex = i;
                }
            }
            sortedPoints.Add(_points[currentIndex]);
            _points.RemoveAt(currentIndex);
        }
        _points = sortedPoints;
    }

    void GenerateTrianglesCircle() {
        for(int x = 0; x < _pointsNumber; x++) {
            for(int y = 0; y < _pointsNumber; y++) {
                if(y <= x)
                    continue;
                for(int z = 0; z < _pointsNumber; z++) {
                    if(z <= y)
                        continue;

                    Vector2 A = _points[x];
                    Vector2 B = _points[y];
                    Vector2 C = _points[z];

                    float a1 = B.x - A.x;
                    float b1 = B.y - A.y;
                    float c1 = (B.x * B.x - A.x * A.x + B.y * B.y - A.y * A.y) / 2f;

                    float a2 = C.x - A.x;
                    float b2 = C.y - A.y;
                    float c2 = (C.x * C.x - A.x * A.x + C.y * C.y - A.y * A.y) / 2f;

                    float denominator = a1 * b2 - a2 * b1;

                    if(denominator == 0) {
                        continue;
                    }

                    float h = (c1 * b2 - c2 * b1) / denominator;
                    float k = (a1 * c2 - a2 * c1) / denominator;

                    Vector2 circlePos = new Vector2(h, k);

                    float r = Mathf.Sqrt((A.x - h) * (A.x - h) + (A.y - k) * (A.y - k));
                    float sqrR = r * r;

                    Vector2[] points = new Vector2[3];
                    points[0] = _points[x];
                    points[1] = _points[y];
                    points[2] = _points[z];

                    bool addCircle = true;
                    foreach(Vector2 point in _points) {
                        if(points.Contains<Vector2>(point)) {
                            continue;
                        }
                        if((point - circlePos).sqrMagnitude < sqrR) {
                            addCircle = false;
                        }
                    }

                    if(addCircle) {
                        _circleList.Add(new Circle(points, circlePos, r));
                    }
                }
            }
        }
    }

    IEnumerator EC_Triangulate() {
        for(int x = 0; x < _pointsNumber; x++) {
            for(int y = 0; y < _pointsNumber; y++) {
                if(y <= x)
                    continue;
                for(int z = 0; z < _pointsNumber; z++) {
                    if(z <= y)
                        continue;

                    Vector2 A = _points[x];
                    Vector2 B = _points[y];
                    Vector2 C = _points[z];

                    float a1 = B.x - A.x;
                    float b1 = B.y - A.y;
                    float c1 = (B.x * B.x - A.x * A.x + B.y * B.y - A.y * A.y) / 2f;

                    float a2 = C.x - A.x;
                    float b2 = C.y - A.y;
                    float c2 = (C.x * C.x - A.x * A.x + C.y * C.y - A.y * A.y) / 2f;

                    float denominator = a1 * b2 - a2 * b1;

                    if(denominator == 0) {
                        continue;
                    }

                    float h = (c1 * b2 - c2 * b1) / denominator;
                    float k = (a1 * c2 - a2 * c1) / denominator;

                    Vector2 circlePos = new Vector2(h, k);

                    float r = Mathf.Sqrt((A.x - h) * (A.x - h) + (A.y - k) * (A.y - k));
                    float sqrR = r * r;

                    Vector2[] points = new Vector2[3];
                    points[0] = _points[x];
                    points[1] = _points[y];
                    points[2] = _points[z];

                    bool addCircle = true;
                    foreach(Vector2 point in _points) {
                        if(points.Contains<Vector2>(point)) {
                            continue;
                        }
                        if((point - circlePos).sqrMagnitude < sqrR) {
                            addCircle = false;
                        }
                    }

                    if(addCircle) {
                        _circleList.Add(new Circle(points, circlePos, r));
                        yield return new WaitForSeconds(1f / _circlePerSecondGeneration);
                    }
                }
            }
        }

    }

    private void OnDrawGizmos() {
        //Vector2 originPoint = new Vector2(-_areaRadius, _areaRadius);
        //float maxLength = (originPoint  - new Vector2(_areaRadius, _areaRadius)).sqrMagnitude;
        //foreach(var point in _points) {
        //    float redValue = (originPoint - point).sqrMagnitude / maxLength;
        //    Gizmos.color = new UnityEngine.Color(1, 1 - redValue, 1 - redValue, 1);

        //    Gizmos.DrawSphere(point, _pointSize);
        //}

        for(int i = 0; i < _points.Count; i++) {
            Gizmos.color = new UnityEngine.Color(1f, 1f - ((float)i / (float)_points.Count), 1f - ((float)i / (float)_points.Count), 1);

            Gizmos.DrawSphere(_points[i], _pointSize);
        }

        //foreach(var circle in _circleList) {
        //    Gizmos.color = UnityEngine.Color.red;
        //    Gizmos.DrawWireSphere(circle._position, circle._radius);
        //}

        foreach(var circle in _circleList) {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawLine(circle._points[0], circle._points[1]);
            Gizmos.DrawLine(circle._points[1], circle._points[2]);
            Gizmos.DrawLine(circle._points[2], circle._points[0]);
        }
    }
}
