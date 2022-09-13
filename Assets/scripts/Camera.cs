using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public GameObject targetobj;
    public Vector3 diff;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = targetobj.transform.position + new Vector3(0.0f,0.0f,-10.0f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 tarpos = targetobj.transform.position + new Vector3(0.0f, 0.0f, -10.0f);
        Vector3 nowpos = transform.position;
        transform.position = Vector3.Lerp(nowpos, tarpos,Mathf.Pow((tarpos-nowpos).magnitude/2,3));
    }

    private void LateUpdate()
    {
        
    }
}
