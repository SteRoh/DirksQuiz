using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public int CurrentCorrectAnswers { get; set; }
    public int NeededAnswers { get; set; }

    public Slider ProgressBarSlider;
    // Start is called before the first frame update
    void Start()
    {
        NeededAnswers = 20;
        CurrentCorrectAnswers = 0;
        ProgressBarSlider.maxValue = 20;
        ProgressBarSlider.minValue = 0;
    }

    public void RightAnswer()
    {
        CurrentCorrectAnswers++;
        ProgressBarSlider.value = CurrentCorrectAnswers;
    }


    public void WrongAnswer()
    {
        CurrentCorrectAnswers = 0;
        ProgressBarSlider.value = CurrentCorrectAnswers;
    }

}


