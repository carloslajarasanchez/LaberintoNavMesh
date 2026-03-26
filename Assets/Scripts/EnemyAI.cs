using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

/// <summary>
/// Ponlo en el prefab del enemigo junto con un NavMeshAgent.
/// El enemigo sigue al jugador continuamente.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float _updateInterval = 0.2f; // Cada cuanto recalcula la ruta (optimizacion)

    private NavMeshAgent _agent;
    private Transform _player;
    private float _timer;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        // Busca el jugador por tag
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
            _player = playerObj.transform;
        else
            Debug.LogWarning("EnemyAI: No se encontro un GameObject con tag 'Player'.");
    }

    private void Update()
    {
        if (_player == null) return;

        _timer += Time.deltaTime;
        if (_timer >= _updateInterval)
        {
            _timer = 0f;
            _agent.SetDestination(_player.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
