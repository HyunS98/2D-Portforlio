using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    public Boss boss;
    Image blackBG;

    void Start()
    {
        blackBG = GetComponent<Image>();
    }

    void Update()
    {
        if (boss.hp <= 0)
        {
            StartCoroutine(BlackOut());
        }
    }

    IEnumerator BlackOut()
    {
        yield return new WaitForSeconds(3f);

        Color alpha = blackBG.color;

        alpha.a += Time.deltaTime;

        blackBG.color = alpha;
    }
}
