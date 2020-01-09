using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife_transform : MonoBehaviour
{
    Vector3 origin;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Movement());
        origin = this.gameObject.transform.position;
    }


    IEnumerator Movement()
    {


        LeanTween.move(this.gameObject, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 40, this.gameObject.transform.position.z), 0.5f).setOnComplete(()=> {
            LeanTween.move(this.gameObject, origin, 1f);
        });
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(Movement());
    }    
}
