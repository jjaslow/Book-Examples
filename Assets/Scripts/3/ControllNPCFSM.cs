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
    float distance;
    Transform FPS;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        FPS = GameObject.Find("FPSController").transform;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position + Vector3.up;
        ray.direction = transform.forward;
        info = anim.GetCurrentAnimatorStateInfo(0);
        objectInSight = "";

        distance = Vector3.Distance(transform.position, FPS.position);
        //Debug.Log(distance);
        bool withinReach, closeToPlayer;
        withinReach = (distance < 1.5f);
        anim.SetBool("withinArmsReach", withinReach);

        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        //Debug.DrawRay(ray.origin, new Vector3(-.025f,0,1) * 100, Color.red);
        //Debug.DrawRay(ray.origin, new Vector3(.025f, 0, 1) * 100, Color.red);


        if (Physics.Raycast(ray.origin, ray.direction * 100, out hit))
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
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
        else if (info.IsName("ATTACK_CLOSE_RANGE"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
        else if (info.IsName("FOLLOW_PLAYER"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = GameObject.Find("playerMiddle").transform.position;
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;

            /*if (objectInSight != "Player")
            {
                anim.SetBool("canSeePlayer", false);
                Debug.Log("just lost the player");
            }
            else
            {
                transform.LookAt(GameObject.Find("playerMiddle").transform);
                transform.Translate(Vector3.forward * Time.deltaTime);
            }
            Debug.Log("we are in FOLLOW_PLAYER state");
            */
        }


    }
}
