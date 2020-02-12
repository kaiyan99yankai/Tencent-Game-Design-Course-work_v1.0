using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    public bool notSelected = true;
    public bool notlaunched = true;
    private Vector2 mousePosition;
    public Arrow arrowOriginal;
    public Arrow arrow;
    public Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        arrowOriginal = FindObjectOfType<Arrow>();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void choosePosition() {
        transform.position = getMousePosition();
    }
    public Vector3 getMousePosition() {
        mousexinUnity = Input.touches[0].position.x / Screen.width * ScreenWidthinUnity;
        mouseyinUnity = Input.touches[0].position.y / Screen.height * ScreenHeightinUnity;
        mousePosition = new Vector3(mousexinUnity, mouseyinUnity,-5);
        return mousePosition;
    }
}
