using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Guide_1 : MonoBehaviour
{
    private GameObject letter;
    private Ball ball;
    private Accelerator accelerator;
    private Lock lock_1;
    private Dialog dialog;
    private Index index;
    [SerializeField] Button setting;
    [SerializeField] Button save;
    [SerializeField] Button restart;
    [SerializeField] Button resume;
    [SerializeField] Button quit;
    GameObject window;
    //They are the objects that would be used during the game
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    //The coefficients used to check the location of the mouse
    private void Awake()
    {
        
        ball = FindObjectOfType<Ball>();
        window = GameObject.Find("Button_dir");
        window.SetActive(false);
        accelerator = FindObjectOfType<Accelerator>();
        lock_1 = FindObjectOfType<Lock>();
        dialog = FindObjectOfType<Dialog>();
        dialog.gameObject.SetActive(true);
        index = FindObjectOfType<Index>();
        //Find the gameoject from the scene
        GameObject.DontDestroyOnLoad(index.gameObject);
        ball.transform.position = new Vector3(8, 1, -5); //The original position of the ball
        accelerator.transform.position = new Vector3(2, 8, -1); //The original position of the accelerator
        lock_1.transform.position = new Vector3(8, 11, -1); //The original position of the lock
        //Preserve the index when reload the scene
        setting.onClick.AddListener(Onclick_setting);
        restart.onClick.AddListener(Onclick_restart);
        resume.onClick.AddListener(Onclick_resume);
        save.onClick.AddListener(Onclick_save);
        quit.onClick.AddListener(Onclick_quit);
        letter = GameObject.Find("backLetter1");
        letter.SetActive(false);
        dialog.setText("尝试使用角色球\n来击碎屏幕上方的锁吧\n" + "轻触开始游戏");
        Time.timeScale = 0;
        
    }
    private void Onclick_quit() {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void Onclick_save() {
        string user_gameLevel = PlayerPrefs.GetString("username") + "_gameLevel";
        PlayerPrefs.SetInt(user_gameLevel, 0);
    }
    private void Onclick_resume() {
        setting.gameObject.SetActive(true);
        window.SetActive(false);
        Time.timeScale = 1;
    }
    private void Onclick_restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(4);
    }
    private void Onclick_setting() {
        window.SetActive(true);
        setting.gameObject.SetActive(false);
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
            if (!dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Moved)
            {
                accelerator.transform.position = new Vector3(2, Mathf.Clamp(getMousePosition_3().y, 1, 11),-1);
            }
            if (Input.touches[0].phase == TouchPhase.Ended)
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
        if (Input.touches[0].phase == TouchPhase.Ended)
        {
            Destroy(ball.arrow.gameObject);
            ball.arrow.notSelected = false;
            ball.direction = ball.getMousePosition() - new Vector3(ball.transform.position.x, ball.transform.position.y,-5);
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
    private void startGame() {
        if (!index.index_1)
        {
            continueGame();
        }
        else
        {
            if (dialog.gameObject.activeSelf&& Input.touches[0].phase == TouchPhase.Ended)
            {
                continueGame();
            }
        }
    }
    private void checkLock() {
        if (lock_1.condition == 1)  //If the lock is hit with inadequate speed
        {
            index.index_1 = false;
            dialogOut("锁是关卡中的特殊元素\n需要足够大的速度才能击破它\n尝试通过调整左边的加速球位置\n来给角色球加速吧\n轻触重试");
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(4);
            }
        }
        else if (lock_1.condition == 2)
        {
            letter.SetActive(true);
            Time.timeScale = 0;
            GameCofit.GetInstance().SetGameLevel((GameLevel)1);

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Time.timeScale = 1;

                SceneManager.LoadScene("Linving Room");

            }
        }
    }
    private void continueGame() {
        dialog.gameObject.SetActive(false);
        accelerator.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
    private void dialogOut(string outPut) {
        dialog.gameObject.SetActive(true);
        dialog.setText(outPut);
        Time.timeScale = 0;
    }
}
