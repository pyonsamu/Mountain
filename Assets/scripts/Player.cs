using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject bulletprefab;
    public Transform launchpoint;
    public Rigidbody rb;
    public bool canshot = true;
    public bool canjump = true;
    public int onGround = 0;
    public bool shotmode = false;
    public float shottime = 0;
    public GameObject bullet;
    public Timer timer;
    public GameObject startpos;
    public GameObject destructible1;
    public GameObject destructible2;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 10;
        rb.centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float horizontalRawInput = Input.GetAxisRaw("Horizontal");
        horizontalInput = horizontalInput / 0.7f;
        if (horizontalInput > 1)
        {
            horizontalInput = 1;
        }
        else if (horizontalInput < -1)
        {
            horizontalInput = -1;
        }
        Debug.Log(horizontalInput);
        
        if (onGround > 0)
        {
            rb.maxAngularVelocity = 7;
            rb.angularDrag = 5f;
            rb.AddTorque(new Vector3(0.0f, 0.0f, horizontalInput * -30), ForceMode.Force);
        }
        else
        {
            rb.maxAngularVelocity = 10;
            rb.angularDrag = 0.05f;
            rb.angularVelocity = new Vector3(rb.angularVelocity.x, rb.angularVelocity.y, horizontalInput * -10);
        }
        switch (horizontalInput)
        {
            case 1:

                break;
            case 0:

                break;
            case -1:

                break;
        }
        if (!shotmode)
        {
            canshot = false;
        }

        float shot = Input.GetAxis("Fire1");
        bool shot1 = Input.GetButton("Fire1");
        bool shot2 = Input.GetButtonDown("Fire1");
        bool shot3 = Input.GetButtonUp("Fire1");

        /*if (shot == 1 && canshot)
        {
            GameObject bullet = (GameObject)Instantiate(bulletprefab, launchpoint.position, launchpoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = rb.velocity;
            canshot = false;
        }
        else if (shot == 0)
        {
            canshot = true;
        }*/

        if (canshot && bullet == null)
        {
            if (shot2)
            {
                shottime = 0.5f;
            }
            if (shot1)
            {
                if (shottime < 0)
                {
                    Shot(10f,3f);
                }
                shottime -= Time.deltaTime;
            }
            
            else if (shot3)
            {
                Shot(5f,1f);
            }
        }

        if (!shot1)
        {
            canshot = true;
        }
        

        

        float jump = Input.GetAxis("Jump");
        if (jump == 1 && canjump && onGround > 0)
        {
            rb.velocity = new Vector3(rb.velocity.x,10.0f, rb.velocity.z);
            canjump = false;
        }
        else if (jump == 0)
        {
            canjump = true;
        }

        bool reset = Input.GetButtonDown("Reset");
        if(reset)
        {
            Restart();
        }

        if (Input.GetKey(KeyCode.Escape)) Application.Quit();
    }

    public void Shot(float v0, float time)
    {
        bullet = (GameObject)Instantiate(bulletprefab, launchpoint.position, launchpoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = rb.velocity;
        bullet.GetComponent<Bullet>().v0 = v0;
        bullet.GetComponent<Bullet>().time = time;
        canshot = false;
    }

    public void Restart()
    {
        transform.position = startpos.transform.position;
        transform.eulerAngles = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        destructible1.GetComponent<Explode>().Reset();
        destructible2.GetComponent<Explode>().Reset();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Bullet" && other.tag != "TimerStart" && other.tag != "TimerStop" && other.tag != "TimerReset")
        {
            onGround++;
        }

        if(other.tag == "TimerStart")
        {
            timer.TimerStart();
        }
        if (other.tag == "TimerStop")
        {
            timer.TimerStop();
        }
        if (other.tag == "TimerReset")
        {
            timer.TimerReset();
        }
        
    }
    public void OnTriggerStay(Collider other)
    {
        
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag != "Bullet" && other.tag != "TimerStart" && other.tag != "TimerStop" && other.tag != "TimerReset")
        {
            onGround--;
        }
    }
}
