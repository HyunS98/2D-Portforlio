using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockfall : MonoBehaviour
{
    public GameObject rock;
    bool NoCreate = false;

    void Update()
    {
        if(NoCreate == false)
        {
            StartCoroutine(CreateRock());
            NoCreate = true;
        }
    }

    IEnumerator CreateRock()
    {
        int ran = Random.Range(3, 5);
        yield return new WaitForSeconds(ran);
        Instantiate(rock, transform.position, Quaternion.Euler(0,0,90));
        NoCreate = false;
    }
}
