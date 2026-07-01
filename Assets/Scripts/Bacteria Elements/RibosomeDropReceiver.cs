using UnityEngine;

namespace SuperPhage
{
    public class RibosomeDropReceiver : MonoBehaviour
    {
        [SerializeField] Ribosome ribosome;

        public bool TryReceive(MicroEntity entity)
        {
            if (entity == null) return false;

            switch (entity.entityType)
            {
                case EntityType.AminoAcid:
                    ribosome.AddAminoAcid();
                    return true;
                case EntityType.AGTC:
                    ribosome.AddAGTC();
                    return true;
                case EntityType.mRNA_Nuclease:
                    ribosome.SelectFactoryType(RibosomeFactoryType.Nuclease);
                    return true;
                case EntityType.mRNA_Phage:
                    ribosome.SelectFactoryType(RibosomeFactoryType.Phage);
                    return true;
                default : return false;
            }
        }
    }

}

