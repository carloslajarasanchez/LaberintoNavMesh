using UnityEngine;

public class EnemyKillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (EnemySpawnZone.Instance != null)
            EnemySpawnZone.Instance.KillAllEnemies();
    }
}
