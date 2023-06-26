using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    // 먼지
    [Header("-- Smoke --")]
    public Transform dustPos;
    public GameObject dustPrf;
    public SpriteRenderer player_SRN;
    public Transform player;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // 새로운 오브젝트를 선언해서 생성하지 않으면 프리팹이 삭제가 안됨
            GameObject dust = Instantiate(dustPrf, dustPos);

            if (player_SRN.flipX == false)
            {
                dust.transform.position = new Vector2(player.position.x - 0.82f, dustPos.position.y);
            }
            if (player_SRN.flipX == true)
            {
                SpriteRenderer dust_flip;    // 먼지 좌우방향 전환 변수
                dust_flip = dust.GetComponent<SpriteRenderer>();
                dust_flip.flipX = true;
                dust.transform.position = new Vector2(player.position.x + 0.82f, dustPos.position.y);
            }

            Destroy(dust, 0.5f);
        }
    }
}
