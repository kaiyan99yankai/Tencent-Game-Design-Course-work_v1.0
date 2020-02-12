using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessManager_1 : MonoBehaviour
{
    public Ball ball;
    private Jet jet;
    private bool jetSelected = false;
    private bool directionSelected = false;
    //[SerializeField] private int accelerationNumber = 1;
    private Accelerator acceleratorOriginal;
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private Vector3 originalVelocity;
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        jet = FindObjectOfType<Jet>();
        acceleratorOriginal = FindObjectOfType<Accelerator>();
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
        else if (!jetSelected)
        {
            useJet();
        }
        else if (jetSelected&&!directionSelected) {
            if (Input.GetMouseButtonDown(0))
            {
                ball.direction = ball.getMousePosition() - new Vector3(ball.transform.position.x, ball.transform.position.y,-5);
                ball.GetComponent<Rigidbody2D>().velocity = 1.5f * originalVelocity.magnitude*(ball.direction.normalized);
                Destroy(ball.arrow.gameObject);
                directionSelected = true;
            }
        }
    }
    private void useJet() {
        if (jet.distance <= 1)
        {
            ball.transform.position = jet.transform.position;
            originalVelocity = ball.GetComponent<Rigidbody2D>().velocity;
            ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, 0), ball.transform.rotation);
            Destroy(jet.gameObject);
            jetSelected = true;
        }
    }
    public void chooseAcceleration()
    {
        if (acceleratorOriginal.notSelected)
        {
            acceleratorOriginal.transform.position = getMousePosition();
            if (Input.GetMouseButtonDown(0))
            {
                acceleratorOriginal.notSelected = false;
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, 0), ball.transform.rotation);
            }
        }
        else if (!acceleratorOriginal.notSelected)
        {
            chooseArrow();
        }
    }
    public void chooseArrow()
    {
        if (Input.GetMouseButtonDown(1))
        {
            acceleratorOriginal.notSelected = true;
            Destroy(acceleratorOriginal.gameObject);
            Destroy(ball.arrow.gameObject);
            chooseAcceleration();
        }
        else if (Input.GetMouseButtonDown(0))
        {
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
