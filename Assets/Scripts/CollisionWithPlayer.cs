using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionWithPlayer : MonoBehaviour
{

    int score;
    float messageTimer;
    public Text messageText;
    public Text scoreText;
    bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        messageTimer = 0;
        messageText.text = "";
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -2)
        {
            messageText.text = "Dead";
            gameOver = true;
            Destroy(this.gameObject);
        }

        scoreText.text = "Score: " + score;

        if (messageTimer +2 < Time.fixedTime && !gameOver)
        {
            messageText.text = "";
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "pick_me")
        {
            Destroy(collision.gameObject);
            score++;
            Debug.Log(score);
            messageText.text = "Got One";
            messageTimer = Time.fixedTime;
        }

        if (score == 4 && collision.gameObject.name=="End")
        {
            Debug.Log("win");
            messageText.text = "WIN";
            gameOver = true;
            Destroy(GameObject.Find("Launchers"));
        }
    }
}
