using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    // ■■■■■■■ 변수 ■■■■■■■
    GameObject player;

    // ■■■■■■■ 카메라 최소,대 이동 위치 ■■■■■■■
    [Header("-- X 좌표 조절 --")]
    public float min;
    public float max;

    [Header("-- Y 좌표 조절 --")]
    public float min_y;
    public float max_y;

    // ■■■■■■■ 플레이어의 물리 관련(rigid)로 인해 FixedUpdate 사용
    void FixedUpdate()
    {
        player = GameObject.Find("Player");

        ViewRange();
    }

    void ViewRange()
    {
        Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, min, max), player.transform.position.y + 2.5f, -10f);

        // 만약 카메라가 min_y 아래로 떨어지면 Vector 값위치에 카메라는 멈춤
        if (transform.position.y <= min_y)
        {
            Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, min, max), min_y, -10f);
        }

        if (transform.position.y >= max_y)
        {
            Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, min, max), max_y, -10f);
        }
    }
}