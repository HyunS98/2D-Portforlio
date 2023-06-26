using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTornado : MonoBehaviour
{
    // �׽�Ʈ
    public static bool isTornado = false;
    BoxCollider2D trd_col;      // �ڽ��ݶ��̴�

    void Start()
    {
        trd_col = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Execution();
    }

    // �������� ���� ���ο� ���� �ڽ� ũ�� ��������
    public void Execution()
    {
        if (isTornado == true)
        {
            trd_col.size = new Vector2(1.5f, 1);
        }
        else
        {
            trd_col.size = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(isTornado == true)
        {
            if (other.gameObject.layer == 8)
            {
                other.GetComponent<Skeleton>().hp -= 100;
                //Debug.Log(gameObject.name + " : " + other.GetComponent<EnemyManager>().hp);
            }

            if (other.gameObject.layer == 13)
            {
                other.GetComponent<Boss>().hp -= 7;
                
            }
        }
    }
}
