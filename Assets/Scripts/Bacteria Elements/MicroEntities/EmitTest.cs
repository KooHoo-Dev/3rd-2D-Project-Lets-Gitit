using SuperPhage;
using System.Collections;
using UnityEngine;

public class EmitTest : MonoBehaviour
{
    MicroEntityEmitter emitter;
    private WaitForSeconds waitTime = new WaitForSeconds(2f);

    private void Awake()
    {
        emitter = GetComponent<MicroEntityEmitter>();
    }

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true) 
        {
            emitter.EmitRandom();

            yield return waitTime;
        }
    }
}