using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour,IGameMouseHandle
{

    [SerializeField]
    private GameObject resultPage;

    [SerializeField]
    private GameObject[] talkObjs;

    private GameObject curShowObj;

    private int curIndex = 0;
    private int maxIndex = 0;
    private bool isShowResult = false;
    private bool isShowVedio = false;

    // Use this for initialization
    void Start()
    {
        maxIndex = talkObjs.Length;
        curIndex = -1;
        isShowResult = false;
        isShowVedio = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool onMouseDownHandle(Vector3 pos)
    {
        return false;
    }

    public bool onMouseDownHandleLater(Vector3 pos)
    {
        curIndex++;
        print("onMouseDownHandleLateronMouseDownHandleLateronMouseDownHandleLateronMouseDownHandleLater");
        print(curIndex + "  "+ maxIndex);

        if (curIndex < maxIndex)
        {
            if(curShowObj)
            {
                curShowObj.SetActive(false);
            }

            curShowObj = talkObjs[curIndex];
            print(curShowObj);

            if (curShowObj)
            {
                curShowObj.SetActive(true);
            }
        }
        else if(!isShowResult)
        {
            if (curShowObj)
            {
                curShowObj.SetActive(false);
            }

            resultPage.SetActive(true);
            resultPage.GetComponent<Animator>().SetInteger("condition", 1);
            isShowResult = true;
        }
        else
        {
            SceneManager.LoadScene("cg");
        }

        return false;
    }

    public bool onMouseDownHandleBegin(Vector3 pos)
    {
        return false;
    }
}
