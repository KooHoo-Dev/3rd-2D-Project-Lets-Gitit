using UnityEngine;

namespace SuperPhage
{
    public enum EntityType
    {
        AminoAcid,
        AGTC,
        mRNA_Phage,
        mRNA_Nuclease,
        PhageAI,
        Nuclease
    }

    public class MicroEntity : MonoBehaviour
    {
        [field: SerializeField] public EntityType entityType { get; private set; }
        private MicroEntityEmitter emitter;
        
        public virtual void Init(MicroEntityEmitter emitter)
        {
            this.emitter = emitter;
        }

        private void OnDestroy()
        {
            if (emitter == null)
            {
                return;
            }

            emitter.DecreaseCount();

            if (RandomAreaSpawner.Instance != null)
            {
                RandomAreaSpawner.Instance.DecreaseCount();
            }
        }
    }
}
