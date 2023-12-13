using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Anchorpoint : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private AnimationCurve animationcurve;
    private float angleRotation;
    [SerializeField] float strength=1; 
    private int r;
    
    private float _startRot;

    public Transform parent;
    
   public float Ovetip;
    
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
        Ovetip=trans.x-transform.position.x;
        
            if (Ovetip >0&&!front)
            {

                Vector3 localr = new Vector3(0, 0, --angleRotation);
                transform.localEulerAngles = localr;
            }
            
            if (Ovetip < 0&&!back)
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
