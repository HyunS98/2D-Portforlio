using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    // �������� ���� ��������
    public Transform CameraTransform;

    void LateUpdate()
    {
        transform.position = CameraTransform.position + new Vector3(0, 0, 10f);
    }

}
