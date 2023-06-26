using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage2_Child : MonoBehaviour
{
    Transform player;
    public string[] far_Talk;          // ��Ÿ��� �ֶ�
    public string[] close_Talk;          // ��Ÿ��� �ֶ�

    void Update()
    {
        player = GameObject.Find("Player").transform;

    }

    void OnMouseDown()
    {
        if (!(EventSystem.current.IsPointerOverGameObject()))
        {
            if (Vector2.Distance(this.transform.position, player.position) < 3f)
            {
                QuestManager.Childcnt++;
                TalkManager.instance.OnTalk(close_Talk, 0);
                gameObject.SetActive(false);
            }
            else
            {
                TalkManager.instance.OnTalk(far_Talk, 0);
            }
        }
    }
}
