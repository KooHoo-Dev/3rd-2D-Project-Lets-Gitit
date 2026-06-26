using UnityEngine;

namespace SuperPhage
{
    public class EnergyOrb : MonoBehaviour
    {
        [SerializeField] private float offset = 0.2f;
        [SerializeField] private float speed = 6f;
        [SerializeField] private float energyAmount = 5f;
        private Transform target;
        private bool isAttracted = false;
        private PhageEnergy phageEnergy;

        private const string PLAYER_TAG = "Player";

        private void Update()
        {
            if (isAttracted == false || target == null) return;

            transform.position = Vector2.MoveTowards(
                transform.position, target.position, speed * Time.deltaTime);

            float sqrDist = (target.position - transform.position).sqrMagnitude;
            if (sqrDist < offset * offset)
            {
                phageEnergy.GainEnergy(energyAmount);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(PLAYER_TAG))
            {
                target = collision.gameObject.GetComponent<Transform>();
                phageEnergy = target.GetComponent<PhageEnergy>();
                isAttracted = true;
            }
        }
    }
}

