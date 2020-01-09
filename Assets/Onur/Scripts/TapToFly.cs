using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToFly : MonoBehaviour
{
    public static bool isRestarted = false;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.7f).setLoopPingPong(0);
        StartCoroutine(set_fadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        if (isRestarted)
        {   
            LeanTween.scale(this.gameObject, new Vector3(1.2f, 1.2f, 1.2f), 0.7f).setLoopPingPong(0);
            StartCoroutine(set_fadeIn());
            isRestarted = false;
        }
    }
    IEnumerator set_fadeIn()
    {
        for (float a = 0.4f; a <= 1f; a += 0.05f)
        {
            Color c = this.gameObject.GetComponent<Text>().color;
          
            c.a = a;

            this.gameObject.GetComponent<Text>().color = c;
           
            yield return new WaitForSeconds(0.05f);
        }
        //StartCoroutine(set_fadeOut());
        StartCoroutine(set_fadeOut());


    }
    IEnumerator set_fadeOut()
    {
        for (float a = 1f; a >= 0.4f; a -= 0.05f)
        {
            Color c = this.gameObject.GetComponent<Text>().color;

            c.a = a;

            this.gameObject.GetComponent<Text>().color = c;

            yield return new WaitForSeconds(0.05f);
        }
        
        
        StartCoroutine(set_fadeIn());


    }
}
