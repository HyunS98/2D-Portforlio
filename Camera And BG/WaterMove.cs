using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    // ¡á¡á¡á¡á¡á¡á¡á º¯¼ö ¡á¡á¡á¡á¡á¡á¡á
    public Transform CameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(CameraTransform.position.x, -10.3f);
    }
}
