using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class PokeballScript : MonoBehaviour
{
    private float x;
    private float y;
    private float z;
    public GameObject pokemonToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("colliding");
            Instantiate(pokemonToSpawn, this.transform.position + new Vector3(x, y, z), Quaternion.identity);
        }
    }
}
