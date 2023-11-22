using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchorpoint : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private AnimationCurve animationcurve;
    public float fuck;
    private float angleRotation;
    [SerializeField] float strength=1; 
    private int r;
    
    private float _startRot;

    public Transform parent;
    
    //debug var
    public float DebugOvetip;
    
    private void Awake()
    {

        angleRotation = transform.eulerAngles.z;
        rg = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void rot(Vector3 trans,bool back, bool front)
    {
        DebugOvetip=trans.x-transform.position.x;
        
            if (DebugOvetip >0&&!front)
            {

                Vector3 localr = new Vector3(0, 0, --angleRotation);
                transform.localEulerAngles = localr;
            }
            
            if (DebugOvetip < 0&&!back)
            {
                Vector3 localr = new Vector3(0, 0, ++angleRotation);
                transform.localEulerAngles = localr;
            }
        


    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            
         
                
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
                         
        }
    }
}
