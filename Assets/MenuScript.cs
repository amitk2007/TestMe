using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour
{
    public TextMeshProUGUI[] comingSoonTexts;
    public TMP_InputField username;
    public TMP_InputField password;
    public TextMeshProUGUI wrongInput;
    public DanielLochner.Assets.SimpleScrollSnap.SimpleScrollSnap simpleSnap;

    // Start is called before the first frame update
    void Start()
    {
        foreach (TextMeshProUGUI comingSoonText in comingSoonTexts)
        {
            if (comingSoonText != null)
            {
                comingSoonText.enabled = false;
            }
        }

        if (wrongInput != null)
        {
            wrongInput.enabled = false;
            ///only for testings
            PlayerData.ResetUsernameAndPassword();
            ///

            if (PlayerData.IsUsernameAndPassword("", ""))
            {
                Application.LoadLevel("MainMenu");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InputFieldCharacterLimit(int limit)
    {
        if (username.text.Length > limit)
        {
            username.text = username.text.Substring(0, limit);
        }
        if (password.text.Length > limit)
        {
            password.text = password.text.Substring(0, limit);
        }
    }

    public void LogIn()
    {
        if (PlayerData.IsUsernameAndPassword(username.text, password.text))
        {
            Application.LoadLevel("MainMenu");
        }
        else
        {
            wrongInput.enabled = true;
        }
    }

    public void ComingSoon()
    {
        GameObject text = EventSystem.current.currentSelectedGameObject.transform.Find("Coming Soon").gameObject;
        if (text != null)
        {
            text.GetComponent<TextMeshProUGUI>().enabled = true;
        }
    }

    public void CloseComingSoonCanvas()
    {
        //comingSoonCanvas.SetActive(false);
    }

    public void OpenLevels()
    {
        Application.LoadLevel("Levels");
    }

    public void OpenCards()
    {
        GameData.CurrentQuestionPack  = simpleSnap.Panels[simpleSnap.CurrentPanel].name;
        Application.LoadLevel("Card");
    }
}
