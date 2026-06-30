using System;
using UnityEngine;

namespace SuperPhage
{
    public enum RibosomeFactoryType
    {
        None,
        Phage,
        Nuclease
    }

    public class Ribosome : MicroEntityEmitter
    {
        [Header("Ribosome/FactoryOutputPrefabs")]
        [SerializeField] private GameObject PhagePrefab;
        [SerializeField] private GameObject NucleasePrefab;

        [Header("Ribosome/Settings")]
        [SerializeField] private float ProduceInterval = 3;
        [SerializeField] private int RequiredAminoAcidForNuclease = 3;
        [SerializeField] private int RequiredAminoAcidForPhage = 3;
        [SerializeField] private int RequiredAGTCForPhage = 3;
        [SerializeField] private SpriteRenderer mRNA;


        // serialize for debugging
        [field: SerializeField] public int aminoAcidCount { get; private set; } = 0;
        [field: SerializeField] public int AGTCCount { get; private set; } = 0;
        private float produceTimer = 0;
        // serialize for debugging
        [SerializeField] private RibosomeFactoryType FactoryType = RibosomeFactoryType.None;
        

        private void Awake()
        {
            SelectType(RibosomeFactoryType.Phage);
        }

        private void Update()
        {
            if (FactoryType == RibosomeFactoryType.None) return;
            if (CanProduce() == false) return;

            produceTimer += Time.deltaTime;

            if (produceTimer >= ProduceInterval)
            {
                produceTimer = 0f;
                Produce();
            }
        }

        private bool CanProduce()
        {
            if (FactoryType == RibosomeFactoryType.Nuclease)
            {
                return aminoAcidCount >= RequiredAminoAcidForNuclease;
            }

            if (FactoryType == RibosomeFactoryType.Phage)
            {
                return (aminoAcidCount >= RequiredAminoAcidForPhage)
                    && (AGTCCount >= RequiredAGTCForPhage);
            }

            return false;
        }

        private void Produce()
        {
            if (FactoryType == RibosomeFactoryType.Nuclease)
            {
                aminoAcidCount -= RequiredAminoAcidForNuclease;
                base.Emit(NucleasePrefab);
            }

            if (FactoryType == RibosomeFactoryType.Phage)
            {
                aminoAcidCount -= RequiredAminoAcidForPhage;
                AGTCCount -= RequiredAGTCForPhage;
                base.Emit(PhagePrefab);
            }
        }

        public void SelectType(RibosomeFactoryType type)
        {
            if (type == RibosomeFactoryType.Nuclease)
            {
                FactoryType = RibosomeFactoryType.Nuclease;
                mRNA.color = Color.orange;
            }

            if (type == RibosomeFactoryType.Phage)
            {
                FactoryType = RibosomeFactoryType.Phage;
                mRNA.color = Color.red;
            }
        }
    }
}


