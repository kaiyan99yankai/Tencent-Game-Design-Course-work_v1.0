
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
/// <summary>
/// 此脚本挂载在pan_Login_Window上
/// </summary>
public class Register : MonoBehaviour
{
    //  为了方便在此我就选择拖拽的方式
    //  三个输入表格，为了引用表格中输入的文本
    [SerializeField]
    InputField m_inpfiName;
    [SerializeField]
    InputField m_inpufiPwd;
    [SerializeField]
    InputField m_ConfimPwd;
    //  完成按钮
    [SerializeField]
    Button m_Wancheng;
    //  提示文本
    [SerializeField]
    Text m_Tips;
    //  登陆界面
    [SerializeField]
    GameObject m_pan_Login_Window;
    //  可见密码
    [SerializeField]
    Toggle m_SeePwd;
    private void Start()
    {
        m_SeePwd.onValueChanged.AddListener(OnSeePwd);
        m_inpfiName.onEndEdit.AddListener(OnNameEnd);
        m_ConfimPwd.onEndEdit.AddListener(OnPwdEnd);
        m_Wancheng.onClick.AddListener(OnWancheng);
    }
    //  使密码可见
    private void OnSeePwd(bool arg0)
    {
        //  转换密码类型
        m_inpufiPwd.contentType = arg0 ? InputField.ContentType.Standard : InputField.ContentType.Password;
        m_inpufiPwd.Select();
    }

    //  当结束编辑名字的时候，查看是否已经存在该用户名
    private void OnNameEnd(string arg0)
    {
        if (PlayerPrefs.HasKey(m_inpfiName.text))
        {
            m_Tips.text = "该用户名已经存在";
        }
    }

    private void OnPwdEnd(string arg0)
    {
        if (m_ConfimPwd.text != m_inpufiPwd.text)
        {
            m_Tips.text = "前后密码输入不一致";
        }
    }

    private void OnWancheng()
    {
        //  playerprefs中不存在这个名字，两次密码输入一致，存入Playerprefs，到登陆界面
        if (!PlayerPrefs.HasKey(m_inpfiName.text))
        {
            if (m_inpufiPwd.text == m_ConfimPwd.text)
            {
                PlayerPrefs.SetString(m_inpfiName.text, m_ConfimPwd.text);
                SceneManager.LoadScene(0);
            }
        }
    }
}