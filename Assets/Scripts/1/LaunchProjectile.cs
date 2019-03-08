using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{

    public GameObject projectile;
    public GameObject shootPoint;
    public GameObject target;
    float time;
    float shootInterval;

    // Start is called before the first frame update
    void Start()
    {
        shootInterval = Random.Range(5f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > shootInterval && target)
        {
            time = 0;
            GameObject t = Instantiate(projectile, shootPoint.transform.position, shootPoint.transform.rotation);
            Destroy(t, 3);
            t.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * 1500);
        }

        if (target)
        {
            transform.LookAt(target.transform);
        }

       
        
        
    }
}
