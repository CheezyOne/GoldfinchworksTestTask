using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private int index;

    private void Awake()
    {
        StartCoroutine(TesTt());
    }

    private IEnumerator TesTt()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            for (int i = 0; i < 50; i++)
            {
            }
        }
    }
}