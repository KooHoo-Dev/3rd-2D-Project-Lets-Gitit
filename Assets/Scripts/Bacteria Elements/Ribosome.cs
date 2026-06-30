using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

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
        public float produceTimer { get; private set; } = 0;
        // serialize for debugging
        [SerializeField] private RibosomeFactoryType FactoryType = RibosomeFactoryType.None;
        [SerializeField] private RibosomeView view;

        private void SelectFactoryTypeForDebugging()
        {
            if (Keyboard.current.digit1Key.wasPressedThisFrame)
            {
                SelectType(RibosomeFactoryType.Phage);
            }
            if (Keyboard.current.digit2Key.wasPressedThisFrame)
            {
                SelectType(RibosomeFactoryType.Nuclease);
            }
            if (Keyboard.current.digit3Key.wasPressedThisFrame)
            {
                SelectType(RibosomeFactoryType.None);
            }
        }

        private void Update()
        {
            SelectFactoryTypeForDebugging();

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
                view.NucleaseFactoryView();
            }

            if (type == RibosomeFactoryType.Phage)
            {
                FactoryType = RibosomeFactoryType.Phage;
                mRNA.color = Color.red;
                view.PhageFactoryView();
            }

            if (type == RibosomeFactoryType.None)
            {
                FactoryType = RibosomeFactoryType.None;
                mRNA.color = Color.white;
                view.EraseView();
            }
        }
    }
}


