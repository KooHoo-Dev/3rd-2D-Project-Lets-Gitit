using UnityEngine;
using UnityEngine.Rendering;

namespace SuperPhage
{
    public class SwimFieldRestorer : MonoBehaviour
    {
        [SerializeField] private Transform playerPhage;
        [SerializeField] private BacteriaIdentity[] bacterias;
        [SerializeField] private GameObject phageAIPrefab;
        [SerializeField] private Transform phageAIParent;
        [SerializeField] private float spawnRadius = 5f;

        private void Start()
        {
            RestorePhagePosition();
            RestoreBacterias();
            SpawnPendingPhageAIs();
        }

        private void RestorePhagePosition()
        {
            SwimFieldState state = SwimFieldState.Instance;

            if (state.HasSavedPhagePosition == false) return;
            if (playerPhage == null) return;

            playerPhage.position = state.SavedPhagePosition;
        }

        private void RestoreBacterias()
        {
            if (SwimFieldState.IsInitialized == false) return;

            foreach (BacteriaIdentity bacteria in bacterias)
            {
                if (bacteria == null) continue;

                if (SwimFieldState.Instance.IsBacteriaDefeated(bacteria.ID))
                {
                    bacteria.gameObject.SetActive(false);
                }
            }
        }

        private void SpawnPendingPhageAIs()
        {
            if (!SwimFieldState.IsInitialized) return;
            if (phageAIPrefab == null) return;
            if (playerPhage == null) return;

            foreach (Vector3 center in SwimFieldState.Instance.DefeatedBacteriaPositions)
            {
                for (int i = 0; i < 50; i++)
                {
                    Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                    Vector3 spawnPosition = center + (Vector3)randomOffset;

                    GameObject spawned = Instantiate(phageAIPrefab, spawnPosition, Quaternion.identity, phageAIParent);

                    if (spawned.TryGetComponent(out PhageAI phageAI))
                    {
                        phageAI.SetWanderArea(new Vector2(250f, 180f));
                    }
                }
            }


            int spawnCount = SwimFieldState.Instance.ConsumePendingPhageAISpawnCount();

            for (int i = 0; i < spawnCount; i++)
            {
                Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPosition = playerPhage.position + (Vector3)randomOffset;

                
            }
        }
    }

}
