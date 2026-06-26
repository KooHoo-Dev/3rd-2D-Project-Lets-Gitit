using UnityEngine;

public class Receptor : MonoBehaviour
{
    [SerializeField] private float requiredStayTime = 3f;
    private float stayingTime = 0f;
    private bool isStaying = false;
    private bool isBound = false;

    private const string PHAGE_LEG_TAG = "PhageLeg";

    private void Update()
    {
        if (isStaying == false || isBound) return;

        stayingTime += Time.deltaTime;

        if (stayingTime >= requiredStayTime)
        {
            isBound = true;
        }

        if (isBound) Debug.Log("결합 성공");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(PHAGE_LEG_TAG))
        {
            isStaying = true;
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