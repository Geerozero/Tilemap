using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jump = 4;
    public float jumpBoost;
    public float jumpPad = 4;
    public int score = 0;
    public int lives = 3;
    public Text scoreText;
    public Text winText;
    public Text livesText;
    public Text boostTextBox;
    private Rigidbody2D rb2d;
    public Transform newSpawn;
    public Camera mainCamera;
    public Transform cameraSpot;
    public AudioClip background;
    public AudioClip winMusic;
    public AudioSource audioPlayer;
    public AudioSource slappy;
    public AudioSource jumpAudio;
    private bool moveFlag = false;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isRight = false;
    private bool isAir = false;
    private bool musicPlay = false;
    public PlayAudio playAudio;
    private bool boostTip = false;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        winText.text = "";
        boostTextBox.text = "";

        SetText();

        audioPlayer.clip = background;
        audioPlayer.Play();

        animator = GetComponent<Animator>();

        animator.SetInteger("State", 0);

        sprite = GetComponent<SpriteRenderer>();
    }

    
    void Update()
    {

        if (Input.GetKey("escape"))
             Application.Quit();

        if(lives == 0 && !musicPlay)
        {
           this.gameObject.SetActive(false); 
        }

        if(score == 4 && !moveFlag)
        {
            transform.position = newSpawn.position;

            lives = 3;
            
            mainCamera.gameObject.transform.position = cameraSpot.position;

            moveFlag = true;

            SetText();
        }

        JumpAndFallAnim();

        
    }

    private void FixedUpdate() 
    {
        float moveHorizontal = Input.GetAxis("Horizontal");


        if(moveHorizontal != 0)
        {
            animator.SetInteger("State", 1);

            if(moveHorizontal > 0 && isRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;

                isRight = !isRight;   
            }

            if(moveHorizontal < 0 && !isRight)
            {
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;

                isRight = !isRight; 
            }
        }
        

        else
        {
            animator.SetInteger("State", 0);
        }
        
        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);

        

    }
    

    private void JumpAndFallAnim()
    {
        if(rb2d.velocity.y > 0)
        {
            animator.SetInteger("State", 2);
            isAir = true;
        }

        if(rb2d.velocity.y < 0)
        {
            animator.SetInteger("State", 3);
            isAir = true;
        }

        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(other.collider.tag == "Ground")
        {
            if(rb2d.velocity.y == 0 && isAir)
            {
                animator.SetInteger("State", 0);
                isAir = false;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                //rb2d.AddForce(new Vector2(0, jump), ForceMode2D.Impulse);

                jumpAudio.Play();
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jump;

                
            }
        }

        if(other.collider.tag == "Jump")
        {
            if(rb2d.velocity.y == 0 && isAir)
            {
                animator.SetInteger("State", 0);
                isAir = false;
            }
            //if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                jumpAudio.Play();
                GetComponent<Rigidbody2D>().velocity = Vector2.up * jumpPad;
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

            playAudio.Slap();
            pick.gameObject.SetActive(false);
            
            if(!musicPlay)
            {
            
                lives--;
            }
        }

        if(pick.gameObject.CompareTag("Power"))
        {
            playAudio.Boost();
            pick.gameObject.SetActive(false);
            
            
            jump = jump + jumpBoost;

            if(boostTip == false)
            {
                StartCoroutine(BoostText());
                boostTip = true;

                
            }
        }

        if(pick.gameObject.CompareTag("Life"))
        {
            playAudio.Boost();
            pick.gameObject.SetActive(false);
            
            
            lives += 1;

    
                StartCoroutine(LifeText());
    
        }

        SetText();
    }
    
    IEnumerator BoostText()
    {
        boostTextBox.text = "Powerups increase jump!";
        yield return new WaitForSeconds(5);

        boostTextBox.text = "";
    }

    IEnumerator LifeText()
    {
        boostTextBox.text = "Bonus Life! Use it against that ghost!";
        yield return new WaitForSeconds(5);

        boostTextBox.text = "";
    }
    private void SetText()
    {
        scoreText.text = "Score: " + score.ToString();
        livesText.text = "Lives: " + lives.ToString();

        if(score >= 8 && !musicPlay)
        {
            audioPlayer.Stop();
            
            winText.text = "You Win!";

            audioPlayer.clip = winMusic;
            audioPlayer.Play();

            musicPlay = true;
        }

        if(lives == 0 && score < 8)
        {
            winText.text = "You Lose..";
        }
    }


}
