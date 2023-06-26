using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillLight : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    Animator ani;                     // 자체 애니메이션 가져오기
    float skillSpeed;                 // 스킬 속도
    Rigidbody2D skillRig;             // 스킬 리지드
    SpriteRenderer player_Ren;        // 플레이어 스프라이트
    SpriteRenderer light_Ren;         // 라이트 스프라이트  

    void Start()
    {
        skillSpeed = 7;
        skillRig = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        player_Ren = GameObject.Find("Player").GetComponent<SpriteRenderer>();
        light_Ren = GetComponent<SpriteRenderer>();

        Light_Dir();
    }

    void Update()
    {
        StartCoroutine(TimeDelay());
    }

    // ■■■■■■■ 이동방향 함수 ■■■■■■■
    void Light_Dir()
    {
        if (player_Ren.flipX == false)
        {
            skillRig.velocity = new Vector2(skillSpeed, 0);
        }
        else
        {
            skillRig.velocity = new Vector2(-skillSpeed, 0);
            light_Ren.flipX = true;
        }
    }

    // ■■■■■■■ 애니메이션 후 제거 함수 ■■■■■■■
    IEnumerator DeleteDelay()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    // ■■■■■■■ 스킬의 생존 시간 함수 ■■■■■■■
    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }

    // ■■■■■■■ 벽 or 물체 부딪치면 사라짐 ■■■■■■■
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 7 || other.gameObject.layer == 6 || other.gameObject.layer == 8 || other.gameObject.layer == 13)
        {
            skillRig.velocity = Vector2.zero;
            ani.SetTrigger("End");
            StartCoroutine(DeleteDelay());
        }

        //if(other.gameObject.layer == 13)
        //{
        //    Boss.instance.hp -= 10;
        //    skillRig.velocity = Vector2.zero;
        //    ani.SetTrigger("End");
        //    StartCoroutine(Boss.instance.ChageBossColor());
        //}
    }
}
