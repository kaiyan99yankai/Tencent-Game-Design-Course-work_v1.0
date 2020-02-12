using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followCamera : MonoBehaviour
{
    [SerializeField]
    private GameObject followTarget;

    [SerializeField]
    private SpriteRenderer imgbg;

    private float MinCameraX = 0;
    private float MaxCameraX = 0;

    // Start is called before the first frame update
    private void Start()
    {
        float ScreenHeigth = GetComponent<Camera>().orthographicSize/*屏幕高度的一半*/ * 2;
        float ScreenWidth = (Screen.width * 1.0f / Screen.height) * ScreenHeigth;

        Vector3 imgBgPos = imgbg.transform.position;
        Vector2 size = imgbg.bounds.size;

        //MinCameraX = imgBgPos.x - size.x / 2 + ScreenWidth / 2;
        MinCameraX = imgBgPos.x + ScreenWidth / 2; //锚点 x=0
        //MaxCameraX = imgBgPos.x + size.x / 2 - ScreenWidth / 2;
        MaxCameraX = imgBgPos.x + size.x - ScreenWidth / 2;
    }

    private void LateUpdate()
    {
        updateCameraFollow();
    }

    private void updateCameraFollow()
    {
        float unitPosX = followTarget.transform.localPosition.x;
        unitPosX = unitPosX < MinCameraX ? MinCameraX : unitPosX;
        unitPosX = unitPosX > MaxCameraX ? MaxCameraX : unitPosX;

        Vector3 pos = transform.localPosition;
        pos.x = unitPosX;
        transform.localPosition = pos;
    }
}