using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDeletion : MonoBehaviour
{




    private Water _water;
    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject != null)
        {
            
            var _water = other.gameObject.GetComponent<Water>();
            if (_water)
            {
                Destroy(other.gameObject);
            }
          
            
          
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

}
