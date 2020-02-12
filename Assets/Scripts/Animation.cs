using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Animation : MonoBehaviour, IGameMouseHandle
{
    private float moveDi = 0.05f;
    private int moveIndex = 0;
    private bool moveD = true;

    [SerializeField]
    private GameObject followCamera;

    private float ScreenWidthinUnity = 30f;

    private float ScreenHeightinUnity = 10f;

    private float mousexinUnity;
    private float mouseyinUnity;
    private Vector3 mousePosition;
    private float destination;
    private float destination_1;

    private bool click = false;
    private Animator _animator;
    private Animator b1_ani;
    private Animator b2_ani;
    private Animator s1_ani;
    private Animator s2_ani;
    private GameObject board_1;
    private GameObject board_2;
    private GameObject shelf_1;
    private GameObject shelf_2;

    private bool b1 = false;
    private bool b2 = false;
    private bool s1 = false;
    private bool s2 = false;
    private bool l1 = false;

    //信篮子相关
    private GameObject letterBasketObj;

    [SerializeField]
    private Sprite[] letterShowSprite;

    //触发点
    private Vector3 b1triggerPos;

    private Vector3 b2triggerPos;
    private Vector3 s1triggerPos;
    private Vector3 s2triggerPos;
    private Vector3 letterBasketTriggerPos;
    public bool clicked = false;

    private void Start()
    {
        ScreenHeightinUnity = followCamera.GetComponent<Camera>().orthographicSize/*屏幕高度的一半*/ * 2;
        ScreenWidthinUnity = (Screen.width * 1.0f / Screen.height) * ScreenHeightinUnity;

        board_1 = transform.parent.transform.Find("board_1").gameObject;
        board_2 = transform.parent.transform.Find("board_2").gameObject;
        shelf_1 = transform.parent.transform.Find("shelf_down").gameObject;
        shelf_2 = transform.parent.transform.Find("shelf_up").gameObject;

        this.gameObject.transform.position = new Vector3(10, 2.5f, -1);
        _animator = this.GetComponent<Animator>();
        _animator.SetInteger("condition", 0);
        destination = gameObject.transform.position.x;
        b1_ani = board_1.GetComponent<Animator>();
        b2_ani = board_2.GetComponent<Animator>();
        s1_ani = shelf_1.GetComponent<Animator>();
        s2_ani = shelf_2.GetComponent<Animator>();

        b1triggerPos = GameObject.Find("triggerboard_1").transform.position;
        b2triggerPos = GameObject.Find("triggerboard_2").transform.position;
        s1triggerPos = GameObject.Find("triggershelf_down").transform.position;
        s2triggerPos = GameObject.Find("triggershelf_up").transform.position;
        letterBasketTriggerPos = GameObject.Find("trigger_letterBasket").transform.position;

        // 信篮子相关
        letterBasketObj = transform.parent.transform.Find("letter").gameObject;

        clicked = false;
    }

    private void move()
    {
        if (moveD)
        {
            gameObject.transform.position += new Vector3(moveDi, 0, 0);
        }
        else
        {
            gameObject.transform.position -= new Vector3(moveDi, 0, 0);
        }
    }

    private void bout_1()
    {
        float posX = followCamera.transform.position.x;
        Vector3 pos = board_1.transform.position;
        pos.x = posX;
        board_1.transform.position = pos;

        board_1.SetActive(true);
        b1_ani.SetInteger("condition", 1);
        b1 = true;
    }

    private void bout_2()
    {
        float posX = followCamera.transform.position.x;
        Vector3 pos = board_2.transform.position;
        pos.x = posX;
        board_2.transform.position = pos;

        board_2.SetActive(true);
        b2_ani.SetInteger("condition", 1);
        b2 = true;
    }

    private void bin_1()
    {
        b1_ani.SetInteger("condition", 2);
        b1 = false;
    }

    private void bin_2()
    {
        b2_ani.SetInteger("condition", 2);
        b2 = false;
    }

    private void sin_1()
    {
        float posX = followCamera.transform.position.x;
        Vector3 pos = shelf_1.transform.position;
        pos.x = posX;
        shelf_1.transform.position = pos;

        shelf_1.SetActive(true);
        s1_ani.SetInteger("condition", 2);
        s1 = false;
    }

    private void sin_2()
    {
        float posX = followCamera.transform.position.x;
        Vector3 pos = shelf_2.transform.position;
        pos.x = posX;
        shelf_2.transform.position = pos;

        shelf_2.SetActive(true);
        s2_ani.SetInteger("condition", 2);
        s2 = false;
    }

    private void sout_1()
    {
        s1_ani.SetInteger("condition", 1);
        s1 = true;
    }

    private void sout_2()
    {
        s2_ani.SetInteger("condition", 1);
        s2 = true;
    }

    private void letterout()
    {
        float posX = followCamera.transform.position.x;
        Vector3 pos = letterBasketObj.transform.position;
        pos.x = posX;
        letterBasketObj.transform.position = pos;

        Sprite _sp = letterShowSprite[(int)GameCofit.GetInstance().GetGameLevel()];
        letterBasketObj.GetComponent<SpriteRenderer>().sprite = _sp;

        letterBasketObj.SetActive(true);
        letterBasketObj.GetComponent<Animator>().SetInteger("condition", 1);
        l1 = true;
    }

    private void letterin()
    {
        letterBasketObj.GetComponent<Animator>().SetInteger("condition", 2);
        l1 = false;
    }

    public bool onMouseDownHandleBegin(Vector3 pos)
    {
        if (b1)
        {
            bin_1();
            return true;
        }
        if (b2)
        {
            bin_2();
            print("close b2");
            return true;
        }
        if (s1)
        {
            sin_1();
            return true;
        }
        if (s2)
        {
            sin_2();
            return true;
        }
        if (l1)
        {
            letterin();

            starGame();
            return true;
        }

        return false;
    }

    public bool onMouseDownHandle(Vector3 pos)
    {

        if (GameCofit.GetInstance().GetGameLevel() >= GameLevel.level_Max)
        {
            //游戏结束场景
            doEndGame();
            return true;
        }

        if (!b1 && inRange(transform.position.x, b1triggerPos.x - 1f, b1triggerPos.x + 1f))
        {
            //开启交互
            if (inRange(pos.x, b1triggerPos.x - 1f, b1triggerPos.x + 1f))
            {
                bout_1();
                return true;
            }
        }

        if (!b2 && inRange(transform.position.x, b2triggerPos.x - 1f, b2triggerPos.x + 0.5f))
        {
            //开启交互
            if (inRange(pos.x, b2triggerPos.x - 1f, b2triggerPos.x + 0.5f))
            {
                print("open b2");
                bout_2();
                return true;
            }
        }

        if (!s1 && inRange(transform.position.x, s1triggerPos.x - 1f, s1triggerPos.x + 1.5f))
        {
            if (inRange(pos.x, s1triggerPos.x - 1f, s1triggerPos.x + 1.5f))
            {
                if (inRange(pos.y, s1triggerPos.y - 1f, s1triggerPos.y + 1.5f))
                {
                    sout_1();
                    return true;
                }
            }
        }

        if (!s2 && inRange(transform.position.x, s2triggerPos.x - 1f, s2triggerPos.x + 1.5f))
        {
            if (inRange(pos.x, s2triggerPos.x - 1f, s2triggerPos.x + 1.5f))
            {
                if (inRange(pos.y, s2triggerPos.y - 1f, s2triggerPos.y + 1.5f))
                {
                    sout_2();
                    return true;
                }
            }
        }

        if (clicked&&!l1 && inRange(transform.position.x, letterBasketTriggerPos.x - 1.5f, letterBasketTriggerPos.x + 1.5f))
        {
            if (inRange(pos.x, letterBasketTriggerPos.x - 1.5f, letterBasketTriggerPos.x + 1.5f))
            {
                if (inRange(pos.y, letterBasketTriggerPos.y - 1.5f, letterBasketTriggerPos.y + 1.5f))
                {
                    letterout();
                    return true;
                }
            }
        }

        return false;
    }

    public bool onMouseDownHandleLater(Vector3 pos)
    {
        //role move
        destination = pos.x;
        return false;
    }

    private void Update()
    {
        if (destination != gameObject.transform.position.x)
        {
            if ((destination - gameObject.transform.position.x) > moveDi)
            {
                moveD = true;
                _animator.SetInteger("condition", 2);
                move();
            }
            else if ((destination - gameObject.transform.position.x) < -moveDi)
            {
                moveD = false;
                _animator.SetInteger("condition", 3);
                move();
            }
            else
            {
                if (moveD)
                {
                    _animator.SetInteger("condition", 0);
                }
                else
                {
                    _animator.SetInteger("condition", 1);
                }
            }
        }
    }

    private bool inRange(float x, float low, float up)
    {
        return ((low < x) && (x < up));
    }
    //这里开始小游戏
    private void starGame()
    {
        //这里写进入小游戏
        StartCoroutine(loadGame());


        //测试写法
        //下一关
    }
    IEnumerator loadGame() {
        SceneManager.LoadScene((int)GameCofit.GetInstance().GetGameLevel()+4);
        string user_gameLevel = PlayerPrefs.GetString("username")+"_gameLevel";
        PlayerPrefs.SetInt(user_gameLevel, (int)GameCofit.GetInstance().GetGameLevel());
        yield return new WaitForSeconds(0.01f);
    }
    private void doEndGame()
    {
        //游戏结束场景
        SceneManager.LoadScene("sickroom");
    }
}