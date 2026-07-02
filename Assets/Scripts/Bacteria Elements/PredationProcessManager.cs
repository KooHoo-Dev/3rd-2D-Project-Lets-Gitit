using UnityEngine;

namespace SuperPhage
{
    public class PredationProcessManager : SingletonBase<PredationProcessManager>
    {
        [SerializeField] int phageThreshold = 5;
        private int phageAICount = 0;
        private bool canExplode = false;

        private void Update()
        {
            if (canExplode) return;

            if (phageAICount >= phageThreshold)
            {
                canExplode = true;
                EnableExplodeButton();
            }
        }

        private void EnableExplodeButton()
        {
            Debug.Log("Explode");
        }

        public void IncreasePhageCount()
        {
            phageAICount += 1;
        }
    }

}

