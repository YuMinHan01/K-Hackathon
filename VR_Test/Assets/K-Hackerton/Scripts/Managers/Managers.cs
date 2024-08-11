using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // 유일성 보장
    static Managers Instance { get{ Init(); return s_instance; } } // 매너저를 가져옴

    InputManager _input = new InputManager();
    public static InputManager Input {get {return Instance._input;}}

    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();
    }

    static void Init ()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find ("@Managers");
            if(go == null)
            {
                go = new GameObject{name = "@Managers"};
                go.AddComponent<Managers>();
            }
            DontDestroyOnLoad(go);
            s_instance = go.GetComponent<Managers>();
        }
    }
}
