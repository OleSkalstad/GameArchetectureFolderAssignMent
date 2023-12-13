using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extraboos : MonoBehaviour
{
    
    private Water waterball;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != null)
        {
            var waterball = other.gameObject.GetComponent<Water>();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
