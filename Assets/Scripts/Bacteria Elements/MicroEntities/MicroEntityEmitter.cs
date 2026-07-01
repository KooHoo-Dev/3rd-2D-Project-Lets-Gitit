using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    public class MicroEntityEmitter : MonoBehaviour
    {
        [Header("MicroEntity/Settings")]
        [SerializeField] private Transform emitPoint;
        [SerializeField] private float emitSpeed = 3.0f;
        [SerializeField] private float emitDuration = 1.0f;
        [SerializeField] private int maxEntities = 10;
        private int currentEntities = 0;

        protected void Emit(GameObject prefab)
        {
            Emit(prefab, GetEmitDirection());
        }

        protected void Emit(GameObject prefab, Vector2 direction)
        {
            if (prefab == null) return;
            if (currentEntities >= maxEntities) return;

            GameObject spawnedObject = Instantiate(prefab, emitPoint.position, Quaternion.identity);

            MicroEntity entity = spawnedObject.GetComponent<MicroEntity>();
            entity.Init(this);

            currentEntities += 1;

            // Emit(prefab, Vector2.zero)같이 호출하는 경우를 방지
            Vector2 emitDirection = direction.sqrMagnitude > 0f
                ? direction.normalized
                : GetEmitDirection();

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