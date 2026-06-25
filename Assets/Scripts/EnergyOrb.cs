using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace SuperPhage
{
    public class EnergyOrb : MonoBehaviour
    {
        [SerializeField] private float offset = 0.2f;
        [SerializeField] private float speed = 4f;
        private Transform target;
        private bool isAttracted = false;
        
        private const string PLAYER_TAG = "Player";

        private void Update()
        {
            if (!isAttracted || target == null) return;

            transform.position = Vector2.MoveTowards(
                transform.position, target.position, speed * Time.deltaTime);

            float sqrDist = (target.position - transform.position).sqrMagnitude;
            if (sqrDist < offset * offset)
            {
                Debug.Log("범위 내에 접촉");
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(PLAYER_TAG))
            {
                target = collision.gameObject.GetComponent<Transform>();
                isAttracted = true;
            }
        }
    }
}

