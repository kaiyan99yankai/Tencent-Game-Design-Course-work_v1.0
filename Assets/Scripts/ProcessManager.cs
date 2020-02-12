using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager : MonoBehaviour
{
    // Start is called before the first frame update
    private Ball ball;
    //[SerializeField] private int accelerationNumber = 1;
    private Acceleration acceleration;
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private Vector3 fixPosition;
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        acceleration = FindObjectOfType<Acceleration>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ball.notlaunched)
        {
            if (ball.notSelected)
            {
                ball.choosePosition(); 
            }
            else
            {
                chooseAcceleration();
            }
        }
    }
    public void chooseAcceleration() {
        if (acceleration.notSelected)
        {
            acceleration.gameObject.SetActive(true);
            acceleration.transform.position = getMousePosition();
            if (Input.GetMouseButtonDown(0))
            {
                acceleration.notSelected = false;
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, 0), ball.transform.rotation);
            }
        }
        else if(!acceleration.notSelected)
        {
            chooseArrow();
        }
    }
    public void chooseArrow() {
        if (Input.GetMouseButtonDown(1))
        {
            acceleration.notSelected = true;
            acceleration.gameObject.SetActive(false);
            Destroy(ball.arrow.gameObject);
            chooseAcceleration();
        }else if (Input.GetMouseButtonDown(0))
        {
            Destroy(ball.arrow.gameObject);
            ball.arrow.notSelected = false;
            ball.direction = ball.getMousePosition() - new Vector3(ball.transform.position.x, ball.transform.position.y,-5);
            ball.GetComponent<Rigidbody2D>().velocity = 10 * ball.direction.normalized;
            ball.notlaunched = false;
            Destroy(ball.arrow.gameObject);
        }
        
    }
    public Vector3 getMousePosition()
    {
        mousexinUnity = Input.mousePosition.x / Screen.width * ScreenWidthinUnity;
        mouseyinUnity = Input.mousePosition.y / Screen.height * ScreenHeightinUnity;
        mousePosition = new Vector3(mousexinUnity, mouseyinUnity, -5);
        return mousePosition;
    }
}
