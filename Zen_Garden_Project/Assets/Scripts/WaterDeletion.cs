using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class WaterDeletion : MonoBehaviour
{
    private float TD=0;
    [SerializeField] private int DESTRUCTION=0;

    

    private void OnCollisionEnter(Collision other)
    {
    
            
        if (other.gameObject != null)
        {
            
            var _water = other.gameObject.GetComponent<Water>();
     
            if (_water)
            {
                Destroy(other.gameObject, DESTRUCTION);
            }
          
            
          
        }
        
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame

}
