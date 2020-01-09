using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.gameObject.transform.position.y >= 15)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 15f, this.gameObject.transform.position.z);
        }

        if (this.gameObject.transform.position.y <= -20)
        {
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, -20, this.gameObject.transform.position.z);
        }
    }
}
