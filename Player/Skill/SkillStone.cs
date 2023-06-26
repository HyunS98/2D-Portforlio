using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStone : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    SpriteRenderer player_Ren;        // 플레이어 스프라이트
    SpriteRenderer stone_Ren;         // 라이트 스프라이트  

    public PlayerAll player;

    void Start()
    {
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        stone_Ren = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player").GetComponent<PlayerAll>();

        player.MoveSpeed = 0;       // 플레이어 움직임 정지
        DirStone();
        AttackBox();
    }

    // ■■■■■■■ 이동방향 함수 ■■■■■■■
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

    // ■■■■■■■ OverlapBoxAll 만들어 피해주기 ■■■■■■■
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

    // ■■■■■■■ 스킬의 생존 시간 함수 ■■■■■■■
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
