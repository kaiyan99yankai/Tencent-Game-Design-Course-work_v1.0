using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jet : MonoBehaviour
{
    // Start is called before the first frame update
    private Guide_4 guide;
    public float distance;
    private Vector2 ballPosition2;
    private Vector2 position2;
    void Start()
    {
        guide = FindObjectOfType<Guide_4>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1));
        position2 = new Vector2(transform.position.x, transform.position.y);
        ballPosition2 = new Vector2(guide.ball.transform.position.x, guide.ball.transform.position.y);
        distance = Vector2.Distance(position2, ballPosition2);
    }
}
