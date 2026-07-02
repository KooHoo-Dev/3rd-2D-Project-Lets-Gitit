using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SuperPhage
{
    [RequireComponent(typeof(SwimMotor))]
    public class PhageAI : MicroEntity
    {
        SwimMotor swimMotor;

        private void Awake()
        {
            swimMotor = GetComponent<SwimMotor>();
        }

        public override void Init(MicroEntityEmitter emitter)
        {
            base.Init(emitter);
            PredationProcessManager.Instance.IncreasePhageCount();
        }


    }
}
