using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{
    public bool hasFound = false;
    private Quaternion rotations = Quaternion.identity;
    private GameObject ball;
    private Vector3 mousePosition;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 horizon = new Vector3(0f, 1f, 0f);
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    public bool notSelected = true;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("MainBall");
        hasFound = true;
    }
    // Update is called once per frame
    void Update() {
        if (hasFound&&notSelected)
        {
            mousexinUnity = Input.touches[0].position.x / Screen.width * ScreenWidthinUnity;
            mouseyinUnity = Input.touches[0].position.y / Screen.height * ScreenHeightinUnity;
            mousePosition = new Vector3(mousexinUnity, mouseyinUnity, -5);
            rotations.SetFromToRotation(horizon, mousePosition - ball.transform.position);
            transform.rotation = rotations;
        }
    }
}
