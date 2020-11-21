using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DanielLochner.Assets.SimpleSideMenu;

public class CreatQuestionCard : MonoBehaviour
{
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
    #endregion

    public int currentCard = 0;

    // Start is called before the first frame update
    void Start()
    {
        JsonReader.SetUp("TryJson");
        questionText.text = JsonReader.questions.Length + "";
        CreatCard(0);
    }
    #region buttons
    public void NextCardButtonClicked(bool isSkip)
    {
        //set the card number
        currentCard = (currentCard == JsonReader.questions.Length - 1) ? 0 : currentCard + 1;
        CreatCard(currentCard);
        isScreenClickable = true;
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
            for (int i = 0; i < JsonReader.questions[currentCard].answers.Length; i++)
            {
                if (thisButton.GetComponentInChildren<TextMeshProUGUI>().text == correctAns)
                {
                    print("You are right");
                    ShowAnswer(true);
                    return;
                }
            }
            ShowAnswer(false);
            print("Try again");
        }
    }

    public void ShowAnswer(bool isRight)
    {
        isScreenClickable = false;

        if (isRight)
        {
            answerIsRightText.color = Color.green;
            answerIsRightText.text = "Correct";
            RightImage.enabled = true;
        }
        else
        {
            answerIsRightText.color = Color.red;
            answerIsRightText.text = "Maybe next time";
            RightImage.enabled = false;
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

        if (question.IsQuestionFormula)
        {
            CreatImageFormula(question.question, questionImage);
            questionText.enabled = false;
        }
        else
        {
            questionText.text = question.question;
            questionImage.enabled = false;
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
        for (int i = answersNumber; i < answerButtons.Length; i++)
        {
            answerButtons[i].gameObject.SetActive(false);
        }
    }
    #endregion
}
