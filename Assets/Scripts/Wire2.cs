using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire2 : MonoBehaviour
{
    [SerializeField] private Transform _base1;
    [SerializeField] private Transform _base2;
    [SerializeField] private Transform _end2;
    [SerializeField] private Transform _end1;


    private void Update()
    {
        _end1.position = _base1.position;
    }
}