using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace SuperPhage
{
    public class ScreenEffectManager : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadeGroup;
        [SerializeField] private float defaultFadeDuration = 1f;

        private Coroutine fadeRoutine;

        private void Awake()
        {
            fadeGroup.alpha = 1f;
        }

        private void Start()
        {
            FadeIn();
        }

        public Coroutine FadeOut()
        {
            return FadeOut(defaultFadeDuration);
        }

        public Coroutine FadeOut(float duration)
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }

            fadeRoutine = StartCoroutine(FadeRoutine(1f, duration));
            return fadeRoutine;
        }

        public Coroutine FadeIn()
        {
            return FadeIn(defaultFadeDuration);
        }

        public Coroutine FadeIn(float duration)
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }

            fadeRoutine = StartCoroutine(FadeRoutine(0f, duration));
            return fadeRoutine;
        }

        private IEnumerator FadeRoutine(float targetAlpha, float duration)
        {
            float startAlpha = fadeGroup.alpha;
            float time = 0f;

            while (time < duration)
            {
                time += Time.deltaTime;
                fadeGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
                yield return null;
            }

            fadeGroup.alpha = targetAlpha;
            fadeRoutine = null;
        }
    }
}
