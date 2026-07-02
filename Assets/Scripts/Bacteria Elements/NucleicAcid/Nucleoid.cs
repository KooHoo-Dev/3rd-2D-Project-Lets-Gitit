using System;
using System.Collections;
using UnityEngine;

namespace SuperPhage
{
    public class Nucleoid : MicroEntityEmitter
    {
        [Header("Nucleoid/Prefabs")]
        [SerializeField] private GameObject AGTCPrefab;
        [SerializeField, Range(0f, 1f)]
        private float emitPositionRatio = 0.8f;

        public void EmitAGTC(Vector2 nucleasePosition)
        {
            Vector3 emitPosition = Vector2.Lerp(transform.position, nucleasePosition, emitPositionRatio);
            base.SetEmitPoint(emitPosition);

            Vector2 direction = nucleasePosition - (Vector2)transform.position;
            base.Emit(AGTCPrefab, direction);
        }
    }
}