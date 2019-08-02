using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnemy : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public float speed;
    private float journeyLength;
    private float startTime;
    private Rigidbody2D rb2d;
    public bool flipped = false;
    private float vx;
    private AudioSource audioSource;
    
    void Start()
    {
        startTime = Time.time;

        transform.position = startPos.position;

        journeyLength = Vector3.Distance(startPos.position, endPos.position);
        rb2d = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {

        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, Mathf.PingPong (fracJourney, 1));

    }


    private void LateUpdate() 
    {
        if(Vector3.Distance(transform.position,startPos.position) < 0.1 && flipped)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            flipped = false;
        }

        else if(Vector3.Distance(transform.position,endPos.position) < 0.1 && !flipped)
        {
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
            flipped = true;

        }
    }

}

        /*if(Vector2.Distance(transform.position, startPos.position) < 0.001f)
        {
            transform.position = Vector2.MoveTowards(transform.position, endPos.position, speed * Time.deltaTime);
        }
        if(Vector2.Distance(transform.position, endPos.position) < 0.001f)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos.position, speed * Time.deltaTime);
        }
        */

