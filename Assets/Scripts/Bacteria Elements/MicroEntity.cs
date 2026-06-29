using UnityEngine;

namespace SuperPhage
{
    public class MicroEntity : MonoBehaviour
    {
        [SerializeField] private float lifeTime = 10f;
        private MicroEntityEmitter emitter;

        private void OnEnable()
        {
            Destroy(gameObject, lifeTime);
        }

        public void Init(MicroEntityEmitter emitter)
        {
            this.emitter = emitter;
        }

        private void OnDestroy()
        {
            emitter.DecreaseCount();
        }
    }

}
