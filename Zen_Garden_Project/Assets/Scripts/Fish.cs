using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using Color = UnityEngine.Color;

[RequireComponent(typeof(MeshFilter))]
public class Fish : MonoBehaviour
{

    private int n;
    private const int _DegreeI = 2;
    [SerializeField] private float[] _t;
    [SerializeField] public List<Vector3> controlPoints = new();  
    
    public Vector3[] Vertices;
    
    private const float H = 0.05f;
    private const float Tmin = 0.0f;
    private const float Tmax = 3.0f;



    private void Awake()
    {
        
        
        BSplineCurve();
        
    }


    int FindKnotInterval(float x)
    {
        int my = controlPoints.Count - 1; //Index til siste kontollpunkt
        while (my>=0 && my<_t.Length && x < _t[my] && my > _DegreeI)
            my--; 
        
        return my;
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
            a[_DegreeI - j] = controlPoints[my - j];
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
        foreach (var pointcont in controlPoints)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(pointcont, 0.2f);
        }

        for (int i = 0; i > controlPoints.Capacity; i++)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(controlPoints[i], controlPoints[i + 1]);
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
            a[i] = controlPoints[i];
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
        n = 5;
        _t = new float[]
        {
            0, 0, 0,
            1, 2,
            3, 3, 3,
        };
        

        Vector3 v = new Vector3(2.0f,2.0f,1.0f);
        Vector3[] c = new Vector3[4]; 
        c[0] = v; // første kontoll punkt
        
        
        //legg inn data for flere løsningpunkter
        //5 kontrollpunker kurve mange pnkter
        float delta_t = 0.1f;
        


    }
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
