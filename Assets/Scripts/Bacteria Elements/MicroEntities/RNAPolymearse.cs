using SuperPhage;
using System.Collections;
using UnityEngine;

public class RNAPolymearse : MicroEntityEmitter
{
    [Header("RNAPolymearse/Prefabs")]
    [SerializeField] private GameObject[] mRNAPrefabs;
    private WaitForSeconds waitTime = new WaitForSeconds(2f);

    private void Start()
    {
        StartCoroutine(Test());
    }

    private IEnumerator Test()
    {
        while (true) 
        {
            int randIdx = Random.Range(0, mRNAPrefabs.Length);
            Emit(mRNAPrefabs[randIdx]);
            
            yield return waitTime;
        }
    }
}