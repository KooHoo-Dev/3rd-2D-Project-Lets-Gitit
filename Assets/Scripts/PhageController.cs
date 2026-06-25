using UnityEngine;
using UnityEngine.InputSystem;

namespace SuperPhage
{
    [RequireComponent(typeof(SwimMotor))]
    public class PhageController : MonoBehaviour
    {
        [SerializeField] private InputActionReference moveActionRefence;
        private SwimMotor swimMotor;

        private void Awake()
        {
            swimMotor = GetComponent<SwimMotor>();
        }

        private void Update()
        {
            Vector2 moveDirection = moveActionRefence.action.ReadValue<Vector2>().normalized;
            
            if (moveDirection.sqrMagnitude > 0.001f)
            {
                swimMotor.SetGoalDirection(moveDirection);
            }
            else
            {
                swimMotor.Brake();
            }
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
