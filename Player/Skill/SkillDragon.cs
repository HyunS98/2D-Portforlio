using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDragon : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    float skillSpeed;                   // 스킬 속도
    Rigidbody2D skillRig;               // 스킬 리지드
    SpriteRenderer player_Ren;          // 플레이어 스프라이트     
    SpriteRenderer dragon_Ren;          // 드래곤 스프라이트
    
    void Start()
    {
        skillSpeed = 100f;
        skillRig = GetComponent<Rigidbody2D>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        dragon_Ren = GetComponent<SpriteRenderer>();

        DragonDir();
    }

    // ■■■■■■■ 이동방향 함수 ■■■■■■■
    void DragonDir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.AddForce(Vector2.right * skillSpeed, ForceMode2D.Force);
        }
        else
        {
            skillRig.AddForce(Vector2.left * skillSpeed, ForceMode2D.Force);
            dragon_Ren.flipX = true;
        }
        StartCoroutine(TimeDelay());
    }

    // ■■■■■■■ 스킬의 생존 시간 함수 ■■■■■■■
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(4f);
        Destroy(gameObject);
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.gameObject.layer == 8)
    //    {
    //        other.GetComponent<EnemyManager>().hp -= 10;
    //        Debug.Log(gameObject.name + " : " + other.GetComponent<EnemyManager>().hp);
    //    }
    //}
}
