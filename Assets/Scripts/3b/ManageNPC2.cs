using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageNPC2 : MonoBehaviour
{

    int health;
    public GameObject smoke;

    // Start is called before the first frame update
    void Start()
    {
        health = 550;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) Destroy();
    }

    public void GotHit()
    {
        health -= 50;
        GetComponent<ControllNPCFSM>().setGotHitParameter();
    }

    public void GotHitByGrenade()
    {
        health = 0;
        GetComponent<ControllNPCFSM>().setGotHitParameter();
    }

    public void Destroy()
    {
        //GameObject lastSmoke = Instantiate(smoke, transform.position, Quaternion.identity);
        //Destroy(lastSmoke, 3);
        //Destroy(gameObject, .25f);
        GetComponent<ControllNPCFSM>().setLowHealthParameter();
        Destroy(gameObject, 5);
    }

}
