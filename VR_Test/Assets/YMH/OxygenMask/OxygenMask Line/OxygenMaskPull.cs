using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMaskPull : MonoBehaviour
{
    private PullLine[] pullLines;

    private float pullDistance;
    private float beforePullSize;
    private float afterPullSize;

    private void Start()
    {
        pullLines = GetComponentsInChildren<PullLine>();
    }
    public void Init(float pullDistance, float beforePullSize, float afterPullSize)
    {
        this.pullDistance = pullDistance;
        this.beforePullSize = beforePullSize;
        this.afterPullSize = afterPullSize;

        foreach (var pullLine in pullLines)
            pullLine.Init(pullDistance, beforePullSize, afterPullSize);
    }
    public void OnPull()
    {
        pullLines[0].OnPull();
        pullLines[1].OnPull();
    }
}
