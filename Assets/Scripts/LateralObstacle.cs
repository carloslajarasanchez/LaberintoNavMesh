using UnityEngine;

public class LateralObstacle : MonoBehaviour
{
    [SerializeField] private float _moveDistance = 3f;  // Distancia total de desplazamiento lateral
    [SerializeField] private float _speed = 2f;         // Velocidad de movimiento

    private Vector3 _startPosition;
    private float _direction = 1f;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        // Movimiento lateral en el eje X
        transform.position += Vector3.right * _direction * _speed * Time.deltaTime;

        float offset = transform.position.x - _startPosition.x;

        if (offset >= _moveDistance / 2f)
        {
            _direction = -1f;
        }
        else if (offset <= -_moveDistance / 2f)
        {
            _direction = 1f;
        }
    }
}
