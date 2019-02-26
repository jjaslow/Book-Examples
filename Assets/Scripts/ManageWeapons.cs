using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageWeapons : MonoBehaviour
{

    Camera playersCamera;
    Ray rayFromPlayer;
    RaycastHit hit;
    public GameObject sparksAtImpact;
    int gunAmmo = 10;

    // Start is called before the first frame update
    void Start()
    {
        playersCamera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        rayFromPlayer = playersCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 100, Color.red);

        if(Input.GetKeyDown(KeyCode.F) && gunAmmo > 0)
        {
            gunAmmo--;
            Debug.Log("you have " + gunAmmo + " bullets left.");
            if (Physics.Raycast(rayFromPlayer, out hit, 100))
            {
                Debug.Log("Object: " + hit.collider.gameObject.name + " in front of player.");
                Vector3 positionOfImpact = hit.point;
                Instantiate(sparksAtImpact, positionOfImpact, Quaternion.identity);
                if(hit.collider.gameObject.tag == "target")
                {
                    hit.collider.gameObject.GetComponent<ManageNPC>().gotHit();
                }
            }
        }
        

    }
}
