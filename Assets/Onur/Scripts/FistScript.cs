using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FistScript : MonoBehaviour
{
    public GameObject human;
    public GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.move(this.gameObject, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 30, this.gameObject.transform.position.z), 0.75f).setLoopPingPong(-1).setEase(LeanTweenType.easeInExpo);
           
    }

    // Update is called once per frame
    void Update()
    {
        target = GameObject.Find("Follow");
        if (this.gameObject.transform.position.y <= -100f)
        {
            Destroy(this.gameObject);
        }
        if (LevelManager.FeverActive && Vector3.Distance(this.gameObject.transform.position, target.transform.position) <= 10)
        {
            if(this.gameObject.GetComponent<Rigidbody>() == null)
            {
                this.gameObject.AddComponent<Rigidbody>();
            }
            
            this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
    }



    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "flying_human")
        {
            if (!LevelManager.FeverActive)
            {
                Destroy(other.gameObject);
                LevelManager.human_count--;
                iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
                GameObject clone = Instantiate(human, other.gameObject.transform.position, new Quaternion(0, 0, 0, 0)) as GameObject;
                clone.transform.Rotate(0, 90, 0);
                clone.AddComponent<Rigidbody>();
                clone.GetComponent<Rigidbody>().useGravity = true;
                clone.GetComponent<Rigidbody>().AddForce(400 * Vector3.up, ForceMode.Impulse);
                StartCoroutine(DestroyOther(clone));
            }
           
        }


    }

    IEnumerator DestroyOther(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        
        Destroy(obj);

    }
}
