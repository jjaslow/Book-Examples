using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllNPCFSM : MonoBehaviour
{
    Animator anim;
    Ray ray, rayShoot;
    RaycastHit hit, hitShoot;
    AnimatorStateInfo info;
    float distance;
    Transform FPS;
    GameObject gun;
    private float soundTimer;
    private bool soundPlaying;

    public Vector3 direction;
    public bool isInTheFieldOfView;
    public bool noObjectBetweenNPCAndPlayer = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        FPS = GameObject.Find("FPSController").transform;
        gun = GameObject.Find("hand_gun");
        gun.SetActive(false);
        soundTimer = 0;
        soundPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        ray.origin = transform.position + Vector3.up;
        ray.direction = transform.forward;
        info = anim.GetCurrentAnimatorStateInfo(0);

        distance = Vector3.Distance(transform.position, FPS.position);
        bool withinReach, closeToPlayer;
        withinReach = (distance < 1.5f);
        anim.SetBool("withinArmsReach", withinReach);

        direction = (FPS.position - transform.position).normalized;
        isInTheFieldOfView = (Vector3.Dot(transform.forward.normalized, direction) > .7);
        Debug.DrawRay(transform.position, direction * 100, Color.green);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.magenta);


        if (Physics.Raycast(transform.position, direction * 100, out hit))   //Physics.Raycast(ray.origin, ray.direction * 100, out hit
        {
            if (hit.collider.tag == "Player") noObjectBetweenNPCAndPlayer = true;
            else noObjectBetweenNPCAndPlayer = false;
           

            if (noObjectBetweenNPCAndPlayer && isInTheFieldOfView)
            {
                anim.SetBool("canSeePlayer", true);
                transform.LookAt(GameObject.Find("playerMiddle").transform);
                //Debug.Log("just saw the player");
            }
            else anim.SetBool("canSeePlayer", false);
        }

        if(info.IsName("IDLE"))
        {
            //Debug.Log("we are in IDLE state");
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
        else if (info.IsName("ATTACK_CLOSE_RANGE"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            if(info.normalizedTime%1.0>=.98)
            {
                GameObject.Find("FPSController").GetComponent<ManagePlayerHealth>().decreaseHealth(5);
            }
        }
        else if (info.IsName("HIT"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
        }
        else if (info.IsName("SHOOT"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
            if (anim.IsInTransition(0) && anim.GetNextAnimatorStateInfo(0).IsName("FOLLOW_PLAYER")) gun.SetActive(false);
            else
            {
                transform.LookAt(GameObject.Find("playerMiddle").transform);
                gun.SetActive(true);
                shootGun();
            }
            
            
        }
        else if (info.IsName("FOLLOW_PLAYER"))
        {
            GetComponent<UnityEngine.AI.NavMeshAgent>().destination = GameObject.Find("playerMiddle").transform.position;
            GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
            soundPlaying = false;

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

    public void setGotHitParameter()
    {
        anim.SetTrigger("gotHit");
    }
    public void setLowHealthParameter()
    {
        anim.SetBool("lowHealth", true);
    }

    private void shootGun()
    {
        rayShoot.origin = transform.position;
        rayShoot.direction = transform.forward;
        Debug.DrawRay(rayShoot.origin, rayShoot.direction * 100, Color.blue);

        if (Physics.Raycast(rayShoot.origin, rayShoot.direction * 100, out hitShoot))
        {
            Debug.Log("sight1: " + hitShoot.collider.tag);
        }


        if (!soundPlaying)
        {
            soundPlaying = true;
            GetComponent<AudioSource>().Play();
            
            Debug.Log("sight2: " + hitShoot.collider.tag);
    
                
                if(hitShoot.collider.tag == "Player")
                {
                    Debug.Log('a');
                    GameObject.Find("FPSController").GetComponent<ManagePlayerHealth>().decreaseHealth(5);
                }
                
    
        }
        //else
        //{
        //    soundTimer += Time.deltaTime;
        //}

    }
}
