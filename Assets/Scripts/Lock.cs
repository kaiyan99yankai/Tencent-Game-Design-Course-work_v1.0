using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject ball;
    private float limitVelocity = 15;
    private float velocityMag;
    public int condition = 0;
    void Start()
    {
        ball = GameObject.Find("MainBall");
    }

    // Update is called once per frame
    void Update()
    {    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        velocityMag = ball.GetComponent<Rigidbody2D>().velocity.magnitude;
        if (velocityMag >= limitVelocity)
        {
            Destroy(this.gameObject);
            condition = 2;
        }
        else
        {
            condition = 1;
        }
    }
}
