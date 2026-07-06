using UnityEngine;

namespace SuperPhage
{
    public class BacteriaContext : SingletonBase<BacteriaContext>
    {
        [field: SerializeField] public Transform[] Bacterias { get; private set; }

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
        }
    }
}