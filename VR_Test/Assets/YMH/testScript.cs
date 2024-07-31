using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    MeshRenderer mesh;
    Color blueColor;
    Color redColor;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        blueColor = new Color(0, 0, 1, 1);
        redColor = new Color(1, 0, 0, 1);
    }
    public void ChangeDefalut()
    {
        mesh.material.color = Color.white;
    }
    public void ChangeBlue()
    {
        Debug.Log("¡¢√À");
        mesh.material.color = blueColor;
    }
    public void ChangeRed()
    {
        Debug.Log("¿‚±‚");
        mesh.material.color = redColor;
    }
}
