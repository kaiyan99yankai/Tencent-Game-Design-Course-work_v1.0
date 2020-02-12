using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class End : MonoBehaviour
{

    public VideoPlayer vPlayer;
    private bool playing=true;
    void Start()
    {
        StartCoroutine(quit());
    }
    IEnumerator quit() {
        yield return new WaitForSeconds(30);
        SceneManager.LoadScene(0);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
