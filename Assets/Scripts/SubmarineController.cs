﻿using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;
using System;

public class SubmarineController : MonoBehaviour
{

    Rigidbody2D rb;

    public GameObject Ship;
    public Transform Greifarm;

    public float Bat;
    public float OX;
    public float Pres = 0;


    public UnityEngine.UI.Slider batSlider;
    public UnityEngine.UI.Slider oxSlider;
    public UnityEngine.UI.Slider presSlider;

    bool block = false;
    bool down = false;
    bool up = false;

    private const int maxHealth = 100;
    public int Health = maxHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Bat = Stats.Instance.maxBat;
        OX = Stats.Instance.maxOX;
    }

    void Update()
    {
        ProcessNeeds();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (up == false && block == false)
            {
                down = true;
                block = true;
            }
        }

        Grab();

        if (block)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
            return;
        }
    }

    void FixedUpdate()
    {
        if (block)
            return;

        float x = CrossPlatformInputManager.GetAxis("Horizontal") * Time.fixedDeltaTime;
        float y = CrossPlatformInputManager.GetAxis("Vertical") * Time.fixedDeltaTime;

        if (y > 0 && transform.position.y >= 0)
        {
            y = 0;
        }
        if (x == 0 && y == 0)
        {
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
        }
        else
        {
            Bat -= Stats.Instance.BatUseRate * Time.fixedDeltaTime;
            batSlider.value = Bat;
        }
        Vector3 delta = new Vector3(x, y, 0);
        rb.velocity = delta.normalized * Stats.Instance.SubSpeed;
        /*
        float angle = Vector2.Angle(Vector2.left, delta) - 90;
        transform.rotation = Quaternion.Euler(0, 0, -angle);*/

        /*
		if((x == 0 && y == 0) || (x == 0 && y != 0) || (x != 0 && y == 0)){
			transform.rotation = Quaternion.Euler (0, 0, 0);//TODO: probably less good
		}
		else if(x > 0 && y < 0){// nach unten rechts
			transform.rotation = Quaternion.Euler(0,0,-rotDeg);
		}
		else if(x < 0 && y < 0){// nach unten links
			transform.rotation = Quaternion.Euler(0,0,rotDeg);
		}
		else if(x > 0 && y > 0){// nach oben rechts
			transform.rotation = Quaternion.Euler(0,0,rotDeg);
		}
		else if(x < 0 && y > 0){// nach oben links
			transform.rotation = Quaternion.Euler(0,0,-rotDeg);
		}*/
    }

    public void DockAtShip()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        GetComponent<PolygonCollider2D>().enabled = false;
        Greifarm.GetComponent<BoxCollider2D>().enabled = false;
        transform.SetParent(Ship.transform, true);
        //Ship.GetComponent<ShipController>().enabled = true;
        transform.position = Ship.GetComponent<ShipController>().SubHolder.position;
        rb.Sleep();
        Ship.GetComponent<Rigidbody2D>().WakeUp();
        Ship.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
        Camera.main.GetComponent<Follow>().target = Ship.transform;
        Restock();
        this.enabled = false;
    }

    void Grab()
    {
        if (down)
        {
            Greifarm.Translate(new Vector3(-1, 0, 0) * Time.deltaTime * Stats.Instance.GreifarmSpeed);
            if (Greifarm.localPosition.y <= -2)
            {
                down = false;
                up = true;
            }
        }
        if (up)
        {
            Greifarm.Translate(new Vector3(1, 0, 0) * Time.deltaTime * Stats.Instance.GreifarmSpeed);
            if (Greifarm.localPosition.y >= 0)
            {
                up = false;
                Greifarm.transform.localPosition = new Vector3(0, 0, 0);
                block = false;
            }
        }
    }

    public void ArmUp()
    {
        up = true;
        down = false;
    }

    void ProcessNeeds()
    {
        Pres = Mathf.Abs(transform.position.y);
        presSlider.value = Pres;
        OX -= Stats.Instance.OXUseRate * Time.deltaTime;
        oxSlider.value = OX;
        if (transform.position.y >= 0)
        {
            OX = Stats.Instance.maxOX;
        }

        if (Bat <= 0)
        {
            Bat = 0;
            block = true;
        }
        if (OX <= 0)
        {
            OX = 0;
            block = true;
        }
        if (Pres >= Stats.Instance.maxPres)
        {
            block = true;
        }
    }

    public void Restock()
    {
        OX = Stats.Instance.maxOX;
        Bat = Stats.Instance.maxBat;
        batSlider.value = Bat;
        oxSlider.value = OX;
    }


    public void Damage(int amt)
    {
        Health -= amt;

        if (Health <= 0)
        {
            //Gameover
        }
    }
}
