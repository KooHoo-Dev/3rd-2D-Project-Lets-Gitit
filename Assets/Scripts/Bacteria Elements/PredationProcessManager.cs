using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace SuperPhage
{
    public class PredationProcessManager : SingletonBase<PredationProcessManager>
    {
        [SerializeField] private int phageThreshold = 5;
        [SerializeField] private Button explodeButton;
        private int phageAICount = 0;
        private bool canExplode = false;
        private const string swimFieldSceneName = "SwimField";

         protected override void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            explodeButton.gameObject.SetActive(false);
        }

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
            explodeButton.gameObject.SetActive(true);
        }

        public void IncreasePhageCount()
        {
            phageAICount += 1;
        }

        public void ExplodeButtonPressed()
        {
            SwimFieldState.Instance.MarkCurrentBacteriaDefeated();
            SceneManager.LoadScene(swimFieldSceneName);
        }
    }

}

