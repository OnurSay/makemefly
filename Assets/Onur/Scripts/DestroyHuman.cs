using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHuman : MonoBehaviour
{

    public GameObject pieceHuman;
    public GameObject particleObj;
    GameObject target;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Follow");
        if(this.gameObject.transform.position.y <= -100f)
        {
            Destroy(this.gameObject);
        }
        if (LevelManager.FeverActive && Vector3.Distance(this.gameObject.transform.position,target.transform.position)<= 5)
        {
            if(this.gameObject.GetComponent<MovementSpike>() == null)
            {
                if(parent.gameObject.GetComponent<Rigidbody>() == null)
                {
                    parent.AddComponent<Rigidbody>();
                }
                
                parent.GetComponent<Rigidbody>().useGravity = true;
            }
            if(this.gameObject.GetComponent<Rigidbody>() == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
            if(this.gameObject.GetComponent<Saw_transform>() != null)
            {
                this.gameObject.GetComponent<Saw_transform>().enabled = false;
            }
            if(this.gameObject.GetComponent<Animator>() != null)
            {
                this.gameObject.GetComponent<Animator>().StopPlayback();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "flying_human")
        {
            if (LevelManager.FeverActive)
            {
                if(this.gameObject.GetComponent<Rigidbody>() == null)
                {
                    this.gameObject.AddComponent<Rigidbody>();
                }
               
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
               
            }
            else
            {
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.Failure);
                Destroy(other.gameObject);
                other.gameObject.GetComponent<CapsuleCollider>().enabled = false;
                GameObject pieces = Instantiate(pieceHuman, other.gameObject.transform.position, other.gameObject.transform.rotation);
                GameObject particle = Instantiate(particleObj, other.gameObject.transform.position, other.gameObject.transform.rotation);

                LevelManager.human_count--;
                if(LevelManager.human_count == 0)
                {
                    LevelManager.human_count = 0;
                }
            }
           

        }
       
    }

   

}
