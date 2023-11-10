using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(100)]
public class Water : MonoBehaviour
{
    private Rigidbody rb;
    
    [SerializeField] public int IMMIENENTDESTRUCTIOn;
    [SerializeField] public Vector3 Direction;
    private Vector3 PrevPosition;
    private Vector3 PosChange;

    private ShShiStick Shishi;
    //making a bool that checks if it is inside
    public bool inside = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Shishi = FindObjectOfType<ShShiStick>();
        addvelocity(Direction);
       // Destroy(gameObject, IMMIENENTDESTRUCTIOn);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (inside)
        {
          
            Shishi.WaterPosUpdate(transform.position);
        }
        
    }
    public void addvelocity(Vector3 retning)
    {
        rb.AddForce(retning,ForceMode.Impulse);
    }
    public void AddABoost()
    {
        rb.AddForce(new Vector3(0,100,0), ForceMode.VelocityChange);
        Debug.Log("hit");
    }
}
