using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMaskLineMidPoint : MonoBehaviour
{
    private Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
}
