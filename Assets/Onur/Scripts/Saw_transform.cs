using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw_transform : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject Strt,End;


    private void Start()
    {
      
         StartCoroutine(Movement());
      

    }




    
    IEnumerator Movement()
    {


        LeanTween.move(this.gameObject, End.transform.position, 1f).setOnComplete(() =>
        {

            LeanTween.move(this.gameObject, Strt.transform.position, 1f);
        });
        
        yield return new WaitForSeconds(2f);
        
            StartCoroutine(Movement());
        

    }

}
