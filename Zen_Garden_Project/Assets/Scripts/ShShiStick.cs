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
    private BedRock _rock;
    private Vector3 averagePosition;
    private bool hitback=false;
    private bool hitfront=false;
    public int SumOfDrops;
    private Anchorpoint _Anchorpoint;
    public Transform BackTransform;
   
    public int backweight;
    private void OnDrawGizmos()
    {
        
        Gizmos.DrawSphere(averagePosition, 0.5f);
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _Anchorpoint = FindObjectOfType<Anchorpoint>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject!=null)
        {
            var waterBall = other.gameObject.GetComponent<Water>();
            if (waterBall != null)
            {
                waterBall.inside = true;
                SumOfVectors += waterBall.transform.position;
                SumOfDrops++;
            }
            var rock = other.gameObject.GetComponent<BedRock>();
            if (rock != null)
            {
                if (rock.CompareTag("Backstone"))
                {
                    hitback = true;
                    rock.PlaySound();

                }
                if (rock.CompareTag("Frontstone"))
                {
                    hitfront = true;
                    rock.PlaySound();
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject != null)
        {
            if (other.gameObject == WaterBall)
            {
                var waterball = other.gameObject.GetComponent<Water>();
                if (waterball)
                    waterball.inside = false;
                SumOfVectors -= waterball.transform.position;
            }
            var rock = other.gameObject.GetComponent<BedRock>();
            if (rock != null)
            {
                if (rock.CompareTag("Backstone"))
                {
                    hitback = false;
                }
                if (rock.CompareTag("Frontstone"))
                {
                    hitfront = false;
                }
            }
            
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
            _Anchorpoint.rot(averagePosition,hitback,hitfront);
                
            SumOfDrops = backweight;
            SumOfVectors = BackTransform.position*backweight;
        }
    }
}