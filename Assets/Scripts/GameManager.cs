using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Xml.Linq;
using System.Threading;
using TMPro;

public class GameManager : MonoBehaviour
{
    public Question[] Questions;
    private static List<Question> UnansweredQuestions;
    private Question CurrentQuestion;

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

    [SerializeField]
    private Text AnswerText;

    [SerializeField]
    private float TimeBetweenQuestions = 5f;

    [SerializeField]
    private Animator animator;

    public void Start()
    {
        //XDocument document = XDocument.Load()
        //TextAsset textAsset = (TextAsset)Resources.Load("QuestionCatalouge.xml");
        //XmlDocument xmldoc = new XmlDocument();
        //xmldoc.LoadXml(textAsset.text);


        if (UnansweredQuestions == null || UnansweredQuestions.Count == 0)
        {
            UnansweredQuestions = Questions.ToList<Question>();
        }
        SetCurrentQuestion();
    }

    private void SetCurrentQuestion()
    {
        //animator.SetTrigger("initState");

        int randomQuestionIndex = UnityEngine.Random.Range(0, UnansweredQuestions.Count);

        CurrentQuestion = UnansweredQuestions[randomQuestionIndex];

        DisplayedQuestion.text = CurrentQuestion.question;
        FirstAnswerText.text = CurrentQuestion.answer1;
        SecondAnswerText.text = CurrentQuestion.answer2;
        ThirdAnswerText.text = CurrentQuestion.answer3;
        FourthAnswerText.text = CurrentQuestion.answer4;

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
        if (selectedAnswer == CurrentQuestion.correctAnswer)
        {
            AnswerText.text = "Right one Dude";
        }
        else
        {
            AnswerText.text = "Try it agaign, mongo";
        }
        animator.SetTrigger("answeredQuestion");

        StartCoroutine(TransitionToNextQuestion());
        //TransitionToNextQuestion();

    }
}
