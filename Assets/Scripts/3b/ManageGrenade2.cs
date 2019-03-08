using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageGrenade2 : MonoBehaviour

{

    float radius = 5.0f;
    float timer;
    bool hasExploded;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        hasExploded = false;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 2 && !hasExploded) Explode();
    }

    void Explode()
    {
        hasExploded = true;
        GetComponent<AudioSource>().Play();
        GameObject myExplosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(myExplosion, 3f);
        GetComponent<MeshRenderer>().enabled = false;
        Destroy(gameObject, 5f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        for (int x = 0; x<colliders.Length; x++)
        {
            if(colliders[x].gameObject.GetComponent<Rigidbody>() != null && colliders[x].gameObject.tag == "target")
            {
                colliders[x].gameObject.GetComponent<ManageNPC>().GotHitByGrenade();
            }
        }

        //play sound
    }
}
