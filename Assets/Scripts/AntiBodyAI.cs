using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    public class AntiBodyAI : MonoBehaviour
    {
        [SerializeField] private float detectDistance = 5.0f;
        [SerializeField] private Transform Target;
        [SerializeField] private SwimMotor swimMotor;

        private Coroutine detectRutine;
        private float detectInterval = 0.5f;
        bool isChasing = false;

        private void Update()
        {
            if (isChasing)
            {
                Vector2 directionToTarget = Target.position - transform.position;
                swimMotor.SetGoalDirection(directionToTarget.normalized);
            }
            else
            {
                swimMotor.Brake();
            }
        }

        private IEnumerator DetectPhage()
        {
            while (true) 
            {
                Vector3 offset = transform.position - Target.transform.position;
                isChasing = offset.sqrMagnitude <= (detectDistance * detectDistance);
                
                yield return new WaitForSeconds(detectInterval);
            }
        }

        private void OnEnable()
        {
            detectRutine = StartCoroutine(DetectPhage());
        }

        private void OnDisable()
        {
            if (detectRutine != null)
            {
                StopCoroutine(detectRutine);
                detectRutine = null;
            }
        }
    }
}

