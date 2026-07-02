using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    [RequireComponent(typeof(SwimMotor))]
    public class Nuclease : MicroEntity
    {
        [SerializeField] private Nucleoid target;
        [SerializeField] private float deComposeDelay = 2f;
        private SwimMotor swimMotor;
        bool isArrived = false;
        
        private void Awake()
        {
            swimMotor = GetComponent<SwimMotor>();
        }

        public override void Init(MicroEntityEmitter emitter)
        {
            base.Init(emitter);
            target = NulceoidContext.Instance.nucleoid;
        }

        private void Update()
        {
            if (target == null) return;
            if (isArrived) return;
            
            Vector2 directionToTarget = target.transform.position - transform.position;
            swimMotor.SetGoalDirection(directionToTarget.normalized);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<Nucleoid>
                (out Nucleoid nucleoid) == false) return;

            isArrived = true;
            StartCoroutine(EmitAGTC());
        }

        private IEnumerator EmitAGTC()
        {
            yield return new WaitForSeconds(deComposeDelay);

            target.EmitAGTC(transform.position);
            Destroy(gameObject);
        }
    }
}
