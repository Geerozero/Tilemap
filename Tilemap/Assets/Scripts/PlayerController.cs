using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump = 4;
    private int score = 0;
    private int lives = 3;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    private Rigidbody2D rb2d;
    public Transform newSpawn;
    public Camera mainCamera;
    public Transform cameraSpot;
    public AudioClip background;
    public AudioClip winMusic;
    public AudioSource audioPlayer;
    private bool moveFlag = false;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        winText.text = "";

        SetText();

        audioPlayer.clip = background;
        audioPlayer.Play();
    }

    
    void Update()
    {

        if (Input.GetKey("escape"))
             Application.Quit();

        if(lives == 0)
        {
           this.gameObject.SetActive(false); 
        }

        if(score == 4 && !moveFlag)
        {
            transform.position = newSpawn.position;

            lives = 3;
            
            mainCamera.gameObject.transform.position = cameraSpot.position;

            moveFlag = true;
        }


        
    }

    private void FixedUpdate() 
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        
        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

    }
    
    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.tag == "Ground")
        {
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                //rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);

                GetComponent<Rigidbody2D>().velocity = Vector2.up * jump;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D pick) 
    {
        if(pick.gameObject.CompareTag("Pickup"))
        {
            pick.gameObject.SetActive(false);

            score++;
        }

        if(pick.gameObject.CompareTag("Enemy"))
        {
            pick.gameObject.SetActive(false);

            lives--;
            
        }

        SetText();
    }
    
    private void SetText()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        if(score >= 8)
        {
            audioPlayer.Stop();
            
            winText.text = "You Win!";

            audioPlayer.clip = winMusic;
            Debug.Log("Should be printing");
            audioPlayer.Play();
        }

        if(lives == 0 && score < 8)
        {
            winText.text = "You Lose..";
        }
    }
}
