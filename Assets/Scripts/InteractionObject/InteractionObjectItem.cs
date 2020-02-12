using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//交互物体
public class InteractionObjectItem : MonoBehaviour, IGameMouseHandle
{
    //某些关卡激活
    private Animation anima;

    [SerializeField]
    private GameLevel[] activeLevel;

    [SerializeField]
    private float tipDistanceX = 3;//距离多少时提示

    private Transform hero;

    private Vector3 selfPos;

    [SerializeField]
    private GameObject tipObj;

    [SerializeField]
    private GameObject showObj;

    private bool isShowTip = false;
    private bool isShowTalk = false;

    private float curShowTalkTime = 0;

    // Start is called before the first frame update
    private void Start()
    {
        anima = FindObjectOfType<Animation>();

        selfPos = gameObject.transform.position;
        hero = GameObject.Find("role").GetComponent<Transform>();

        if (showObj)
            showObj.SetActive(false);

        hideTip();
        hideTalk();
    }

    private bool isActive()
    {
        for (int i = 0; i < activeLevel.Length; ++i)
        {
            if (activeLevel[i] == GameCofit.GetInstance().GetGameLevel())
            {
                return true;
            }
        }

        return false;
    }

    public bool onMouseDownHandleBegin(Vector3 pos)
    {
        if (isActive())
        {
            if (isShowTalk)
            {
                hideTalk();
                print("close 交互");
                return true;
            }
        }

        return false;
    }

    public bool onMouseDownHandleLater(Vector3 pos)
    {
        return false;
    }

    public bool onMouseDownHandle(Vector3 pos)
    {
        if (isActive())
        {
            if (Mathf.Abs(hero.position.x - selfPos.x) <= tipDistanceX)
            {
                if (isShowTip && !isShowTalk)
                {
                    if (inRange(pos.x, selfPos.x - 1f, selfPos.x + 1f))
                    {
                        showTalk();

                        print("show 交互");
                        return true;
                    }
                }
            }
        }
        return false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isActive())
        {
            if (Mathf.Abs(hero.position.x - selfPos.x) <= tipDistanceX)
            {
                if (!isShowTalk && !isShowTip)
                {
                    showTip();
                    
                }
            }
            else if (isShowTip)
            {
                hideTip();
            }
        }
    }

    private void showTip()
    {
        isShowTip = true;
        tipObj.SetActive(true);
    }

    private void hideTip()
    {
        isShowTip = false;
        tipObj.SetActive(false);
    }

    private void showTalk()
    {
        isShowTalk = true;
        curShowTalkTime = 0;

        float posX = Camera.main.transform.position.x;
        Vector3 pos = showObj.transform.position;
        pos.x = posX;
        showObj.transform.position = pos;

        showObj.SetActive(true);
        showObj.GetComponent<Animator>().SetInteger("condition", 1);
        anima.clicked = true;
    }

    private void hideTalk()
    {
        isShowTalk = false;

        if (showObj)
            showObj.GetComponent<Animator>().SetInteger("condition", 2);
    }

    private bool inRange(float x, float low, float up)
    {
        return ((low < x) && (x < up));
    }
}