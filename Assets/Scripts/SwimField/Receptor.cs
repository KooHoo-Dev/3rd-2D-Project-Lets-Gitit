using SuperPhage;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Receptor : MonoBehaviour
{
    [SerializeField] private float requiredStayTime = 3f;
    private float stayingTime = 0f;
    private bool isStaying = false;
    private bool isBound = false;
    private Transform phageTransform;

    private const string PHAGE_LEG_TAG = "PhageLeg";
    private const string BACTERIA_SCENE_NAME = "BacteriaField";

    private void Update()
    {
        if (isStaying == false || isBound) return;

        stayingTime += Time.deltaTime;

        if (stayingTime >= requiredStayTime)
        {
            isBound = true;

            BacteriaIdentity bacteria = GetComponentInParent<BacteriaIdentity>();

            if (bacteria != null && phageTransform != null)
            {
                SwimFieldState.Instance.SetCurrentBacteria(bacteria.ID, bacteria.transform.position);
                SwimFieldState.Instance.SavePhagePosition(phageTransform.position);
            }

            SceneManager.LoadScene(BACTERIA_SCENE_NAME);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PHAGE_LEG_TAG))
        {
            isStaying = true;
            phageTransform = collision.transform.root;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PHAGE_LEG_TAG))
        {
            isStaying = false;
            isBound = false;
            stayingTime = 0f;
        }
        
    }
}