using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementSpike : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.move(this.gameObject, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 30, this.gameObject.transform.position.z), 1f).setLoopPingPong(-1);
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Follow");
        if (LevelManager.FeverActive && Vector3.Distance(this.gameObject.transform.position, target.transform.position) <= 20)
        {
            if(this.gameObject.GetComponent<Rigidbody>() == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            LeanTween.pause(this.gameObject);

        }
    }
}
