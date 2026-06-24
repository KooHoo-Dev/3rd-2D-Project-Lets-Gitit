using UnityEngine;
using UnityEngine.InputSystem;

namespace SuperPhage
{
    public class PhageController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveActionRefence;
        [SerializeField] private SwimMotor swimMotor;

        private void Update()
        {
            Vector2 moveDirection = moveActionRefence.action.ReadValue<Vector2>().normalized;
            swimMotor.SetGoalDirection(moveDirection);
        }

        private void OnEnable()
        {
            moveActionRefence.action.Enable();
        }

        private void OnDisable()
        {
            moveActionRefence.action.Disable();
        }
    }
}
