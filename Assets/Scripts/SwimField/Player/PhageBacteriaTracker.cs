using UnityEngine;

namespace SuperPhage
{
    public class PhageBacteriaTracker : MonoBehaviour
    {
        [SerializeField] private Transform trackerVisual;
        [SerializeField] private float angleOffset = -90f;

        private void Awake()
        {
            if (trackerVisual == null)
            {
                trackerVisual = transform;
            }
        }

        private void Update()
        {
            Transform nearestBacteria = FindNearestBacteria();

            if (nearestBacteria == null)
            {
                return;
            }

            LookAt(nearestBacteria.position);

        }

        private Transform FindNearestBacteria()
        {
            Transform[] bacterias = BacteriaContext.Instance.Bacterias;
            if (bacterias == null || bacterias.Length == 0) return null;

            Transform nearest = null;
            float nearestSqrDistance = float.MaxValue;
            Vector2 currentPosition = transform.position;

            foreach (Transform bacteria in bacterias)
            {
                if (bacteria == null) continue;

                float sqrDisatance = ((Vector2)bacteria.position - currentPosition).sqrMagnitude;
                
                if (sqrDisatance < nearestSqrDistance)
                {
                    nearestSqrDistance = sqrDisatance;
                    nearest = bacteria;

                    }
            }

            return nearest;
        }

        private void LookAt(Vector3 targetPosition)
        {
            Vector2 direction = targetPosition - transform.position;


            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            trackerVisual.rotation = Quaternion.Euler(0f, 0f, angle + angleOffset);
        }
    }

}

