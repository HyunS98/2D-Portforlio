using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
    // ����
    [Header("-- Smoke --")]
    public Transform dustPos;
    public GameObject dustPrf;
    public SpriteRenderer player_SRN;
    public Transform player;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // ���ο� ������Ʈ�� �����ؼ� �������� ������ �������� ������ �ȵ�
            GameObject dust = Instantiate(dustPrf, dustPos);

            if (player_SRN.flipX == false)
            {
                dust.transform.position = new Vector2(player.position.x - 0.82f, dustPos.position.y);
            }
            if (player_SRN.flipX == true)
            {
                SpriteRenderer dust_flip;    // ���� �¿���� ��ȯ ����
                dust_flip = dust.GetComponent<SpriteRenderer>();
                dust_flip.flipX = true;
                dust.transform.position = new Vector2(player.position.x + 0.82f, dustPos.position.y);
            }

            Destroy(dust, 0.5f);
        }
    }
}
