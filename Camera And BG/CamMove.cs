using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
    // �������� ���� ��������
    GameObject player;

    // �������� ī�޶� �ּ�,�� �̵� ��ġ ��������
    [Header("-- X ��ǥ ���� --")]
    public float min;
    public float max;

    [Header("-- Y ��ǥ ���� --")]
    public float min_y;
    public float max_y;

    // �������� �÷��̾��� ���� ����(rigid)�� ���� FixedUpdate ���
    void FixedUpdate()
    {
        player = GameObject.Find("Player");

        ViewRange();
    }

    void ViewRange()
    {
        Camera.main.transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, min, max), player.transform.position.y + 2.5f, -10f);

        // ���� ī�޶� min_y �Ʒ��� �������� Vector ����ġ�� ī�޶�� ����
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