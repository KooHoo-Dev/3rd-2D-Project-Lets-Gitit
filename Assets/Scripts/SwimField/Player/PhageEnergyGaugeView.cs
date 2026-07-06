using UnityEngine;

namespace SuperPhage
{
    public class PhageEnergyGaugeView : MonoBehaviour
    {
        [SerializeField] private PhageEnergy phageEnergy;
        [SerializeField] private RectTransform gaugeFill;

        private Vector3 fullScale;

        private void Awake()
        {
            fullScale = gaugeFill.localScale;
            RefreshDelta(1f);
        }

        private void Update()
        {
            if (phageEnergy == null) return;

            Debug.Log($"{phageEnergy.EnergyRatio}");
            RefreshDelta(phageEnergy.EnergyRatio);
        }

        private void RefreshDelta(float progress)
        {
            float clampedProgress = Mathf.Clamp01(progress);

            gaugeFill.localScale = new Vector3(fullScale.x, fullScale.y * clampedProgress, fullScale.z);
        }
    }
}