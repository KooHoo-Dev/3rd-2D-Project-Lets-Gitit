using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    public class MicroEntityEmitter : MonoBehaviour
    {
        [Header("MicroEntity/Settings")]
        [SerializeField] private GameObject[] emitPrefabs;
        [SerializeField] private Transform emitPoint;
        [SerializeField] private float emitSpeed = 3.0f;
        [SerializeField] private float emitDuration = 1.0f;
        [SerializeField] private int maxEntities = 10;
        private int currentEntities = 0;

        public void EmitRandom()
        {
            if (emitPrefabs == null || emitPrefabs.Length == 0) return;
            if (currentEntities >= maxEntities) return;

            int randIdx = Random.Range(0, emitPrefabs.Length);
            Emit(emitPrefabs[randIdx]);
        }

        protected void Emit(GameObject prefab)
        {
            if (prefab == null) return;

            GameObject spawnedObject = Instantiate(prefab, emitPoint.position, Quaternion.identity);
            MicroEntity entity = spawnedObject.GetComponent<MicroEntity>();
            entity.Init(this);
            currentEntities += 1;

            Vector2 emitDirection = GetEmitDirection();
            StartCoroutine(Eject(spawnedObject.transform, emitDirection));
        }

        private Vector2 GetEmitDirection()
        {
            float targetAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;

            return new Vector2(Mathf.Cos(targetAngle), Mathf.Sin(targetAngle));
        }

        private IEnumerator Eject(Transform target, Vector2 direction)
        {
            float elapsedTime = 0;

            float ejectSpeed = emitSpeed + Random.Range(-1f, 1f);

            while (elapsedTime < emitDuration)
            {
                if (target == null) yield break;

                elapsedTime += Time.deltaTime;

                float progress = Mathf.Clamp01(elapsedTime / emitDuration);
                float currentSpeed = Mathf.Lerp(ejectSpeed, 0f, progress);

                target.position += (Vector3)(direction * currentSpeed * Time.deltaTime);

                yield return null;
            }
        }

        public void DecreaseCount()
        {
            currentEntities -= 1;
        }
    }
}