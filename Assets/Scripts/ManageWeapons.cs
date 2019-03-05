using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManageWeapons : MonoBehaviour
{

    Camera playersCamera;
    Ray rayFromPlayer;
    RaycastHit hit;
    public GameObject sparksAtImpact;
    Text currentGunStatus;
    public GameObject grenade;
    GameObject launcher;

    private const int WEAPON_GUN = 0;
    private const int WEAPON_AUTO_GUN = 1;
    private const int WEAPON_GRENADE = 2;

    private int currentWeapon;

    private float timer;
    private bool timerStarted;
    private bool canShoot = true;

    private bool[] hasWeapon;
    private int[] ammos;
    private int[] maxAmmos;
    private float[] reloadTime;
    private string[] weaponName;

    // Start is called before the first frame update
    void Start()
    {
        playersCamera = GetComponent<Camera>();
        currentGunStatus = GameObject.Find("CurrentGunStatus").GetComponent<Text>();
        launcher = GameObject.Find("launcher");

        hasWeapon = new bool[3];
        hasWeapon[WEAPON_GUN] = true;
        hasWeapon[WEAPON_AUTO_GUN] = true;
        hasWeapon[WEAPON_GRENADE] = true;

        weaponName = new string[3];
        weaponName[WEAPON_GUN] = "GUN";
        weaponName[WEAPON_AUTO_GUN] = "AUTOMATIC GUN";
        weaponName[WEAPON_GRENADE] = "GRENADE";

        ammos = new int[3];  //how many we have currently
        ammos[WEAPON_GUN] = 10;
        ammos[WEAPON_AUTO_GUN] = 10;
        ammos[WEAPON_GRENADE] = 5;

        maxAmmos = new int[3];
        maxAmmos[WEAPON_GUN] = 20;
        maxAmmos[WEAPON_AUTO_GUN] = 20;
        maxAmmos[WEAPON_GRENADE] = 5;

        reloadTime = new float[3]; //not finished yet
        reloadTime[WEAPON_GUN] = 2f;
        reloadTime[WEAPON_AUTO_GUN] = 0.5f;
        reloadTime[WEAPON_GRENADE] = 3f;

        currentWeapon = WEAPON_GUN;
    }

    // Update is called once per frame
    void Update()
    {
        rayFromPlayer = playersCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Debug.DrawRay(rayFromPlayer.origin, rayFromPlayer.direction * 100, Color.red);

        currentGunStatus.text = weaponName[currentWeapon] + " (" + ammos[currentWeapon] + ")";

        if (Input.GetKey(KeyCode.F) && ammos[currentWeapon] > 0 && canShoot && (currentWeapon == WEAPON_GUN || currentWeapon == WEAPON_AUTO_GUN) )
        {
            ammos[currentWeapon]--;
            GetComponent<AudioSource>().Play();
            canShoot = false;
            timer = 0f;
            timerStarted = true;

            //Debug.Log("you have " + ammos[currentWeapon] + " bullets left.");
            if (Physics.Raycast(rayFromPlayer, out hit, 100))
            {
                //Debug.Log("Object: " + hit.collider.gameObject.name + " in front of player.");
                Vector3 positionOfImpact = hit.point;
                GameObject sparks = Instantiate(sparksAtImpact, positionOfImpact, Quaternion.identity);
                Destroy(sparks, 5f);
                if(hit.collider.gameObject.tag == "target")
                {
                    hit.collider.gameObject.GetComponent<ManageNPC>().GotHit();
                }
            }
        }

        if (Input.GetKey(KeyCode.F) && ammos[currentWeapon] > 0 && canShoot && currentWeapon == WEAPON_GRENADE)
        {
            ammos[currentWeapon]--;
            GameObject myGrenade = Instantiate(grenade, launcher.transform.position, Quaternion.identity);
            myGrenade.GetComponent<Rigidbody>().AddForce(launcher.transform.forward * 400);
            
            canShoot = false;
            timer = 0f;
            timerStarted = true;


        }


        if (Input.GetKeyDown(KeyCode.Tab))
        {
            int nextWeapon = currentWeapon + 1;
            if (nextWeapon > 2) nextWeapon = 0;
            while (hasWeapon[nextWeapon]==false)
            {
                nextWeapon++;
                if (nextWeapon > 2) nextWeapon = 0;
            }
            currentWeapon = nextWeapon;
            //Debug.Log("activated weapon: " + weaponName[currentWeapon] + " with ammo of: " + ammos[currentWeapon]);
        }
            
        if(timerStarted)
        {
            timer += Time.deltaTime;
            if(timer >= reloadTime[currentWeapon])
            {
                canShoot = true;
                timerStarted = false;
            }
        }

    }

    public void AddAmmo(string ammoType) //ammo_gun ammo_auto_gun ammo_grenade
    {
        if (ammoType == "ammo_gun")
        {
            ammos[WEAPON_GUN] += 5;
            if (ammos[WEAPON_GUN] > maxAmmos[WEAPON_GUN]) ammos[WEAPON_GUN] = maxAmmos[WEAPON_GUN];
            Debug.Log("5 more bullets, total now at " + ammos[WEAPON_GUN]);
        }
        else if (ammoType == "ammo_auto_gun")
        {
            ammos[WEAPON_AUTO_GUN] += 5;
            if (ammos[WEAPON_AUTO_GUN] > maxAmmos[WEAPON_AUTO_GUN]) ammos[WEAPON_AUTO_GUN] = maxAmmos[WEAPON_AUTO_GUN];
            Debug.Log("5 more auto bullets, total now at " + ammos[WEAPON_AUTO_GUN]);
        }
        else if (ammoType == "ammo_grenade")
        {
            ammos[WEAPON_GRENADE] += 5;
            if (ammos[WEAPON_GRENADE] > maxAmmos[WEAPON_GRENADE]) ammos[WEAPON_GRENADE] = maxAmmos[WEAPON_GRENADE];
            Debug.Log("5 more grenades, total now at " + ammos[WEAPON_GRENADE]);
        }

    }

    
}


