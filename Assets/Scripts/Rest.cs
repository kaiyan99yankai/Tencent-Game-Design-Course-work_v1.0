using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Rest : MonoBehaviour
{
    public bool flag = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        flag = true;
        Debug.Log(1);
    }
}
