using UnityEngine;

public class IntersectFinder : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] Transform _player;
    [SerializeField] Transform _plane;

    [SerializeField] Transform _objectToMove;

    Vector3 _intersection = Vector3.zero;
    public bool LinePlaneIntersection(
        Vector3 origin, Vector3 direction,
        Vector3 PlanePosition, Vector3 planeUp,
        out Vector3 intersection) {
        intersection = Vector3.zero;

        float denom = Vector3.Dot(planeUp, direction);

        // If denom is close to 0 → line parallel to plane
        if(Mathf.Abs(denom) < 0.001f)
            return false;

        float t = Vector3.Dot(planeUp, (PlanePosition - origin)) / denom;
        intersection = origin + t * direction;
        return true;
    }

    private void FixedUpdate() {
        if(!LinePlaneIntersection(_camera.position, _player.position - _camera.position, _plane.position, -_plane.forward, out _intersection)) {
            _intersection = Vector3.zero;
        }
        else {
            _objectToMove.position = _intersection;
        }
    }

    private void OnDrawGizmos() {
        if(_camera == null || _player == null) return;

        Gizmos.color = Color.black;
        Gizmos.DrawSphere(_intersection, 0.1f);
    }
}
