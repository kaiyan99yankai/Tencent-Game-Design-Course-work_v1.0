using UnityEngine;
using System.Collections;

public interface IGameMouseHandle
{
    bool onMouseDownHandleBegin(Vector3 pos);
    bool onMouseDownHandle(Vector3 pos);
    bool onMouseDownHandleLater(Vector3 pos);
}
