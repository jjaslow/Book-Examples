using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManagePlayerHealth : MonoBehaviour
{
    public int health = 100;
    public int nbLives = 3;
    public float alpha;
    public bool screenFlashBool;
    public Image flashColor;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] clones = new GameObject[2];
        clones = GameObject.FindGameObjectsWithTag("Player");
        if (clones.Length > 1) Destroy(clones[1]);

        alpha = 0;
        flashColor = GameObject.Find("screenFlash").GetComponent<Image>();
        flashColor.color = new Color(1, 0, 0, alpha);
        screenFlashBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(screenFlashBool)
        {
            alpha -= Time.deltaTime;
            flashColor.color = new Color(1, 0, 0, alpha);
            if(alpha<=0)
            {
                screenFlashBool = false;
                alpha = 0;
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void decreaseHealth(int healthIncrement)
    {
        Debug.Log('b');
        health -= healthIncrement;
        if (health <= 0) restartLevel();
        screenFlash();
    }

    public void increaseHealth(int healthIncrement)
    {
        health += healthIncrement;
        if (health > 150) health = 150;
    }

    public void restartLevel()
    {
        nbLives -= 1;
        health = 100;
        Application.LoadLevel(Application.loadedLevel);
    }

    public void screenFlash()
    {
        screenFlashBool = true;
        alpha = 1.0f;

    }
}
