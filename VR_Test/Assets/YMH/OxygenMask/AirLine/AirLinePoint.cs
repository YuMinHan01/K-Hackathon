using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirLinePoint : MonoBehaviour
{
    private Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        point = new Vector3(0, 2.1f, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = point;
    }
}
