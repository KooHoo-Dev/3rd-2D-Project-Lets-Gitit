using UnityEngine;

namespace SuperPhage
{
    public class NucleoidContext : SingletonBase<NucleoidContext>
    {
        [field: SerializeField] public Nucleoid nucleoid { get; private set; }
    }
}

