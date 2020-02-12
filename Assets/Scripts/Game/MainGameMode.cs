using UnityEngine;
using System.Collections;

public class MainGameMode : MonoBehaviour
{
    private ArrayList mouseHandles;

    [SerializeField]
    private float mouseCd = 0.5f;

    private float curCd = 0.0f;
    private bool isCd = false;

    // Use this for initialization
    void Start()
    {
        mouseHandles = new ArrayList(gameObject.GetComponentsInChildren<IGameMouseHandle>());
        curCd = 0.0f;
        isCd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCd)
        {
            //避免点击太快
            curCd += Time.deltaTime;
            if(curCd >= mouseCd)
            {
                isCd = false;
            }
        }

        if (!isCd && Input.touches[0].phase==TouchPhase.Began)
        {
            isCd = true;
            Vector3 pos = getMousePosition_3();

            foreach (IGameMouseHandle handle in mouseHandles)
            {
                if (handle.onMouseDownHandleBegin(pos))
                {
                    //拦截事件
                    return;
                }
            }

            foreach (IGameMouseHandle handle in mouseHandles)
            {
                if (handle.onMouseDownHandle(pos))
                {
                    //拦截事件
                    return;
                }
            }

            foreach (IGameMouseHandle handle in mouseHandles)
            {
                if (handle.onMouseDownHandleLater(pos))
                {
                    //拦截事件
                    return;
                }
            }
        }
    }

    public Vector3 getMousePosition_3()
    {
        return Camera.main.ScreenToWorldPoint(Input.touches[0].position);
    }
}
