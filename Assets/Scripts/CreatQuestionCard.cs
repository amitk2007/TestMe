using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DanielLochner.Assets.SimpleSideMenu;

public class CreatQuestionCard : MonoBehaviour
{
    #region Vriables
    public TextMeshProUGUI questionName;
    public TextMeshProUGUI questionText;
    public Image questionImage;
    public Button[] answerButtons;
    public Sprite buttonsSprite;
    JsonReader reader;
    Texture2D texture = null;
    IEnumerator printcard;

    #region answer menu buttons
    public SimpleSideMenu Menu;
    public TextMeshProUGUI answerIsRightText;
    public Image RightImage;
    /// <summary>
    /// can the player answer the question (or is the menu on)
    /// </summary>
    bool isScreenClickable = true;

    public Button retryButton;
    #endregion

    /// <summary>
    /// The current qestion on screen
    /// </summary>
    public int currentCard = 0;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        JsonReader.SetUp(GameData.CurrentQuestionPack);
        //JsonReader.SetUp("TryJson");
        questionText.text = JsonReader.questions.Length + "";
        CreatCard(0);
    }
    #region buttons
    public void NextCardButtonClicked(bool isSkip)
    {
        //reset the buttons colors to the default color
        foreach (Button button in answerButtons)
        {
            ChangeButtonColor(button.gameObject, Color.white, new Color(0.9843137f, 0.4980392f, 0.1176471f));
            button.enabled = true;
        }

        //set the card number
        currentCard = (currentCard == JsonReader.questions.Length - 1) ? 0 : currentCard + 1;
        CreatCard(currentCard);
        isScreenClickable = true;

        //is skipping to the next question or is moving from menu
        if (isSkip == false)
        {
            Menu.Close();
        }
    }

    public void AnswerButtonClick()
    {
        if (isScreenClickable)
        {
            GameObject thisButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
            string correctAns = JsonReader.questions[currentCard].answers[JsonReader.questions[currentCard].courrectAnswer];
            if (thisButton.GetComponentInChildren<TextMeshProUGUI>().text == correctAns)
            {
                print("You are right");
                ShowAnswer(true);
                ChangeButtonColor(thisButton, Color.green, new Color(0.7843137f, 0.5019608f, 0.6509804f));
                return;
            }
            ChangeButtonColor(thisButton, new Color(1f, 0.5235849f, 0.562416f), new Color(0.8867924f, 0.7656231f, 0.6483624f));
            ShowAnswer(false);
            print("Try again");
            thisButton.GetComponent<Button>().enabled = false;
        }
    }

    public void RetryButtonClicked()
    {
        Menu.Close();
        isScreenClickable = true;
    }

    public void ChangeButtonColor(GameObject button, Color color)
    {
        button.GetComponent<Image>().color = color;
        button.transform.GetChild(1).GetComponent<Image>().color = new Color(200, 128, 166);
    }
    public void ChangeButtonColor(GameObject button, Color color, Color color1)
    {
        button.GetComponent<Image>().color = color;
        button.transform.GetChild(1).GetComponent<Image>().color = color1;
    }

    public void ShowAnswer(bool isRight)
    {
        isScreenClickable = false;

        if (isRight)
        {
            answerIsRightText.color = Color.green;
            answerIsRightText.text = "Correct";
            RightImage.enabled = true;
            retryButton.gameObject.SetActive(false);
        }
        else
        {
            answerIsRightText.color = Color.red;
            answerIsRightText.text = "Maybe next time";
            RightImage.enabled = false;
            retryButton.gameObject.SetActive(true);
        }

        Menu.Open();
    }

    public void ExitToMenu()
    {
        Application.LoadLevel("MainMenu");
    }
    #endregion

    #region CreatCard
    public void CreatCard(int cardNumber)
    {
        JsonReader.Questions question = JsonReader.questions[cardNumber];
        //Disable everything and enable just what needed
        questionText.enabled = false;
        questionImage.enabled = false;

        if (question.IsQuestionFormula == Qtype.Formula || question.IsQuestionFormula == Qtype.TextAndFormula)
        {
            questionImage.enabled = true;
            CreatImageFormula(question.formula, questionImage);
        }
        if (question.IsQuestionFormula == Qtype.Text || question.IsQuestionFormula == Qtype.TextAndFormula)
        {
            questionText.enabled = true;
            questionText.text = question.question;
        }

        EnableButton(question.answers.Length);

        if (question.IsAnswersFormula)
        {
            for (int i = 0; i < question.answers.Length; i++)
            {
                CreatImageFormula(question.answers[i], answerButtons[i].transform.GetChild(1).GetComponent<Image>());
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.answers[i];
                //answerButtons[i].transform.GetChild(0).gameObject.SetActive(false);
                //answerButtons[i].GetComponent<Image>().color = buttonsColor;
                //answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
            }
        }
        else
        {
            for (int i = 0; i < question.answers.Length; i++)
            {
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = question.answers[i];
                answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
                //*answerButtons[i].GetComponent<Image>().sprite = buttonsSprite;
                answerButtons[i].transform.GetChild(1).GetComponent<Image>().sprite = null;
                //answerButtons[i].GetComponent<Image>().color = Color.clear;
                //answerButtons[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }

    }

    public void CreatImageFormula(string formula, Image sprite)
    {
        //replace all the + and more with the corect part !!! -- sace it the correct way!
        printcard = CreatImage(formula, sprite);
        StartCoroutine(printcard);
    }

    IEnumerator CreatImage(string formula, Image sprite)
    {
        formula = formula.Replace(" ", "");
        WWW www = new WWW("http://chart.apis.google.com/chart?cht=tx&chl=" + formula);
        yield return www;

        texture = www.texture;

        Sprite s = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        //questionImage.sprite = s;
        sprite.sprite = s;
    }

    public void EnableButton(int answersNumber)
    {
        foreach (Button button in answerButtons)
        {
            button.gameObject.SetActive(true);
        }
        for (int i = answersNumber; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }
    }
    #endregion
}
