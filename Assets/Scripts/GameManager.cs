using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static bool FirstStart = true;
    private static List<XmlQuestion.Question> UnansweredQuestions;
    private static List<XmlAnswer.Answers> AnswerTexts;
    private static List<string> TrueAnswers;
    private static List<string> FalseAnswers;
    private static int NumberOfCorrectAnswers;

    private XmlQuestion.Question CurrentQuestion;

    [SerializeField]
    public Text DisplayedQuestion;
    [SerializeField]
    public TextMeshProUGUI FirstAnswerText;
    [SerializeField]
    public TextMeshProUGUI SecondAnswerText;
    [SerializeField]
    public TextMeshProUGUI ThirdAnswerText;
    [SerializeField]
    public TextMeshProUGUI FourthAnswerText;

    private int CorrectAnswer = 0;

    [SerializeField]
    private Text AnswerText;

    [SerializeField]
    private float TimeBetweenQuestions = 5f;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    public Slider ProgressBarInstance;


    public void Start()
    {
        try
        {

            if (FirstStart == true)
            {
                var serializer = new XmlSerializer(typeof(XmlQuestion.Questions));
                XmlQuestion.Questions result;
                using (var reader = new StringReader(Utilities.QuestionCatalouge))
                {
                    result = (XmlQuestion.Questions)serializer.Deserialize(reader);
                    if (UnansweredQuestions == null)
                    {
                        UnansweredQuestions = result.Question;
                    }
                }

#region AnswerImport    
                var serAnswer = new XmlSerializer(typeof(XmlAnswer.Answers));
                XmlAnswer.Answers answers;
                using (var reader = new StringReader(Utilities.AnswerCatalouge))
                {
                    answers = (XmlAnswer.Answers)serAnswer.Deserialize(reader);
                }

                TrueAnswers = new List<string>();
                FalseAnswers = new List<string>();

                foreach (var item in answers.Answer)
                {
                    if (Utilities.Str2bool(item.State))
                    {
                        TrueAnswers.Add(item.Text);
                    }
                    else
                    {
                        FalseAnswers.Add(item.Text);
                    }
                }
#endregion
                FirstStart = false;
                ProgressBarInstance.value = 0;
                NumberOfCorrectAnswers = 0;
            }
            else
            {
                ProgressBarInstance.value = NumberOfCorrectAnswers;
            }

            //end of the game
            if (UnansweredQuestions.Count == 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            SetCurrentQuestion();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.ToString());
            throw;
        }
    }

    private void SetCurrentQuestion()
    {
        //animator.SetTrigger("initState");

        int randomQuestionIndex = UnityEngine.Random.Range(0, UnansweredQuestions.Count);

        CurrentQuestion = UnansweredQuestions[randomQuestionIndex];

        DisplayedQuestion.text = CurrentQuestion.Text;

        List<Tuple<string, bool>> tmpAnswers = new List<Tuple<string, bool>>();
        tmpAnswers.Add(new Tuple<string, bool>(CurrentQuestion.Answer1.Text, Utilities.Str2bool(CurrentQuestion.Answer1.Correct)));
        tmpAnswers.Add(new Tuple<string, bool>(CurrentQuestion.Answer2.Text, Utilities.Str2bool(CurrentQuestion.Answer2.Correct)));
        tmpAnswers.Add(new Tuple<string, bool>(CurrentQuestion.Answer3.Text, Utilities.Str2bool(CurrentQuestion.Answer3.Correct)));
        tmpAnswers.Add(new Tuple<string, bool>(CurrentQuestion.Answer4.Text, Utilities.Str2bool(CurrentQuestion.Answer4.Correct)));

        // get randomized answers
        tmpAnswers.Shuffle();
        FirstAnswerText.text = tmpAnswers[0].Item1;
        SecondAnswerText.text = tmpAnswers[1].Item1;
        ThirdAnswerText.text = tmpAnswers[2].Item1;
        FourthAnswerText.text = tmpAnswers[3].Item1;

        if (tmpAnswers[0].Item2 == true)
        {
            CorrectAnswer = 1;
        }
        else if (tmpAnswers[1].Item2 == true)
        {
            CorrectAnswer = 2;
        }
        else if (tmpAnswers[2].Item2 == true)
        {
            CorrectAnswer = 3;
        }
        else if (tmpAnswers[3].Item2 == true)
        {
            CorrectAnswer = 4;
        }
    }

    IEnumerator TransitionToNextQuestion()
    {
        UnansweredQuestions.Remove(CurrentQuestion);

        yield return new WaitForSeconds(TimeBetweenQuestions);
        //animator.SetTrigger("initState");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void UserSelectedAnswer(int selectedAnswer)
    {
        // selectedAnswer could be 1,2,3 or 4
        if (selectedAnswer == CorrectAnswer)
        {
            AnswerText.text = TrueAnswers.ElementAt(UnityEngine.Random.Range(0, TrueAnswers.Count));
            ProgressBarInstance.value = ++NumberOfCorrectAnswers;
            if (NumberOfCorrectAnswers == 10)
            {
                animator.SetTrigger("answeredQuestion");
                AnswerText.text = "GRATZ DIRK, U DID IT BRO!";
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
            }
        }
        else
        {
            AnswerText.text = FalseAnswers.ElementAt(UnityEngine.Random.Range(0, FalseAnswers.Count));
            ProgressBarInstance.value = 0;
            NumberOfCorrectAnswers = 0;
        }
        animator.SetTrigger("answeredQuestion");

        StartCoroutine(TransitionToNextQuestion());
        //TransitionToNextQuestion();
    }
}
