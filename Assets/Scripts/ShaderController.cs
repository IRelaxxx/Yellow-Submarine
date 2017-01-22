using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderController : MonoBehaviour
{
    private GameObject sub;
    private Material m;
	void Start ()
    {
        sub = GameObject.FindObjectOfType<SubmarineController>().gameObject;
        m = GetComponent<SpriteRenderer>().material;
	}
	
	void Update ()
    {
        m.SetFloat("_Alpha", 1 / Mathf.Abs(sub.transform.position.y));
	}
}
