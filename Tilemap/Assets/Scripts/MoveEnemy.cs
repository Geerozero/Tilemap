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
    
    void Start()
    {
        startTime = Time.time;

        transform.position = startPos.position;

        journeyLength = Vector3.Distance(startPos.position, endPos.position);
    }
    void Update()
    {

        float distCovered = (Time.time - startTime) * speed;

        float fracJourney = distCovered / journeyLength;

        transform.position = Vector3.Lerp(startPos.position, endPos.position, Mathf.PingPong (fracJourney, 1));
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

