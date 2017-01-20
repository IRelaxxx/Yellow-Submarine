using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System;

public class ShipController : MonoBehaviour
{

    bool turnsRight = false;
    Rigidbody2D rb;

    public GameObject Sub;
    public Transform SubHolder;

    private bool subDocked = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
/*
    void Update()
    {
#if EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //ReleaseSub();
            Touch();//Dock / Release
        }
#endif
    }
*/
    void FixedUpdate()
    {
        if (subDocked)
        {
            float x = CrossPlatformInputManager.GetAxis("Horizontal") * Time.fixedDeltaTime;
            if (x > 0)
            {
                if (turnsRight == false)
                {
                    turnsRight = true;
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
            }
            else if (x < 0)
            {
                if (turnsRight == true)
                {
                    turnsRight = false;
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

            }
            rb.velocity = new Vector2(x, 0).normalized * Stats.Instance.ShipSpeed;
        }
    }

    void ReleaseSub()
    {
        subDocked = false;
        Sub.transform.SetParent(null, true);
        Sub.transform.position -= Vector3.down * -2;
        rb.Sleep();
        Sub.GetComponent<Rigidbody2D>().WakeUp();
        Sub.GetComponent<PolygonCollider2D>().enabled = true;
        Sub.GetComponent<SubmarineController>().Greifarm.GetComponent<BoxCollider2D>().enabled = true;
        Sub.GetComponent<SubmarineController>().enabled = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        Camera.main.GetComponent<Follow>().target = Sub.transform;
        Sub.GetComponent<SubmarineController>().Restock();
        //this.enabled = false;
    }

    private void OnMouseDown()
    {
        if (subDocked == true)
        {
            ReleaseSub();
        }
        else
        {
            float dist = Vector3.Distance(transform.position, Sub.transform.position);
            if (dist < 4)
            {
                subDocked = true;
                Sub.GetComponent<SubmarineController>().DockAtShip();
            }
        }
    }
}