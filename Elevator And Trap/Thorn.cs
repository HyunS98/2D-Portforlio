using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    bool AttackOK = false;  // �ð��������� ������ �ο�

    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(other.gameObject.name);
        if(AttackOK == false)
        {
            if (other.CompareTag("Player"))
            {
                PlayerAll.instance.hp -= 10;
                Debug.Log(PlayerAll.instance.hp);
                AttackOK = true;
                StartCoroutine(Delay());
            }
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.7f);
        AttackOK = false;
    }
}
