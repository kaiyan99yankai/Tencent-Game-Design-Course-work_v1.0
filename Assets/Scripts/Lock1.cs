using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock1 : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject ball;
    private float limitVelocity = 15;
    private float velocityMag;
    public int condition = 0;
    private Guide_2 guide_2;
    void Start()
    {
        ball = GameObject.Find("MainBall");
        guide_2 = FindObjectOfType<Guide_2>();
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
            if (!guide_2.hit_1)
            {
                guide_2.hit_1 = true;
            }
            else { guide_2.hit_2 = true; }
        }
        else
        {
            condition = 1;
        }
    }
}
