using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnZone : MonoBehaviour
{
    [Header("Enemy setup")]
    [SerializeField] private GameObject _enemyPrefab;   // Prefab del enemigo con NavMeshAgent
    [SerializeField] private int _enemyCount = 3;
    [SerializeField] private Transform[] _spawnPoints;  // Puntos de spawn en la zona inicial

    private bool _hasSpawned = false;
    private readonly List<GameObject> _activeEnemies = new List<GameObject>();

    // Referencia estatica para que EnemyKillZone pueda acceder
    public static EnemySpawnZone Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasSpawned) return;
        if (!other.CompareTag("Player")) return;

        _hasSpawned = true;
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < _enemyCount; i++)
        {
            Transform spawnPoint = _spawnPoints[i % _spawnPoints.Length];
            GameObject enemy = Instantiate(_enemyPrefab, spawnPoint.position, Quaternion.identity);
            _activeEnemies.Add(enemy);
        }
        Debug.Log($"{_enemyCount} enemigos spawneados.");
    }

    /// <summary>
    /// Llamado por EnemyKillZone al salir del laberinto
    /// </summary>
    public void KillAllEnemies()
    {
        foreach (GameObject enemy in _activeEnemies)
        {
            if (enemy != null)
                Destroy(enemy);
        }
        _activeEnemies.Clear();
        _hasSpawned = false; // Resetea por si se genera un nuevo nivel
        Debug.Log("Todos los enemigos eliminados.");
    }
}
