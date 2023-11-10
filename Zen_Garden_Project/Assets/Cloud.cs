using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public GameObject prefab;
    public float spawntime;
    private float tp;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //very simple spwan function
        tp += Time.deltaTime;
        if (tp >= spawntime)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
            tp = 0;
        }
    }
}
