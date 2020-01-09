using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boost : MonoBehaviour
{
    float tempSpeed;
    float timer  = 3f;
    bool trigger = false;
    public static int boostCount = 0;
    public static bool speedUp = false;
    GameObject parent;
    
    // Start is called before the first frame update
    void Start()
    {
        parent = this.gameObject.transform.parent.gameObject;
        tempSpeed = 14;
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            timer -= Time.deltaTime;
            if(timer <= 0.5f)
            {
                trigger = false;
                speedUp = false;
                MoveForward.forwardSpeed = tempSpeed;
                timer = 3f;
                boostCount = 0;
                LevelManager.boost0 = false;
            }
        }
        if (FinishScript.isPassedFinishHole)
        {
            timer = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "flying_human"){
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
            iOSHapticFeedback.Instance.Trigger(iOSHapticFeedback.iOSFeedbackType.ImpactHeavy);
            Debug.Log("Triggered");
            this.gameObject.GetComponent<MeshCollider>().enabled = false;
            LeanTween.scale(parent, new Vector3(14f, 14f, 14f), 0.2f);
            LeanTween.scale(parent, new Vector3(0.01f, 0.01f, 0.01f), 0.3f).setEaseInExpo().setDelay(0.3f);
            
            boostCount++;
            
            if (!speedUp && boostCount == 2)
            {
                Debug.Log("Boost active");
                trigger = true;
                MoveForward.forwardSpeed = MoveForward.forwardSpeed * 7f;
                speedUp = true;
                
            }

        }
    }
}
