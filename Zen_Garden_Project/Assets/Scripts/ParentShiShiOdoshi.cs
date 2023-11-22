using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(102)]
public class ParentShiShiOdoshi : MonoBehaviour
{
    private Rigidbody rg;
    [SerializeField] private AnimationCurve animationcurve;
    
    private float angleRotation;
    
    
    private float _startRot;

    public Transform parent;
    
    //debug var
    public float DebugOvetip;
    
    private void Awake()
    {

        _startRot = transform.eulerAngles.z;
        rg = GetComponent<Rigidbody>();
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void rot(float overtip)
    {
        DebugOvetip = overtip;
        angleRotation = animationcurve.Evaluate(Mathf.Clamp(overtip,0,1)*5);

   
        var rot = Quaternion.AngleAxis(angleRotation -_startRot, transform.forward);
        var position = transform.position;
            
        transform.position=(rot * (transform.position - position) + position);
        transform.rotation=(transform.rotation * rot);
        _startRot = angleRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
