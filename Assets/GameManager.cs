using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using System.Xml.Linq;

public class GameManager : MonoBehaviour
{
    public Question[] Questions;
    private static List<Question> UnansweredQuestions;
    private Question CurrentQuestion;

    [SerializeField]
    private Text DisplayedQuestion;
    [SerializeField]
    private Text FirstAnswerText;
    [SerializeField]
    private Text SecondAnswerText;
    [SerializeField]
    private Text ThirdAnswerText;
    [SerializeField]
    private Text FourthAnswerText;

    [SerializeField]
    private float TimeBetweenQuestions = 1f;


    public void Start()
    {
        //XDocument document = XDocument.Load()
        TextAsset textAsset = (TextAsset)Resources.Load("QuestionCatalouge.xml");
        XmlDocument xmldoc = new XmlDocument();
        xmldoc.LoadXml(textAsset.text);


        if (UnansweredQuestions == null || UnansweredQuestions.Count == 0)
        {
            UnansweredQuestions = Questions.ToList<Question>();
        }
        SetCurrentQuestion();
    }

    private void SetCurrentQuestion()
    {
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

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void UserSelectedAnswer(int selectedAnswer)
    {

        if (selectedAnswer == CurrentQuestion.correctAnswer)
        {
            Debug.Log("Right one Dude");
        }
        else
        {
            Debug.Log("Try it agaign");
        }

        StartCoroutine(TransitionToNextQuestion());
        TransitionToNextQuestion();

    }
}
