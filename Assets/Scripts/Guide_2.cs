using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Guide_2 : MonoBehaviour
{
    public bool hit_1=false;
    public bool hit_2 = false;
    private Ball ball;
    private Accelerator accelerator;
    private Lock[] lockComponent;
    private Dialog dialog;
    private Sting[] stings;
    private Index index;
    //They are the objects that would be used in the game
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private Vector3 fixPosition;
    [SerializeField] Button setting;
    [SerializeField] Button save;
    [SerializeField] Button restart;
    [SerializeField] Button resume;
    [SerializeField] Button quit;
    GameObject window;
    private GameObject letter;
    //The coefficients used to check the location of the mouse
    void Start()
    {
        letter = GameObject.Find("backLetter2");
        letter.SetActive(false);
        ball = FindObjectOfType<Ball>();
        window = GameObject.Find("Button_dir");
        window.SetActive(false);
        accelerator = FindObjectOfType<Accelerator>();
        lockComponent = FindObjectsOfType<Lock>();
        stings = FindObjectsOfType<Sting>();
        dialog = FindObjectOfType<Dialog>();
        index = FindObjectOfType<Index>();
        //Find the gameoject from the scene
        GameObject.DontDestroyOnLoad(index.gameObject);
        //Preserve the index when reload the scene
        dialog.setText("在这一关你将遇到新的挑战\n" + "单机左键开始游戏吧");
        Time.timeScale = 0;
        setting.onClick.AddListener(Onclick_setting);
        restart.onClick.AddListener(Onclick_restart);
        resume.onClick.AddListener(Onclick_resume);
        save.onClick.AddListener(Onclick_save);
        quit.onClick.AddListener(Onclick_quit);
    }
    private void Onclick_quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void Onclick_save()
    {
        string user_gameLevel = PlayerPrefs.GetString("username") + "_gameLevel";
        PlayerPrefs.SetInt(user_gameLevel, 1);
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
        SceneManager.LoadScene(5);
    }
    private void Onclick_setting()
    {
        window.SetActive(true);
        setting.gameObject.SetActive(false);
        Time.timeScale = 0;
    }
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(mainGame());
        checkSting();
        if (hit_1 && hit_2) {
            letter.SetActive(true);
            Time.timeScale = 0;
            //GameCofit.GetInstance().SetGameLevel(GameCofit.GetInstance().GetGameLevel() + 1);

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Time.timeScale = 1;
                GameCofit.GetInstance().SetGameLevel((GameLevel)2);
                SceneManager.LoadScene("Linving Room");

            }
        }
    }
    IEnumerator mainGame()
    {
        startGame();
        yield return new WaitForSeconds(0.01f); //use this function to let the two operation separate
        if (ball.notlaunched)
        {
            chooseAccelerator();    //The fuction to choose the position of the accelerato 
        }
        yield return null;
    }
    private void checkSting() {
        for (int i = 0; i < 5; i++)
        {
            if (stings[i].getCol)
            {
                index.index_1 = false;
                if (index.index_2)
                {
                    dialogOut("红的标志表示禁止通行\n边框也有危险\n小心绕开他们\n单机左键重新试试");
                }
                else{
                    Time.timeScale = 0;
                }
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene(5);
                }
            }
        }
    }
    public void chooseAccelerator()
    {
        //dialog.gameObject.SetActive(false);
        if (accelerator.notSelected)
        {
            accelerator.enabled = true;
            if (Input.touches[0].phase == TouchPhase.Moved)
            {
                accelerator.transform.position = new Vector3(Mathf.Clamp(getMousePosition_3().x, 4, 12), 5, -5);
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
            if (dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended)
            {
                continueGame();
            }
        }
    }
    private void continueGame()
    {
        dialog.gameObject.SetActive(false);
        accelerator.gameObject.SetActive(true);
        Time.timeScale = 1;
        //index.index_2 = false;
    }
    private void dialogOut(string outPut)
    {
        dialog.gameObject.SetActive(true);
        dialog.setText(outPut);
        Time.timeScale = 0;
    }
}
