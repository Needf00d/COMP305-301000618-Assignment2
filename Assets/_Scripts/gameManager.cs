using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// File Name: gameManaer.cs
/// Author: Seoyoung
/// Last Modified by: Seoyoung
/// Date Last Modified: Nov. 3, 2019
/// Description: Controller for the Player prefab
/// </summary>

public class gameManager : MonoBehaviour
{
    public static gameManager instance; 
    public Text scoreText;
    public Text livesText;

    private int score = 0;

    void Awake()
    {
        if (!instance) 
            instance = this; 
    }

    public void AddScore(int num) 
    {
        score += num; 
        scoreText.text = "Score : " + score; 
    }



    void Start()
    {

    }

    void Update()
    {

    }
}
