using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    public static float forwardSpeed = 14;
    public static float valueX = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        valueX = this.gameObject.transform.position.x + (forwardSpeed * Time.deltaTime);
        this.gameObject.transform.position = new Vector3(valueX, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
    }
}
