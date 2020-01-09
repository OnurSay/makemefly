using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDeactivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.GetComponent<LineRenderer>().enabled == false)
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
