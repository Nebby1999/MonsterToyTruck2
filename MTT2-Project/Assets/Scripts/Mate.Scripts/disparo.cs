using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disparo : MonoBehaviour
{

    public Transform firepoint;
    public GameObject misilPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Z"))
        {
            Shoot();

        }
        
    }

    void Shoot()
    {
        Instantiate(misilPrefab, firepoint.position, firepoint.rotation);
    }
}
