using UnityEngine;

namespace SuperPhage
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SwimMotor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 1.0f;
        private Vector2 currentDirection;
        private Vector2 goalDirection;
        private float currentAngle;
        private float currentSpeed;
        private Rigidbody2D rb;

        private const float angleOffset = -90f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentAngle = rb.rotation - angleOffset;
            currentSpeed = 0f;
        }

        private void FixedUpdate()
        {
            bool hasInput = goalDirection.sqrMagnitude > 0.001;

            if (hasInput)
            {
                Debug.Log("if");
                float targetAngle = Mathf.Atan2(goalDirection.y, goalDirection.x) * Mathf.Rad2Deg;
                currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
                currentSpeed = moveSpeed;
            }
            else
            {
                Debug.Log($"{currentSpeed}");
                currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, 5f * Time.fixedDeltaTime);
            }

            float currentRadian = currentAngle * Mathf.Deg2Rad;
            currentDirection.x = Mathf.Cos(currentRadian);
            currentDirection.y = Mathf.Sin(currentRadian);

            Vector2 movePosition = rb.position + (currentDirection * currentSpeed * Time.fixedDeltaTime);

            rb.MovePosition(movePosition);
            rb.MoveRotation(currentAngle + angleOffset);
        }

        public void SetGoalDirection(Vector2 dir)
        {
            goalDirection = dir;
        }
    }

}