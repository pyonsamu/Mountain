using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public Rigidbody rb;
    public bool destroy = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (destroy)
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //Destroy(this.gameObject);
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void explode(Transform tf)
    {
        if(rb != null)
        {
            Vector3 vec = transform.position  - tf.position;
            //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + 10, rb.velocity.z - 10);
            rb.velocity = new Vector3(vec.normalized.x * 10, vec.normalized.y * 10, 0.0f);
        }
        else
        {
            Debug.Log(name);
        }

        if(tag == "Destructible")
        {
            destroy = true;
            this.gameObject.GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            //Destroy(this.gameObject);
        }
    }

    public void Reset()
    {
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<BoxCollider>().enabled = true;
        destroy = false;
    }
}
