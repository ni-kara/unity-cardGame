using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingEffe : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(SleepMethod());
    }

    IEnumerator SleepMethod() {
        while (true)
        {
            gameObject.transform.Rotate(Vector3.back, 30f);
            yield return new WaitForSeconds(0.15f);
        }
    }
}
