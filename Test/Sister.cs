using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sister : MonoBehaviour
{
    public float speed;
    Animator ani;

    void Start()
    {
        ani = GetComponent<Animator>();
    }

    void Update()
    {
        
        if(Input.GetButton("MoveL"))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
            ani.SetBool("Run", true);
        }
        else
        {
            ani.SetBool("Run", false);
        }
    }
}
