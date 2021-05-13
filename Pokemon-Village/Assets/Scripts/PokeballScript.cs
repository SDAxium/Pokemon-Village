using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PokeballScript : MonoBehaviour
{
    public GameObject pokemonToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(pokemonToSpawn, this.transform.position + new Vector3(0, 0, 10), Quaternion.identity);
        }
    }
}
