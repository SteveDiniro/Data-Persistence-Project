using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class MenuUIHandler : MonoBehaviour
{
    public InputField nameInputField;

    public void StartNew()
    {
        PlayerData.PlayerName = nameInputField.text;
        SceneManager.LoadScene(1);
    }

    public void StartGame()
    {
        // PlayerData.PlayerName = nameInputField.text;
        // SceneManager.LoadScene("main");
    }

    public void Exit()
    {
        #if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
        #else
                Application.Quit();
        #endif
    }

}
