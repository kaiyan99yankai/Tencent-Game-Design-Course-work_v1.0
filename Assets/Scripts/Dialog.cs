using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public void setText(string outPut)  //The only function here is used to change the text shown in the dialog
    {
        GameObject.Find("/Dialog/Canvas/Text").GetComponent<Text>().text = outPut;
    }
}
