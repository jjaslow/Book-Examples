using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllNPCFSM : MonoBehaviour
{
    Animator anim;
    Ray ray;
    RaycastHit hit;
    AnimatorStateInfo info;
    string objectInSight;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position;
        ray.direction = transform.forward;
        info = anim.GetCurrentAnimatorStateInfo(0);
        objectInSight = "";

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);

        if(Physics.Raycast(ray.origin, ray.direction * 100, out hit))
        {
            objectInSight = hit.collider.tag;
            Debug.Log("object in sight: " + objectInSight);
            if(objectInSight == "Player")
            {
                anim.SetBool("canSeePlayer", true);
                Debug.Log("just saw the player");
            }
        }

        if(info.IsName("IDLE"))
        {
            Debug.Log("we are in IDLE state");
        }
        else if (info.IsName("FOLLOW_PLAYER"))
        {
            transform.LookAt(hit.transform);
            if(objectInSight != "Player")
            {
                anim.SetBool("canSeePlayer", false);
                Debug.Log("just lost the player");
            }
            else
            {
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            Debug.Log("we are in FOLLOW_PLAYER state");
        }


    }
}
