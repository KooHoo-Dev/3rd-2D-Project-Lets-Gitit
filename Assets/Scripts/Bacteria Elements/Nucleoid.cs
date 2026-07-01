using System;
using UnityEngine;

namespace SuperPhage
{
    public class Nucleoid : MicroEntityEmitter
    {
        [Header("Nucleoid/Prefabs")]
        [SerializeField] private GameObject AGTCPrefab;

        public void EmitAGTC(Vector2 nucleasePosition)
        {
            Vector2 direction = nucleasePosition - (Vector2)transform.position;

        }
    }
}