using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************** ������ ******************

�̻��ϰ� ���� �κ��� instance�� ����� �ٸ������� ����ϸ�
(���� ������)�ڵ� ������ ����Ƽ�� ���� ���������� �����߻�

********************************************/

public class DamageBox : MonoBehaviour
{
    // �������� ���� ��������
    Rigidbody2D rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        if(PlayerAll.spriteRender.flipX == true)
        {
            rigid.AddForce(Vector3.left * 4f, ForceMode2D.Impulse);
        }
        else
        {
            rigid.AddForce(Vector3.right * 4f, ForceMode2D.Impulse);
        }
        

        StartCoroutine(test());

    }

    void Update()
    {
        //rigid.AddForce(Vector3.right * 2f * Time.deltaTime, ForceMode2D.Impulse);

        // �������� ĳ���� ���⺰�� �ڽ��ݶ��̴� ��ġ ���� ��������
        //if (playerRen.flipX == true)
        //{
        //    boxPos = -0.8f;
        //}
        //else
        //{
        //    boxPos = 0.8f;
        //}

        // 0.2f�� �����Ҷ� �÷��̾ ��� ���󶧹��� �߰��� 
        //transform.position = new Vector2(playerPos.position.x + boxPos, playerPos.position.y + 0.2f);
    }

    //public void Attack_Box()
    //{
        

    //    // �������� OverlapBox Ȱ��ȭ ��������
    //    //Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(1, 1), 0);

    //    //if (colls != null)
    //    //{
    //    //    for (int i = 0; i < colls.Length; i++)
    //    //    {
    //    //        if (colls[i].gameObject.layer == 8)
    //    //        {

    //    //        }
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    return;
    //    //}
    //}

    IEnumerator test()
    {
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(0.25f, 1, 0));
    }
}
