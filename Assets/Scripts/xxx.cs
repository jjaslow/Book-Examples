using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class xxx : MonoBehaviour
{

    public GameObject a, b;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(a.transform.position);
        Debug.Log(b.transform.position);
        Debug.Log("n1: "+ a.transform.position.normalized);
        Debug.Log("n2: " + b.transform.position.normalized);
        Debug.Log(Vector3.Dot(a.transform.position, b.transform.position));
        Debug.Log(Vector3.Dot(a.transform.position.normalized, b.transform.position.normalized));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
