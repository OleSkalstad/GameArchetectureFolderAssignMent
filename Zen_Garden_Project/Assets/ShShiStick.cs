using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DefaultExecutionOrder(101)]
public class ShShiStick : MonoBehaviour
{
    public GameObject visualAvarage;
    private Vector3 SumOfVectors;
    private Water WaterBall;
    private Vector3 averagePosition;

    private int SumOfDrops;

    private void OnDrawGizmos()
    {
       // Gizmos.DrawSphere(averagePosition, 0.5f);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject!=null)
        {
            var waterball = other.gameObject.GetComponent<Water>();
            if (waterball) waterball.inside = true;
        
            SumOfVectors += waterball.transform.position;
            SumOfDrops++;
            
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            var waterball = other.gameObject.GetComponent<Water>();
            if (waterball) 
                waterball.inside = false;

            SumOfVectors -= waterball.transform.position;
        }
        
    }
    

    public void WaterPosUpdate(Vector3 Positionmove)
    {
        SumOfVectors += Positionmove;
        SumOfDrops++;
    }

    // Update is called once per frame
    void Update()
    {
        if (SumOfDrops > 0)
        {
            
            
            averagePosition = SumOfVectors / SumOfDrops;
            // Do something with the average position of the raindrops.
            if(Input.GetKeyDown(KeyCode.A))
            {
                Debug.Log(averagePosition+" "+SumOfDrops+" "+SumOfVectors);
                Instantiate(visualAvarage, averagePosition, Quaternion.identity);
                
            }

            SumOfDrops = 0;
            SumOfVectors = new Vector3(0, 0, 0);
        }
    }
}
