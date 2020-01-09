using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Fly_Script : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    Rigidbody rb;

    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        ray = new Ray(this.transform.position, Vector3.down);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("fan")))
        {
            float distance_y = Mathf.Abs(this.transform.position.y - hit.collider.transform.position.y);
            if (distance_y < 25f)
            {
                if (rb.velocity.normalized == Vector3.down)
                {
                    rb.velocity -= rb.velocity * 0.5f;
                }

                rb.AddForce(Vector3.up * 20f, ForceMode.Acceleration);
                if (rb.velocity.y >= 50f)
                {
                    rb.velocity = new Vector3(0, 10f, 0);
                }
            }
            else {
                rb.velocity -= rb.velocity * 0.5f;
            }
        }
        else {
            rb.AddForce(Vector3.down * 16f, ForceMode.Acceleration);
        }
    }
}
