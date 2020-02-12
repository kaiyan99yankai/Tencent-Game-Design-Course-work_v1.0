using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Guide_3 : MonoBehaviour
{
    private Ball ball;
    private Lock[] lockComponent;
    private Dialog dialog;
    private int time = 0;
    private Sting sting;
    private Rest[] rests;
    private Index index;
    private bool hasStarted = false;
    [SerializeField] Button setting;
    [SerializeField] Button save;
    [SerializeField] Button restart;
    [SerializeField] Button resume;
    [SerializeField] Button quit;
    GameObject window;
    //They are the objects that would be used in the game
    private bool firstTime = true;
    //Variable to record the time the ball enter the rest area
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private Vector3 fixPosition;
    public bool hit_1 = false;
    public bool hit_2 = false;
    private GameObject letter;
    //The coefficients used to check the location of the mouse
    void Start()
    {
        letter = GameObject.Find("backLetter3");
        letter.SetActive(false);
        ball = FindObjectOfType<Ball>();
        window = GameObject.Find("Button_dir");
        window.SetActive(false);
        lockComponent = FindObjectsOfType<Lock>();
        rests = FindObjectsOfType<Rest>();
        sting = FindObjectOfType<Sting>();
        dialog = FindObjectOfType<Dialog>();
        dialog.gameObject.SetActive(true);
        index = FindObjectOfType<Index>();
        setting.onClick.AddListener(Onclick_setting);
        restart.onClick.AddListener(Onclick_restart);
        resume.onClick.AddListener(Onclick_resume);
        save.onClick.AddListener(Onclick_save);
        quit.onClick.AddListener(Onclick_quit);
        dialog.setText("在这一关卡中\n蓝色的障碍物代表砖块\n第一次碰撞会击碎砖块");
        Time.timeScale = 0;
        hasStarted = false;
        GameObject.DontDestroyOnLoad(index.gameObject);
        //ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, 0), Quaternion.identity);
    }
    private void Onclick_quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void Onclick_save()
    {
        string user_gameLevel = PlayerPrefs.GetString("username") + "_gameLevel";
        PlayerPrefs.SetInt(user_gameLevel, 2);
    }
    private void Onclick_resume()
    {
        setting.gameObject.SetActive(true);
        window.SetActive(false);
        Time.timeScale = 1;
    }
    private void Onclick_restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(6);
    }
    private void Onclick_setting()
    {
        window.SetActive(true);
        setting.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    void Update()
    {
        StartCoroutine(mainGame());
        checkRests();
        checkSting();
        if (hit_1 && hit_2)
        {
            letter.SetActive(true);
            Time.timeScale = 0;
            //GameCofit.GetInstance().SetGameLevel(GameCofit.GetInstance().GetGameLevel() + 1);

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Time.timeScale = 1;
                GameCofit.GetInstance().SetGameLevel((GameLevel)3);
                SceneManager.LoadScene("Linving Room");

            }
        }
    }
    private void checkSting() {
        if (sting.getCol)
        {
            index.index_3 = 2;
            SceneManager.LoadScene(6);
        }
    }
    private void checkRests()
    {
        for (int i = 0; i < 3 - time; i++)
        {
            if (rests[i].flag)
            {
                Debug.Log(i);
                ball.notlaunched = true;
                if (index.index_2 && firstTime)
                {
                    index.index_3 = 1;
                    dialogOut("如果你进入了休整区\n你将可以再次发射你的角色球\n轻触继续");
                    firstTime = false;
                    Destroy(rests[i].gameObject);
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    time++;
                    //ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                }
                else
                {
                    index.index_3 = 1;
                    time++;
                    Destroy(rests[i].gameObject);
                    ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                    ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                }
                for (int j = i; j < 3; j++)
                {
                    rests[j] = rests[j + 1];
                }
            }
        }
    }
    IEnumerator mainGame() {
        startGame();
        yield return new WaitForSeconds(0.01f);
        if (ball.notlaunched)
        {
            chooseArrow();
        }
        yield return null;
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
    public void continueGame() {    //在这一关中没有accelerator，但要在这一步中生成一个arrow
        //ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, 0), Quaternion.identity);
        dialog.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
    private void startGame()
    {
        if (index.index_3 == 0)
        {
            if (dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended && !hasStarted)
            {
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                continueGame();
                hasStarted = true;
            }
        }
        else if (index.index_3 == 1)
        {
            if(dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended) {
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                continueGame();
                index.index_2 = false;
            }
        }
        else if (index.index_3 == 2)
        {
            
            if (dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended && !hasStarted)
            {
                ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                continueGame();
                hasStarted = true;
            }
        }
    }
    private void dialogOut(string outPut)
    {
        dialog.gameObject.SetActive(true);
        dialog.setText(outPut);
        Time.timeScale = 0;
    }
}
