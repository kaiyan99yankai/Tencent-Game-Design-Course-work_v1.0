using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Phone : MonoBehaviour
{
    private Ball ball;
    private Accelerator accelerator;
    private Lock lock_1;
    private Dialog dialog;
    private Index index;
    GameObject setting;
    //They are the objects that would be used during the game
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 12f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    //The coefficients used to check the location of the mouse
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        accelerator = FindObjectOfType<Accelerator>();
        lock_1 = FindObjectOfType<Lock>();
        dialog = FindObjectOfType<Dialog>();
        index = FindObjectOfType<Index>();
        //Find the gameoject from the scene
        GameObject.DontDestroyOnLoad(index.gameObject);
        //Preserve the index when reload the scene
        ball.transform.position = new Vector3(8, 1, -5); //The original position of the ball
        accelerator.transform.position = new Vector3(4, 8, 0); //The original position of the accelerator
        lock_1.transform.position = new Vector3(8, 11, 0); //The original position of the lock
        accelerator.gameObject.SetActive(false);
        dialog.gameObject.SetActive(true);
        setting = GameObject.Find("Canvas/Setting");
        dialog.setText("尝试使用角色球来击碎锁吧\n" + "单机左键开始游戏");
        Time.timeScale = 0;
    }
    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(mainGame());
        checkLock();
    }
    IEnumerator mainGame()
    {
        startGame();
        yield return new WaitForSeconds(0.01f); //use this function to let the two operation separate
        chooseAccelerator();    //The fuction to choose the position of the accelerato 
        yield return null;
    }
    public void chooseAccelerator()
    {
        if (accelerator.notSelected)
        {
            accelerator.enabled = true;
            accelerator.transform.position = new Vector3(2, Mathf.Clamp(getMousePosition_3().y, 1, 11));
            if (Input.GetMouseButtonDown(0))
            {
                accelerator.notSelected = false;
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -1), ball.transform.rotation);
            }
        }
        else if (!accelerator.notSelected)
        {
            chooseArrow();
        }
    }
    public void chooseArrow()
    {
        if (Input.GetMouseButtonDown(1))
        {
            accelerator.notSelected = true;
            accelerator.enabled = false;
            Destroy(ball.arrow.gameObject);
            chooseAccelerator();
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Destroy(ball.arrow.gameObject);
            ball.arrow.notSelected = false;
            ball.direction = ball.getMousePosition() - new Vector3(ball.transform.position.x, ball.transform.position.y, -5);
            ball.GetComponent<Rigidbody2D>().velocity = 10 * ball.direction.normalized;
            //Debug.Log(10 * ball.direction.normalized.magnitude);
            ball.notlaunched = false;
        }

    }
    public Vector3 getMousePosition_3()
    {
        mousexinUnity = Input.touches[0].position.x / Screen.width * ScreenWidthinUnity;
        mouseyinUnity = Input.touches[0].position.y / Screen.height * ScreenHeightinUnity;
        mousePosition = new Vector3(mousexinUnity, mouseyinUnity, -5);
        return mousePosition;
    }
    private void startGame()
    {
        if (!index.index_1)
        {
            continueGame();
        }
        else
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                continueGame();
            }
        }
    }
    private void checkLock()
    {
        if (lock_1.condition == 1)  //If the lock is hit with inadequate speed
        {
            index.index_1 = false;
            dialogOut("锁是关卡中的特殊元素\n需要足够大的速度才能击破它\n尝试通过调整左边的加速球位置\n来给角色球加速吧\n单击左键重试");
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
        else if (lock_1.condition == 2)
        {
            dialogOut("你已经成功的击破了锁\n继续下一关吧\n单机左键进入下一关");
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
    private void continueGame()
    {
        dialog.gameObject.SetActive(false);
        accelerator.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    private void dialogOut(string outPut)
    {
        dialog.gameObject.SetActive(true);
        dialog.setText(outPut);
        Time.timeScale = 0;
    }
}
