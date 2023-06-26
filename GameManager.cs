using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // ¡á¡á¡á¡á¡á¡á¡á º¯¼ö ¡á¡á¡á¡á¡á¡á¡á
    
    


    // ¡á¡á¡á¡á¡á¡á¡á ½Ì±ÛÅæ ¡á¡á¡á¡á¡á¡á¡á
    public static GameManager instance;

    void Awake()
    {
        //var obj = FindObjectsOfType<PlayerAll>();
        //if (obj.Length == 1)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward * 10.0f);
    //}
}