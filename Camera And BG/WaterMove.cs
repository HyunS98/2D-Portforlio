using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMove : MonoBehaviour
{
    // �������� ���� ��������
    public Transform CameraTransform;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(CameraTransform.position.x, -10.3f);
    }
}
