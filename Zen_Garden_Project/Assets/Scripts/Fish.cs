using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

[RequireComponent(typeof(MeshFilter))]
public class Fish : MonoBehaviour
{
    [SerializeField] private Transform[] traveldestination;
    
    private int n;
    private const int _DegreeI = 2;
    [SerializeField] private int[] _t;
    private float randomSpawn;
    
    private const float H = 0.05f;
    private const float Tmin = 0.0f;
    private const float Tmax = 3.0f;
    
    

    private float _TD=0;
    [SerializeField] private float SlowSpeed=1;



    private void Awake()
    {
        //init the spawntime
        randomSpawn = _TD + Random.Range(1.5f, 4f);
        
        //inits the spline
        n = traveldestination.Length;
        InitKjøtevektor();
        BSplineCurve();
        
    }
    
    void InitKjøtevektor()
    {
        int index = 0;
        _t = new int[n + _DegreeI + 1];
        _t[0] = 0;
        _t[1] = 0;
        _t[2] = 0;
     
        
        for (int i = 0; i < n-2; i++)
        {
            _t[i + 3] = i+1;
            index++;
        }

  
        _t[n + _DegreeI-1] = index;
        _t[n + _DegreeI ] = index;
        
       
        
    }


    int FindKnotInterval(float x)
    {
        int my = traveldestination.Length - 1; //Index til siste kontollpunkt
        while (my>=0 && my<_t.Length && x < _t[my] && my > _DegreeI)
            my--; 
        
        return my;
    }

    private void FixedUpdate()
    {
        _TD += Time.deltaTime/SlowSpeed;
        if (_TD > traveldestination.Length - 2)
        {
            if (_TD >= randomSpawn)
            {
                randomSpawn = _TD + Random.Range(1.5f, 4f);
                _TD = 0;
            }
        }
        else
        {
            //makes the fish move and rotate with the spline
            transform.LookAt(transform.position + (transform.position - EvaluateBSplineCurve(_TD)), transform.up);
            transform.position = EvaluateBSplineCurve(_TD);
        }


    }

    //from the notes dag has made
    public Vector3 EvaluateBSplineCurve(float x)
    {
        int my = FindKnotInterval(x);
        Vector3[] a = new Vector3[_DegreeI+1];
        
       // Debug.Log("my= " + my);
        
        for (int j = 0; j <= _DegreeI; j++)
        {
           // Debug.Log("j= "+j);
           a[_DegreeI - j] = traveldestination[my - j].position;
           
        }


        for (int k = _DegreeI; k >0; k--)
            {
                int j = my - k;
                
                for (int i = 0; i < k; i++)
                {
                    j++;


                    float w = (x - _t[j]) / (_t[j + k] - _t[j]);
                    a[i] = a[i] * (1 - w) + a[i + 1] * w;
                }

            }
        
        return a[0];

    }

    private void OnDrawGizmos()
    {
        //drawing the points on the map
        foreach (var pointcont in traveldestination)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointcont.position, 0.2f);
        }

        for (int i = 0; i > traveldestination.Length; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(traveldestination[i].position, traveldestination[i + 1].position);
        }

        // drawing the spline 
        if (_t.Length > 0)
        {
            var prev = EvaluateBSplineCurve(Tmin);
            for (var t = Tmin + H; t <= Tmax; t += H)
            {
                //Debug.Log("prev:  "+prev);
                var current = EvaluateBSplineCurve(t);
                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(prev, current);
                prev = current;
            }
        }
    }
    

    Vector3 EvaluateBezier(float x)
    {

        Vector3[] a= new Vector3[4]; //4=d+1 for kubisk bezier
        for(int i = 0; i<4; i++)
        {
            a[i] = traveldestination[i].position;
        }

        for (int j = _DegreeI; j > 0; j--)  //for (int k=1; k<=d; k++)
        {
            for (int i = 0; i < j; i++) //for (int i=0; i<=d-k;i++)
            {
                a[i] =  a[i] * (1 - x) + a[i + 1] * x;
            }
        }

        return a[0];
    }

    public void BSplineCurve()
    {

        

        Vector3 v = new Vector3(2.0f,2.0f,1.0f);
        Vector3[] c = new Vector3[4]; 
        c[0] = v; // første kontoll punkt
        
        
        //legg inn data for flere løsningpunkter
        //5 kontrollpunker kurve mange pnkter
        float delta_t = 0.1f;
        


    }

    private void Update()
    {
        
    }
}
