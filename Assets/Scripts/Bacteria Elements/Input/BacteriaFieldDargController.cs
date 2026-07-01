using UnityEngine;
using UnityEngine.InputSystem;

namespace SuperPhage
{
    public class BacteriaFieldDargController : MonoBehaviour
    {
        [SerializeField] private InputActionReference pointActionReference;
        [SerializeField] private InputActionReference pressActionReference;
        [SerializeField] private Camera mainCamera;

        [SerializeField] private LayerMask draggableLayer;
        [SerializeField] private LayerMask dropTargetLayer;

        private MicroEntity currentEntity;
        private Vector3 dragOffset;

        private void Update()
        {
            if (pressActionReference.action.WasPressedThisFrame())
            {
                BeginDrag();
            }

            if (currentEntity != null)
            {
                FollowPointer();
            }

            if (pressActionReference.action.WasReleasedThisFrame())
            {
                EndDrag();
            }
        }

        private void BeginDrag()
        {
            Vector3 pointerWorldPosition = GetPointWorldPosition();
            Collider2D hit = Physics2D.OverlapPoint(pointerWorldPosition, draggableLayer);

            if (hit == null) return;
            
            if (hit.TryGetComponent(out MicroEntity entity) == false) return;
        
            currentEntity = entity;

            dragOffset = entity.transform.position - pointerWorldPosition;
        }

        private void FollowPointer()
        {
            Vector3 pointerWorldPosition = GetPointWorldPosition();
            currentEntity.transform.position = pointerWorldPosition;
        }

        private void EndDrag()
        {
            if (currentEntity == null) return;

            Collider2D hit = Physics2D.OverlapPoint(currentEntity.transform.position, dropTargetLayer);
            if (hit != null && hit.TryGetComponent(out RibosomeDropReceiver receiver))
            {
                bool accepted = receiver.TryReceive(currentEntity);

                if (accepted)
                {
                    Destroy(currentEntity.gameObject);
                    currentEntity = null;
                    return;
                }
            }

            currentEntity = null;
        }

        private Vector3 GetPointWorldPosition()
        {
            Vector2 screenPosition = pointActionReference.action.ReadValue<Vector2>();
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(screenPosition);
            worldPosition.z = 0f;

            return worldPosition;
        }

        private void OnEnable()
        {
            pointActionReference.action.Enable();
            pressActionReference.action.Enable();
        }

        private void OnDisable()
        {
            pointActionReference.action.Disable();
            pressActionReference.action.Disable();
        }

    }

}
