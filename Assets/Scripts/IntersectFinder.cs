using UnityEngine;

public class IntersectFinder : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Transform _player;
    [SerializeField] Transform _plane;

    Vector3 _intersection = Vector3.zero;
    public bool LinePlaneIntersection(
        Vector3 L0, Vector3 d,
        Vector3 P0, Vector3 n,
        out Vector3 intersection) {
        intersection = Vector3.zero;

        float denom = Vector3.Dot(n, d);

        // If denom is close to 0 → line parallel to plane
        if(Mathf.Abs(denom) < 0.001f)
            return false;

        float t = Vector3.Dot(n, (P0 - L0)) / denom;
        intersection = L0 + t * d;
        return true;
    }

    private void FixedUpdate() {
        if(LinePlaneIntersection(_camera.position, _player.position - _camera.position, _plane.position, -_plane.forward, out _intersection)) {
            _intersection = Vector3.zero;
        }
    }

    private void OnDrawGizmos() {
        if(_camera == null || _player == null) return;

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(_intersection, 0.1f);
    }
}
