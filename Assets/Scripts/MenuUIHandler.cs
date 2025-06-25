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
        string playerName = nameInputField.text;
        //string playerName = PlayerPrefs.GetString("PlayerName", "Unknown");
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        Debug.Log($"Player: {playerName}, High Score: {highScore}");

        PlayerData.PlayerName = nameInputField.text;

        PlayerPrefs.SetString("PlayerName", playerName);
        Debug.Log("Saving name: " + playerName);
        PlayerPrefs.Save(); // force the write to disk

        SceneManager.LoadScene(1);
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
