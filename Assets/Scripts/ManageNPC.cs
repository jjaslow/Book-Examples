using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageNPC : MonoBehaviour
{

    int health;
    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy();
    }

    public void gotHit()
    {
        health -= 50;
    }

    public void Destroy()
    {
        GameObject lastSmoke = Instantiate(smoke, transform.position, Quaternion.identity);
        Destroy(lastSmoke, 3);
        Destroy(gameObject);
    }

}
