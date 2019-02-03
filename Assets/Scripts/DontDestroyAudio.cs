﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyAudio : MonoBehaviour
{
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("music");
        if (objs.Length > 1)
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    //void Update()
    //{
    //    if (SceneManager.GetActiveScene().name == "QuizGame")
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}
}
