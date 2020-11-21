using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static string testingUsername = "tester";
    public static string testingPassword = "password";

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    #region Username & Password

    public static void ResetUsernameAndPassword()
    {
        PlayerPrefs.SetString("Username", "");
        PlayerPrefs.SetString("Password", "");
    }

    public static bool IsUsernameAndPassword(string username, string password)
    {
        if (username == testingUsername && password == testingPassword)
        {
            PlayerPrefs.SetString("Username", testingUsername);
            PlayerPrefs.SetString("Password", testingPassword);
            return true;
        }
        else if (PlayerPrefs.GetString("Password") == testingPassword)
        {
            return true;
        }

        return false;
    }

    #endregion
}
