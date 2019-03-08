using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageCollisionWithPlayer2 : MonoBehaviour
{
    public AudioClip powerUpSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.name == "ammo_gun" || hit.gameObject.name == "ammo_auto_gun" || hit.gameObject.name == "ammo_grenade")
        {
            transform.GetChild(0).GetComponent<ManageWeapons>().AddAmmo(hit.gameObject.name);
            Destroy(hit.gameObject);
            GetComponent<AudioSource>().clip = powerUpSound;
            GetComponent<AudioSource>().Play();
        }
    }

    

}
