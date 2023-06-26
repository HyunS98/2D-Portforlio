using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour
{
    // �������� ���� ��������
    SpriteRenderer player_Ren;        // �÷��̾� ��������Ʈ
    SpriteRenderer stone_Ren;         // ����Ʈ ��������Ʈ  

    public PlayerAll player;

    void Start()
    {
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        stone_Ren = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerAll>();

        player.MoveSpeed = 0;       // �÷��̾� ������ ����
        DirStone();
        AttackBox();
    }

    // �������� �̵����� �Լ� ��������
    void DirStone()
    {
        if (player_Ren.flipX == false)
        {
            transform.position = new Vector3(transform.position.x + 2, transform.position.y + 0.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x - 2, transform.position.y + 0.1f);
            stone_Ren.flipX = true;
        }
        StartCoroutine(TimeDelay());
    }

    // �������� OverlapBoxAll ����� �����ֱ� ��������
    void AttackBox()
    {
        Collider2D[] colls = Physics2D.OverlapBoxAll(transform.position, new Vector2(3, 1.3f), 0);

        if(colls != null)
        {
            for(int i=0; i<colls.Length; i++)
            {
                if(colls[i].gameObject.layer == 8)
                {
                    colls[i].GetComponent<EnemyManager>().hp -= 20f;
                    Debug.Log(gameObject.name + " : " + colls[i].GetComponent<EnemyManager>().hp);
                }
                if(colls[i].gameObject.layer == 13)
                {
                    Debug.Log("dd");
                }
            }
        }
    }

    // �������� ��ų�� ���� �ð� �Լ� ��������
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(gameObject);
        player.MoveSpeed = 4f;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 1.3f, 0));
    }
}
