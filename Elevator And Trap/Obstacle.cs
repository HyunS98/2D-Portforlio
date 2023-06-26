using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    float rotSpeed = 100f;

    void Update()
    {
        transform.Rotate(0, 0, rotSpeed * Time.deltaTime);

        // 120도에서는 < 방향, 0으로 가면 360로 가기에 > 방향감
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
