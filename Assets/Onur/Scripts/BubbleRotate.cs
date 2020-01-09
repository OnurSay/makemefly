using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleRotate : MonoBehaviour
{

    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.FindGameObjectWithTag("Human");
       if(LevelManager.FeverActive == true)
        {
            if (Vector3.Distance(this.gameObject.transform.position, target.transform.position) <= 10f)
            {
                Debug.Log("Girdi");
                LeanTween.move(this.gameObject, target.gameObject.transform.position, 0.01f);
            }

        }
    }
}
