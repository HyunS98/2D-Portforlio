using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float rotSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        // 120�������� < ����, 0���� ���� 360�� ���⿡ > ���Ⱘ
        if (transform.localEulerAngles.z >= 120)
        {
            rotSpeed = -rotSpeed;
        }
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        PlayerAll.instance.hp -= 10;
    //        Debug.Log(PlayerAll.instance.hp);
    //    }
    //}


}
