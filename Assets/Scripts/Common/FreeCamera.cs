using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SuperPhage
{
    public class FreeCamera : MonoBehaviour
    {
        [SerializeField] private InputActionReference pointActionReference;

        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float lerpSpeed = 8f;
        [SerializeField] private float edgeSize = 32f;

        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        private Vector3 targetPosition;

        private void Awake()
        {
            targetPosition = transform.position;
        }

        private void Update()
        {
            Vector2 pointerPosition = pointActionReference.action.ReadValue<Vector2>();
            Vector2 moveDirection = GetEdgeMoveDirection(pointerPosition);

            if (moveDirection.sqrMagnitude > 0f)
            {
                targetPosition += (Vector3)(moveDirection.normalized * moveSpeed * Time.deltaTime);
                ClampAsBounds();
            }

            transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);
        }

        private Vector2 GetEdgeMoveDirection(Vector2 pointerPosition)
        {
            Vector2 direction = Vector2.zero;

            if (pointerPosition.x <= edgeSize)
            {
                direction.x = -1f;
            }
            else if (pointerPosition.x >= Screen.width - edgeSize)
            {
                direction.x = 1f;
            }

            if (pointerPosition.y <= edgeSize)
            {
                direction.y = -1f;
            }
            else if (pointerPosition.y >= Screen.height - edgeSize)
            {
                direction.y = 1f;
            }

            return direction;
        }

        private void ClampAsBounds()
        {
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            targetPosition.z = transform.position.z;
        }

        private void OnEnable()
        {
            pointActionReference.action.Enable();
        }

        private void OnDisable()
        {
            pointActionReference.action.Disable();
        }
    }
}
