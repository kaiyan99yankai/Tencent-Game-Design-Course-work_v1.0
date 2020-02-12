using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accelerator : MonoBehaviour
{
    // Start is called before the first frame update
    public bool notSelected = true;
    private Ball ball;
    private float distance;
    void Start()
    {
        ball = FindObjectOfType<Ball>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position2 = new Vector2(transform.position.x, transform.position.y);
        Vector2 ballPosition2 = new Vector2(ball.transform.position.x, ball.transform.position.y);
        distance = Vector2.Distance(ballPosition2, position2);
        if (notSelected==false&&distance <= 0.8) {
            ball.GetComponent<Rigidbody2D>().velocity *=2f;
            Destroy(this.gameObject);
        }
    }
}
