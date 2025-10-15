using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnPoint : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] monsterPrefabs; // можливі префаби монстрів
    public float spawnRadius = 5f;      // радіус навколо точки
    public int maxAlive = 5;            // макс одночасно живих
    public int initialSpawn = 2;        // скільки спавнити на старті
    public float spawnInterval = 3f;    // інтервал перевірки/спавну
    public float respawnDelay = 10f;    // час до респавну після смерті одного монстра

    [Header("Behavior")]
    public bool spawnOnStart = true;
    public bool randomizeRotation = true;

    // внутрішні
    private List<GameObject> aliveMonsters = new List<GameObject>();
    private Coroutine spawnRoutine;

    // ================= API ==================
    void Start()
    {
        if (spawnOnStart)
        {
            // початковий спавн
            for (int i = 0; i < initialSpawn; i++)
            {
                TrySpawnOne();
            }
            // починаємо рутин
            spawnRoutine = StartCoroutine(SpawnLoop());
        }
    }

    public void StartSpawning()
    {
        if (spawnRoutine == null)
            spawnRoutine = StartCoroutine(SpawnLoop());
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }

    public void ForceSpawnNow(int count = 1)
    {
        for (int i = 0; i < count; i++)
            TrySpawnOne();
    }

    IEnumerator SpawnLoop()
    {
        while (true)
        {
            // Чистимо null-посилання (об'єкт може бути знищений без сповіщення)
            aliveMonsters.RemoveAll(x => x == null);

            if (aliveMonsters.Count < maxAlive)
            {
                TrySpawnOne();
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    bool TrySpawnOne()
    {
        if (monsterPrefabs == null || monsterPrefabs.Length == 0) return false;
        if (aliveMonsters.Count >= maxAlive) return false;

        // Вибираємо випадковий префаб
        GameObject prefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        if (prefab == null) return false;

        Vector3 pos;
        if (!TryGetRandomPoint(out pos)) return false;

        GameObject go = Instantiate(prefab, pos, Quaternion.identity);
        if (randomizeRotation) go.transform.rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

        // Додаємо маленький маркер, щоб повідомляти спавнер при знищенні
        SpawnedMarker marker = go.AddComponent<SpawnedMarker>();
        marker.owner = this;

        aliveMonsters.Add(go);
        return true;
    }

    bool TryGetRandomPoint(out Vector3 result)
    {
        // Псевдо-рандомна точка в колі на поверхні
        for (int i = 0; i < 12; i++)
        {
            Vector2 rnd = Random.insideUnitCircle * spawnRadius;
            Vector3 candidate = transform.position + new Vector3(rnd.x, 0f, rnd.y);

            // каст вниз з висоти щоб знайти поверхню
            RaycastHit hit;
            if (Physics.Raycast(candidate + Vector3.up * 5f, Vector3.down, out hit, 10f))
            {
                result = hit.point;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    // Викликається маркером при OnDestroy монстра
    internal void NotifyMonsterDestroyed(GameObject monster)
    {
        // видаляємо посилання
        aliveMonsters.Remove(monster);
        // починаємо корутину респавну для цього слоту
        StartCoroutine(RespawnAfter(respawnDelay));
    }

    IEnumerator RespawnAfter(float delay)
    {
        yield return new WaitForSeconds(delay);
        // перевіряємо ще раз кількість
        aliveMonsters.RemoveAll(x => x == null);
        if (aliveMonsters.Count < maxAlive)
        {
            TrySpawnOne();
        }
    }

    // Gizmo для візуалізації області
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.2f, 0.2f, 0.25f);
        Gizmos.DrawSphere(transform.position, 0.25f);

        Gizmos.color = new Color(0f, 0.6f, 1f, 0.12f);
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }

    // Допоміжний компонент-інформер, прикріплюється до кожного згенерованого монстра
    public class SpawnedMarker : MonoBehaviour
    {
        [System.NonSerialized]
        public MonsterSpawnPoint owner;

        void OnDestroy()
        {
            if (owner != null)
            {
                owner.NotifyMonsterDestroyed(this.gameObject);
            }
        }
    }
}
