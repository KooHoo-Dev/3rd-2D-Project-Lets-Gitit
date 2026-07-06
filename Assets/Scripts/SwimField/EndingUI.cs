using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperPhage
{
    public class EndingUI : MonoBehaviour
    {
        [SerializeField] private PhageController player;
        private const string SWIM_FIELD_SCENE_NAME = "SwimField";

        public void Show()
        {
            gameObject.SetActive(true);

            CapsuleCollider2D playerCollider = player.GetComponent<CapsuleCollider2D>();
            playerCollider.enabled = false;
        }

        public void Restart()
        {
            Time.timeScale = 1f;

            if (SwimFieldState.IsInitialized)
            {
                SwimFieldState.Instance.Clear();
            }

            SceneManager.LoadScene(SWIM_FIELD_SCENE_NAME);
        }
    }
}