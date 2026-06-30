using UnityEngine;
using UnityEngine.InputSystem;

namespace SuperPhage
{
    public class BacteriaFieldDargController : MonoBehaviour
    {
        [SerializeField] private InputActionReference pointActionReference;
        [SerializeField] private InputActionReference pressActionReference;

        private MicroEntity currentEntity;

        private void Update()
        {
            
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
