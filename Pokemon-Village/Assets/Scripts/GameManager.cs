using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public GameObject mew;

    public bool mewSpawned = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(player, new Vector3(120, 55, 650), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
