using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

    GameObject target;
    float distanceX, distanceY, timeFactor;
    
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Human");
        distanceX = Random.Range(-3, 3);
        distanceY = Random.Range(-3, 3);
        timeFactor = Random.Range(8, 10);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(target.transform.position.x-4-distanceX,target.transform.position.y-distanceY,this.transform.position.z), Time.deltaTime * 8);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.transform.rotation, Time.deltaTime * timeFactor);
    }





}
