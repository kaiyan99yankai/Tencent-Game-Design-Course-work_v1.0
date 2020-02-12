using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Guide_4 : MonoBehaviour
{
    // Start is called before the first frame update
    public Ball ball;
    //[SerializeField] private int accelerationNumber = 1;
    private Acceleration[] acceleration;
    private bool jetSelected = false;
    private bool directionSelected = false;
    [SerializeField] Button setting;
    [SerializeField] Button save;
    [SerializeField] Button restart;
    [SerializeField] Button resume;
    [SerializeField] Button quit;
    GameObject window;
    [SerializeField] private float ScreenWidthinUnity = 16f;
    [SerializeField] private float ScreenHeightinUnity = 9f;
    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private Vector3 fixPosition;
    private Jet[] jets;
    private Vector3 originalVelocity;
    private int index = 0;
    private Dialog dialog;
    private bool flag = false;
    private bool use = false;
    public bool hit_1 = false;
    public bool hit_2 = false;
    private GameObject letter;
    void Start()
    {
        letter = GameObject.Find("backLetter4");
        letter.SetActive(false);
        ball = FindObjectOfType<Ball>();
        window = GameObject.Find("Button_dir");
        window.SetActive(false);
        ball.gameObject.transform.position = new Vector3(8,4,-5);
        acceleration = FindObjectsOfType<Acceleration>();
        acceleration[0].transform.position = new Vector3(2, 4, -5);
        acceleration[1].transform.position = new Vector3(14, 4, -5);
        acceleration[0].gameObject.SetActive(false);
        acceleration[1].gameObject.SetActive(false);
        jets = FindObjectsOfType<Jet>();
        jets[0].gameObject.transform.position = new Vector3(3.5f, 4,-5);
        jets[1].gameObject.transform.position = new Vector3(12.5f,4,-5);
        dialog = FindObjectOfType<Dialog>();
        setting.onClick.AddListener(Onclick_setting);
        restart.onClick.AddListener(Onclick_restart);
        resume.onClick.AddListener(Onclick_resume);
        save.onClick.AddListener(Onclick_save);
        quit.onClick.AddListener(Onclick_quit);
        dialogOut("黄色的墙体会减速你的角色球\n尽量避开他们");
        Time.timeScale = 0;
    }
    private void Onclick_quit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    private void Onclick_save()
    {
        string user_gameLevel = PlayerPrefs.GetString("username") + "_gameLevel";
        PlayerPrefs.SetInt(user_gameLevel, 3);
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
        SceneManager.LoadScene(7);
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
        StartCoroutine(start());
        if(!ball.notlaunched) useJet();
        if (dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended)
        {
            ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
            continueGame();
        }
        if (hit_1 && hit_2)
        {
            letter.SetActive(true);
            Time.timeScale = 0;
            //GameCofit.GetInstance().SetGameLevel(GameCofit.GetInstance().GetGameLevel() + 1);

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                Time.timeScale = 1;
                GameCofit.GetInstance().SetGameLevel((GameLevel)4);
                SceneManager.LoadScene("Linving Room");

            }
        }
    }
    IEnumerator start(){
        if (dialog.gameObject.activeSelf && Input.touches[0].phase == TouchPhase.Ended && !flag)
        {
            dialog.gameObject.SetActive(false);
            Time.timeScale = 1;
            flag = true;
        }
        yield return new WaitForSeconds(0.01f);
        if (ball.notlaunched && flag)
        {
            if (acceleration[0].notSelected && acceleration[1].notSelected)
            {
                acceleration[0].gameObject.SetActive(true);
                acceleration[0].transform.position = new Vector3(2, Mathf.Clamp(getMousePosition().y, 1.5f, 4.2f), -5);
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    acceleration[0].notSelected = false;
                }
                yield return new WaitForSeconds(0.01f);
            }
            else if (!acceleration[0].notSelected && acceleration[1].notSelected)
            {
                acceleration[1].gameObject.SetActive(true);
                acceleration[1].transform.position = new Vector3(14, Mathf.Clamp(getMousePosition().y, 1.5f, 4.2f), -5);
                if (Input.touches[0].phase == TouchPhase.Ended)
                {
                    acceleration[1].notSelected = false;
                    ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), ball.transform.rotation);

                }
                yield return new WaitForSeconds(0.1f);
            }
            else if (!acceleration[0].notSelected && !acceleration[1].notSelected) {
                chooseArrow();
            }
        }
        yield return new WaitForSeconds(0.01f);
    }
    void chooseArrow()
    {
        if (ball.notlaunched&& Input.touches[0].phase == TouchPhase.Ended)
        {
            Destroy(ball.arrow.gameObject);
            ball.arrow.gameObject.SetActive(false);
            ball.arrow.notSelected = false;
            ball.direction = ball.getMousePosition() - new Vector3(ball.transform.position.x, ball.transform.position.y, -5);
            ball.GetComponent<Rigidbody2D>().velocity = 10 * ball.direction.normalized;
            //Debug.Log(10 * ball.direction.normalized.magnitude);
            ball.notlaunched = false;
        }
    }
    public Vector3 getMousePosition()
    {
        mousexinUnity = Input.touches[0].position.x / Screen.width * ScreenWidthinUnity;
        mouseyinUnity = Input.touches[0].position.y / Screen.height * ScreenHeightinUnity;
        mousePosition = new Vector3(mousexinUnity, mouseyinUnity, -5);
        return mousePosition;
    }
    private void useJet()
    {
        for (int i = 0; i < 2-index; i++)
        {
            if (jets[i].distance <= 0.5f)
            {
                ball.notlaunched = true;
                if (index == 0)
                {
                    dialog.gameObject.SetActive(true);
                    dialogOut("喷气背包的效果和休整区类似\n而且你的角色球也同时或被加速\n点击按钮继续");
                    ball.transform.position = jets[i].transform.position;
                    originalVelocity = ball.GetComponent<Rigidbody2D>().velocity;
                    ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    index++;
                    use = true;
                    Destroy(jets[i].gameObject);
                    if (index == 1 && i == 0)
                    {
                        jets[0] = jets[1];
                    }
                }
                else {
                    ball.transform.position = jets[i].transform.position;
                    originalVelocity = ball.GetComponent<Rigidbody2D>().velocity;
                    ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                    index++;
                    use = true;
                    Destroy(jets[i].gameObject);
                    if (index == 1 && i == 0)
                    {
                        jets[0] = jets[1];
                    }
                    ball.arrow = Instantiate(ball.arrowOriginal, new Vector3(ball.transform.position.x, ball.transform.position.y, -4), Quaternion.identity);
                }
            }
        }
    }
    private void dialogOut(string s)
    {
        dialog.gameObject.SetActive(true);
        dialog.setText(s);
        Time.timeScale = 0;
    }
    public void continueGame()
    {
        dialog.gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
