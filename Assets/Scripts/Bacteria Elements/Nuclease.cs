using UnityEngine;

namespace SuperPhage
{
    [RequireComponent(typeof(SwimMotor))]
    public class Nuclease : MicroEntity
    {
        [SerializeField] private Transform Target;


        private SwimMotor swimMotor;

        private void Awake()
        {
            swimMotor = GetComponent<SwimMotor>();
        }
    }
}
