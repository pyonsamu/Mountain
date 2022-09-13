using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public Rigidbody rb;
    public Renderer render;
    public float time = 1;
    public float extime = 0;
    public List<GameObject> objects = new List<GameObject>();
    public float v0;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //rb.velocity = transform.forward * 10;
        rb.velocity = new Vector3(rb.velocity.x + transform.forward.x * v0, rb.velocity.y + transform.forward.y * v0, 0.0f);
        render = GetComponent<MeshRenderer>();
        render.material.EnableKeyword("_EMISSION");
        extime = 3;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        render.material.SetColor("_Color",Color.Lerp(new Color(0,0,0),new Color(1,0,0),1-time/extime));
        if (Mathf.Abs(time -0.5f*1)<0.05f || Mathf.Abs(time - 0.5f * 2) < 0.05f || Mathf.Abs(time - 0.5f * 3) < 0.05f || Mathf.Abs(time - 0.5f * 4) < 0.05f || Mathf.Abs(time - 0.5f * 5) < 0.05f || Mathf.Abs(time - 0.5f * 6) < 0.05f)
        {
            render.material.SetColor("_EmissionColor", Color.Lerp(new Color(0, 0, 0), new Color(1, 0, 0), 1 - time / extime));
        }
        else
        {
            render.material.SetColor("_EmissionColor", Color.Lerp(new Color(0, 0, 0), new Color(0, 0, 0), 1));
        }
        
        rb.AddForce(new Vector3(0.0f,-15.0f,0.0f));
        time -= Time.deltaTime;
        if(time < 0)
        {
            List <GameObject> rootobjlist = new List<GameObject>();
            for(int i=0;i< objects.Count; i++)
            {
                if (objects[i] == null)
                {
                    continue;
                }
                var rootobj = objects[i].transform.root.gameObject;
                if (!rootobjlist.Contains(rootobj))
                {
                    rootobjlist.Add(rootobj);
                    if (rootobj.GetComponent<Explode>() != null)
                    {
                        rootobj.GetComponent<Explode>().explode(transform);
                    }
                    else
                    {

                    }
                }
                
            }
            Destroy(gameObject);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        objects.Add(other.gameObject);
    }
    public void OnTriggerExit(Collider other)
    {
        objects.Remove(other.gameObject);
    }
}
