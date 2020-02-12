using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]int LevelChoice;
    int CurrentSceneIndex;
    public void LoadNextScene()
    {
        CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(CurrentSceneIndex + 1);
    }
    public void LoadStartScene()
    {
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadScenewithDifferentLevels()
    {
        SceneManager.LoadScene(CurrentSceneIndex + LevelChoice +1);
    }
    public void SetLevelto1()
    {
        LevelChoice = 0;
    }
    public void SetLevelto2()
    {
        LevelChoice = 1;
    }
    public void SetLevelto3()
    {
        LevelChoice = 2;
    }
}
