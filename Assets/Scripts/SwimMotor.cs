using UnityEngine;

namespace SuperPhage
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SwimMotor : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 5.0f;
        [SerializeField] private float rotationSpeed = 180.0f;
        [SerializeField] private float deceleration = 5.0f;
        private Rigidbody2D rb;
        private Vector2 currentDirection;
        private Vector2 goalDirection;
        private float currentAngle;
        private float currentSpeed;
        private bool hasGoalDirection = false;

        private const float angleOffset = -90f;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            currentAngle = rb.rotation - angleOffset;
        }

        private void FixedUpdate()
        {
            if (hasGoalDirection)
            {
                float targetAngle = Mathf.Atan2(goalDirection.y, goalDirection.x) * Mathf.Rad2Deg;
                currentAngle = Mathf.MoveTowardsAngle(currentAngle, targetAngle, rotationSpeed * Time.fixedDeltaTime);
            }

            float currentRadian = currentAngle * Mathf.Deg2Rad;
            currentDirection.x = Mathf.Cos(currentRadian);
            currentDirection.y = Mathf.Sin(currentRadian);

            Vector2 movePosition = rb.position + (currentDirection * currentSpeed * Time.fixedDeltaTime);
            
            rb.MovePosition(movePosition);
            rb.MoveRotation(currentAngle + angleOffset);

            currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, deceleration * Time.fixedDeltaTime);
        }

        public void SetGoalDirection(Vector2 dir)
        {
            goalDirection = dir.normalized;
            currentSpeed = moveSpeed;
            hasGoalDirection = true;
        }

        public void Brake()
        {
            hasGoalDirection = false;
        }

        /// <summary>
        /// If speed is under 0, set moveSpeed to 0
        /// </summary>
        /// <param name="speed"></param>
        public void SetMoveSpeed(float speed)
        {
            moveSpeed = Mathf.Max(0f, speed);
        }
    }

}