using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    public class RandomAreaSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject spawnPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private Vector2 areaSize = new Vector2(10f, 6f);
        [SerializeField] private float minDistance = 1.0f;
        [SerializeField] private int maxSpawnCount = 10;
        [SerializeField] private int maxAttemps = 30;
        [SerializeField] private float spawnInterval = 1.0f;
        [SerializeField] private LayerMask blockingLayer;

        private int currentSpawnCount = 0;

        private void Start ()
        {
            StartCoroutine(SpawnLoop());
        }

        private IEnumerator SpawnLoop()
        {
            while(true)
            {
                TrySpawn();

                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void TrySpawn()
        {
            if (spawnPrefab == null) return;
            if (TryGetSapwnPosition(out Vector2 spawnPoint) == false) return;
            if (currentSpawnCount >= maxSpawnCount) return;

            GameObject spawnedObject = Instantiate(spawnPrefab, spawnPoint, Quaternion.identity);
            spawnedObject.transform.SetParent(parent);

            if (spawnedObject.TryGetComponent(out MicroEntity entity))
            {
                entity.RegisterSpawner(this);
            }

            currentSpawnCount += 1;
        }

        private bool TryGetSapwnPosition(out Vector2 position)
        {
            for (int i = 0; i < maxAttemps; i++)
            {
                position = GetRandomPointInArea();
                bool blocked = Physics2D.OverlapCircle(position, minDistance, blockingLayer);

                if (blocked == false)
                {
                    return true;
                }
            }

            position = default;
            return false;
        }

        private Vector2 GetRandomPointInArea()
        {
            float x = Random.Range(-areaSize.x * 0.5f, areaSize.x * 0.5f);
            float y = Random.Range(-areaSize.y * 0.5f, areaSize.y * 0.5f);
            
            Vector2 point = (Vector2)transform.position + new Vector2(x, y);

            return point;
        }

        public void DecreaseCount()
        {
            currentSpawnCount -= 1;
        }
    }
}