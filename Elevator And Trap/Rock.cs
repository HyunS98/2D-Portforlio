using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Player"))
        {
            PlayerAll.instance.hp -= 3;
            Destroy(gameObject);
            Debug.Log(PlayerAll.instance.hp);
        }
    }

}
