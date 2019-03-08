using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithProjectile : MonoBehaviour


{

    public GameObject explosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Destroy(collision.gameObject);
            collision.gameObject.transform.position = GameObject.Find("Start").transform.position;
            GameObject smoke = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(smoke, 10);
        }
    }
}
