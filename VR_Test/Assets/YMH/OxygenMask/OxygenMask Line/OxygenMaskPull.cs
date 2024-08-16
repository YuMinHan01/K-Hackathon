using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMaskPull : MonoBehaviour
{
    private PullLine[] pullLines;

    private void Start()
    {
        pullLines = GetComponentsInChildren<PullLine>();
    }
    public void OnPull()
    {
        pullLines[0].OnPull();
        pullLines[1].OnPull();
    }
}
