using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTar : MonoBehaviour
{

    bool isTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isTouched)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction, Color.red,5f);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                hit.transform.gameObject.GetComponent<Jellyfier>().ApplyPressureToPoint(hit.point, 10000);
            }
        }
       
    }

    public void TouchDown()
    {
        isTouched = true;
    }

    public void TouchUp()
    {
        isTouched = false;
    }
}

