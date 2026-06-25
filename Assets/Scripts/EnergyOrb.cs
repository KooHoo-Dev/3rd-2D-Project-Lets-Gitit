using UnityEngine;

namespace SuperPhage
{
    public class EnergyOrb : MonoBehaviour
    {
        [SerializeField] private float offset = 0.2f;
        [SerializeField] private float speed = 4f;
        [SerializeField] private float energyAmount = 5f;
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
                target.GetComponent<PhageEnergy>().GainEnergy(energyAmount);
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

