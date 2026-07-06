using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SuperPhage
{
    public class PhageDeathController : MonoBehaviour
    {
        [SerializeField] private Transform head;
        [SerializeField] private Rigidbody2D headRigidbody;
        [SerializeField] private ScreenEffectManager screenEffectManager;
        [SerializeField] private float popForce = 6f;
        [SerializeField] private float reloadDelay = 0.8f;
        [SerializeField] private float fadeDuration = 1f;

        private float contactTimer;
        private float requiredContactTime = 2f;
        private float contactGraceTime = 0.6f;
        private float headDamping = 3f;
        private float lastContactTime = float.MinValue;
        private bool isDead = false;

        private void Awake()
        {
            headRigidbody.simulated = false;
        }

        private void Update()
        {
            if (isDead) return;

            bool recentlyTouched = Time.time - lastContactTime <= contactGraceTime;

            if (recentlyTouched)
            {
                contactTimer += Time.deltaTime;

                if (contactTimer >= requiredContactTime)
                {
                    Die();
                }
            }
            else
            {
                contactTimer = 0f;
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.GetComponent<AntiBodyAI>() == null) return;

            lastContactTime = Time.time;
        }

        private void Die()
        {
            isDead = true;

            GetComponent<PhageController>().enabled = false;
            GetComponent<SwimMotor>().enabled = false;

            StartCoroutine(DeathRoutine());
        }

        private void PopHead()
        {
            if (head == null || headRigidbody == null) return;
            
            head.SetParent(null);
            headRigidbody.simulated = true;
            headRigidbody.linearDamping = headDamping;
            headRigidbody.AddForce(transform.up * popForce, ForceMode2D.Impulse);
        }

        private IEnumerator DeathRoutine()
        {
            PopHead();

            yield return new WaitForSeconds(reloadDelay);

            if (screenEffectManager != null )
            {
                yield return screenEffectManager.FadeOut(fadeDuration);
            }

            if (SwimFieldState.IsInitialized)
            {
                SwimFieldState.Instance.Clear();
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
