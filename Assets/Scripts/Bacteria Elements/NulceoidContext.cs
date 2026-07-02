using UnityEngine;

namespace SuperPhage
{
    public class NulceoidContext : SingletonBase<NulceoidContext>
    {
        [field: SerializeField] public Nucleoid nucleoid { get; private set; }
    }
}

