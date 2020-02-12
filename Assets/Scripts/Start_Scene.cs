using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Start_Scene : MonoBehaviour
{
    [SerializeField] Button load;
    [SerializeField] Button register;
    // Start is called before the first frame update
    void Start()
    {
        load.onClick.AddListener(Onclick_load);
        register.onClick.AddListener(Onclick_register);
    }
    void Onclick_load() {
        SceneManager.LoadScene(2);
    }
    void Onclick_register() {
        SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
