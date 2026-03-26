using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset = new Vector3(0f, 15f, -4f);
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private float _pitchAngle = 75f; // 90 = totalmente vertical, 45 = lateral

    private void LateUpdate()
    {
        if (_target == null) return;

        Vector3 desiredPosition = new Vector3(
            transform.position.x,
            _target.position.y + _offset.y,
            _target.position.z + _offset.z
        );

        transform.position = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_pitchAngle, 0f, 0f);
    }
}