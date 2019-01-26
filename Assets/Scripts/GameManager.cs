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
    //public Question[] Questions;
    private static List<XmlQuestion.Question> UnansweredQuestions;
    private static List<XmlAnswer.Answers> AnswerTexts;
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

    public void Start()
    {
        try
        {
            var serializer = new XmlSerializer(typeof(XmlQuestion.Questions));
            XmlQuestion.Questions result;
            using (var reader = new StringReader(QuestionCatalouge))
            {
                result = (XmlQuestion.Questions)serializer.Deserialize(reader);
            }

            if (UnansweredQuestions == null)
            {
                UnansweredQuestions = result.Question;
            }
            else if (UnansweredQuestions.Count == 0)
            {
                //end of the game
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }

            // var serAnswer = new XmlSerializer(typeof(XmlAnswer.Answers));
            // XmlAnswer.Answers answers;
            // using (var reader = new StringReader(AnswerCatalouge))
            // {
            //     answers = (XmlAnswer.Answers)serializer.Deserialize(reader);
            // }

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
        FirstAnswerText.text = CurrentQuestion.Answer1.Text;
        SecondAnswerText.text = CurrentQuestion.Answer2.Text;
        ThirdAnswerText.text = CurrentQuestion.Answer3.Text;
        FourthAnswerText.text = CurrentQuestion.Answer4.Text;

        if (CurrentQuestion.Answer1.Correct == "true" || CurrentQuestion.Answer1.Correct == "True")
        {
            CorrectAnswer = 1;
        }
        else if (CurrentQuestion.Answer2.Correct == "true" || CurrentQuestion.Answer2.Correct == "True")
        {
            CorrectAnswer = 2;
        }
        else if (CurrentQuestion.Answer3.Correct == "true" || CurrentQuestion.Answer3.Correct == "True")
        {
            CorrectAnswer = 3;
        }
        else if (CurrentQuestion.Answer4.Correct == "true" || CurrentQuestion.Answer4.Correct == "True")
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

    string AnswerCatalouge = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<Answers>
<Answer Text = ""Von wem stammt die Lebensweisheit: Oamoi dipfeen geht imma?"" State=""True""/>
 </Answers>";

    string QuestionCatalouge = @"<?xml version=""1.0"" encoding=""UTF-8"" standalone=""yes"" ?>
<Questions>
<Question Text = ""Von wem stammt die Lebensweisheit: Oamoi dipfeen geht imma?"" >
    <Answer1 Text=""Rohwedder "" Correct=""false""></Answer1>
    <Answer2 Text = ""Stoppel"" Correct=""False""></Answer2>
    <Answer3 Text = ""Fischbacher"" Correct=""True""></Answer3>
    <Answer4 Text = ""Heinze"" Correct=""False""></Answer4>
 </Question>
<Question Text = ""Wer hat damals auf der Wiesn unterm Tisch gekotzt?"" >
    <Answer1 Text=""Rohwedder "" Correct=""false""></Answer1>
    <Answer2 Text = ""Stoppel"" Correct=""False""></Answer2>
    <Answer3 Text = ""Fischbacher"" Correct=""False""></Answer3>
    <Answer4 Text = ""Heinze"" Correct=""True""></Answer4>
</Question>
<Question Text = ""Wieviel Watt hatte Glockenjunges Basemachine auf der SMS?"" >
    <Answer1 Text=""3kW"" Correct=""true""></Answer1>
    <Answer2 Text = ""100kW"" Correct=""false""></Answer2>
    <Answer3 Text = ""300W"" Correct=""false""></Answer3>
    <Answer4 Text = ""2kW"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was ist mit dem Kühlschrank auf der SMS passiert?"" >
    <Answer1 Text=""Wurde über den Zeltplatz gassi geführt"" Correct=""true""></Answer1>
    <Answer2 Text = ""Wurde gesprengt"" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer ist auf der Betriebsversammlung verschollen?"" >
    <Answer1 Text=""Franz"" Correct=""true""></Answer1>
    <Answer2 Text = ""Dirk"" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wie hieß der Mitbewohner vom Christoph?"" >
    <Answer1 Text=""Johannes"" Correct=""true""></Answer1>
    <Answer2 Text = """" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer hat seinen Pensi auf der Betriebsversammlung entblöst?"" >
    <Answer1 Text=""Dirk"" Correct=""true""></Answer1>
    <Answer2 Text = """" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Um wieviel Uhr geht Flo immer zum kacken?"" >
    <Answer1 Text=""13 Uhr"" Correct=""true""></Answer1>
    <Answer2 Text = """" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wieviel Katzen hatte Franz schwuler Mitbewohner?"" >
    <Answer1 Text=""3"" Correct=""true""></Answer1>
    <Answer2 Text = ""100"" Correct=""false""></Answer2>
    <Answer3 Text = ""1"" Correct=""false""></Answer3>
    <Answer4 Text = ""2"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was haben wir bei Franz um zwei Uhr Nachts gegessen?"" >
    <Answer1 Text=""eklige Pizza"" Correct=""true""></Answer1>
    <Answer2 Text = ""ekligen Döner"" Correct=""false""></Answer2>
    <Answer3 Text = ""1"" Correct=""false""></Answer3>
    <Answer4 Text = ""2"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer war der lustigste Smiley?"" >
    <Answer1 Text=""Dirk"" Correct=""true""></Answer1>
    <Answer2 Text = ""Stefano"" Correct=""false""></Answer2>
    <Answer3 Text = ""Glockenjunge"" Correct=""false""></Answer3>
    <Answer4 Text = ""Franz"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wieviel Monate war Dirk bei MixedMode beschäftigt?"" >
    <Answer1 Text=""69"" Correct=""true""></Answer1>
    <Answer2 Text = ""96"" Correct=""false""></Answer2>
    <Answer3 Text = ""68"" Correct=""false""></Answer3>
    <Answer4 Text = ""70"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wieviele Profilaufrufe hatte Dirk auf Xing bis zum 16.01.2019?"" >
    <Answer1 Text=""2468"" Correct=""true""></Answer1>
    <Answer2 Text = ""10380"" Correct=""false""></Answer2>
    <Answer3 Text = ""77"" Correct=""false""></Answer3>
    <Answer4 Text = ""1"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Nenne die 72te Nachkommastelle von Pi?"" >
    <Answer1 Text=""6"" Correct=""true""></Answer1>
    <Answer2 Text = ""0"" Correct=""false""></Answer2>
    <Answer3 Text = ""4"" Correct=""false""></Answer3>
    <Answer4 Text = ""7"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was heißt 40 auf rumänisch?"" >
    <Answer1 Text=""patruzeci"" Correct=""true""></Answer1>
    <Answer2 Text = ""fasefulu"" Correct=""false""></Answer2>
    <Answer3 Text = ""cuarenta"" Correct=""false""></Answer3>
    <Answer4 Text = ""berrogei"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer ist Flo's Lieblingsyoutuber?"" >
    <Answer1 Text=""Drachenlord"" Correct=""true""></Answer1>
    <Answer2 Text = ""Tanzverbot"" Correct=""false""></Answer2>
    <Answer3 Text = ""Suzie Grime"" Correct=""false""></Answer3>
    <Answer4 Text = ""100 Sekunden Physik"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was heißt &quot;Dirk Heinze&quot; auf chinesisch?"" >
    <Answer1 Text=""德克海因策"" Correct=""true""></Answer1>
    <Answer2 Text = ""王八蛋"" Correct=""false""></Answer2>
    <Answer3 Text = ""阴茎"" Correct=""false""></Answer3>
    <Answer4 Text = ""黑公鸡"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Welches chinesische Sternzeichen ist Dirk?"" >
    <Answer1 Text=""Schaf"" Correct=""true""></Answer1>
    <Answer2 Text = ""Drache"" Correct=""false""></Answer2>
    <Answer3 Text = ""Löwe"" Correct=""false""></Answer3>
    <Answer4 Text = ""Tiger"" Correct=""false""></Answer4>
</Question>
<Question Text = ""An welchem Tag wurde Dirk eine Milliarden Sekunden alt?"" >
    <Answer1 Text=""16.10.2010"" Correct=""true""></Answer1>
    <Answer2 Text = ""24.07.2008"" Correct=""false""></Answer2>
    <Answer3 Text = ""08.09.2011"" Correct=""false""></Answer3>
    <Answer4 Text = ""06.04.2018"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wie hieß der bayrische Whiskey der bei Christoph getrunken wurde?"" >
    <Answer1 Text=""Slyrs"" Correct=""true""></Answer1>
    <Answer2 Text = ""Bavarka"" Correct=""false""></Answer2>
    <Answer3 Text = ""Stonewood"" Correct=""false""></Answer3>
    <Answer4 Text = ""Coillmór"" Correct=""false""></Answer4>
</Question>
<Question Text = ""An welchem Wochentag haben wir uns nen Kasten Bier in den Schädel gepresst?"" >
    <Answer1 Text=""Mittwoch"" Correct=""true""></Answer1>
    <Answer2 Text = ""Freitag"" Correct=""false""></Answer2>
    <Answer3 Text = ""Dienstag"" Correct=""false""></Answer3>
    <Answer4 Text = ""Montag"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer wurde an Dirks Geburtstag auch geboren?"" >
    <Answer1 Text=""Dieter Bohlen"" Correct=""true""></Answer1>
    <Answer2 Text = ""Pietro Lombardi"" Correct=""false""></Answer2>
    <Answer3 Text = ""Daniel Küblböck"" Correct=""false""></Answer3>
    <Answer4 Text = ""Michelle Kardashian"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wer hatte eine Freundin, oder doch nicht?"" >
    <Answer1 Text=""Franz"" Correct=""true""></Answer1>
    <Answer2 Text = ""Christoph"" Correct=""false""></Answer2>
    <Answer3 Text = ""Michi"" Correct=""false""></Answer3>
    <Answer4 Text = ""Stefano"" Correct=""false""></Answer4>
</Question>
<Question Text = ""An welchem Geburstag hat Franz &quot;Freundin&quot; mit ihm Schluss gemacht?"" >
    <Answer1 Text=""Stefanos 29."" Correct=""true""></Answer1>
    <Answer2 Text = ""Stefanos 28."" Correct=""false""></Answer2>
    <Answer3 Text = ""Stefanos 27."" Correct=""false""></Answer3>
    <Answer4 Text = ""Stefanos 30."" Correct=""false""></Answer4>
</Question>
<Question Text = ""Welcher Mongo ist mit der UBahn einfach abgehaun?"" >
    <Answer1 Text=""Stefano"" Correct=""true""></Answer1>
    <Answer2 Text = ""Flo"" Correct=""false""></Answer2>
    <Answer3 Text = ""Dirk"" Correct=""false""></Answer3>
    <Answer4 Text = ""Michi"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Wie oft wäscht man seine Jeans?"" >
    <Answer1 Text=""einmal pro Monat"" Correct=""true""></Answer1>
    <Answer2 Text = ""nie"" Correct=""false""></Answer2>
    <Answer3 Text = ""jede Woche"" Correct=""false""></Answer3>
    <Answer4 Text = ""jeden Tag"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was ist das schlechteste Spiel?"" >
    <Answer1 Text=""WoW"" Correct=""true""></Answer1>
    <Answer2 Text = ""Heartstone"" Correct=""false""></Answer2>
    <Answer3 Text = ""CS Go"" Correct=""false""></Answer3>
    <Answer4 Text = ""LoL"" Correct=""false""></Answer4>
</Question>
<Question Text = ""Welcher Game of Thrones Charakter passt am besten zum Dirk?"" >
    <Answer1 Text=""Hodor"" Correct=""true""></Answer1>
    <Answer2 Text = """" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was würdest du eher tun (Negerschwanz)?"" >
    <Answer1 Text=""Auf Kuchen sitzen und Penis essen"" Correct=""true""></Answer1>
    <Answer2 Text = ""Auf Penis sitzen und Kuchen essen"" Correct=""false""></Answer2>
    <Answer3 Text = ""Auf Penis sitzen und Penis essen"" Correct=""false""></Answer3>
    <Answer4 Text = ""Auf einer fetten sitzen"" Correct=""false""></Answer4>
</Question>
<Question Text = ""... (Binäre/Bitweise Operationen) ergibt?"" >
    <Answer1 Text="""" Correct=""true""></Answer1>
    <Answer2 Text = """" Correct=""false""></Answer2>
    <Answer3 Text = """" Correct=""false""></Answer3>
    <Answer4 Text = """" Correct=""false""></Answer4>
</Question>
<Question Text = ""Was ist kein Begriff in der Arnold C Syntax?"" >
    <Answer1 Text=""TALK TO MY HAND"" Correct=""true""></Answer1>
    <Answer2 Text = ""BULLSHIT"" Correct=""false""></Answer2>
    <Answer3 Text = ""I NEED YOU CLOTHES YOUR BOOTS AND YOUR MOTORCYCLE"" Correct=""false""></Answer3>
    <Answer4 Text = ""YOU HAVE NO RESPECT FOR LOGIC"" Correct=""false""></Answer4>
</Question>
</Questions>";



}
