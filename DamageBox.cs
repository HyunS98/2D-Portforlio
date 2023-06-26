using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/***************** 주의점 ******************

이상하게 여기 부분은 instance를 만들고 다른곳에서 사용하면
(게임 실행중)코드 수정후 유니티로 돌아 갔을때마다 에러발생

********************************************/

public class DamageBox : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
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

        // ■■■■■■■ 캐릭터 방향별로 박스콜라이더 위치 변경 ■■■■■■■
        //if (playerRen.flipX == true)
        //{
        //    boxPos = -0.8f;
        //}
        //else
        //{
        //    boxPos = 0.8f;
        //}

        // 0.2f는 공격할때 플레이어가 띄는 현상때문에 추가함 
        //transform.position = new Vector2(playerPos.position.x + boxPos, playerPos.position.y + 0.2f);
    }

    //public void Attack_Box()
    //{
        

    //    // ■■■■■■■ OverlapBox 활성화 ■■■■■■■
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
