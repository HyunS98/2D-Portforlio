using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stage2_Child : MonoBehaviour
{
    Transform player;
    public string[] far_Talk;          // 사거리가 멀때
    public string[] close_Talk;          // 사거리가 멀때

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
