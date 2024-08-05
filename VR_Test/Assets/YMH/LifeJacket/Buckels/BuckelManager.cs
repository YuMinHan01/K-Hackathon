using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuckelManager : MonoBehaviour
{
    Buckel[] buckels;

    private void OnEnable()
    {
        buckels = GetComponentsInChildren<Buckel>();
        buckels[0].CreateString(null);
        buckels[1].CreateString(null); 
    }

}
