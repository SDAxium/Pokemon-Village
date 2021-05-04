using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mew_Script : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(0,0,(float)0.1);
    }

    private void OnCollisionEnter(Collision other)
    {
        print("hit");
    }
}
