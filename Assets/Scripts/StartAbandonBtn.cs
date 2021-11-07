using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartAbandonBtn : MonoBehaviour
{
  [SerializeField] public UIMediator mediator;
  private bool isRunning = false;
  private Text txt;
  private Image img;
  
  
  private void Awake()
  {
    ScoreCalculator.instance.gameOver.AddListener(OnGameOver);
    txt = GetComponentInChildren<Text>();
    img = GetComponent<Image>();
    txt.text = "START";
    img.color = Color.green;
  }
  
  public void BtnPressed()
  {
    isRunning = !isRunning;
    if (isRunning)
    {
      mediator.RocketStart();
      txt.text = "ABANDON";
      img.color = Color.red;
    }
    else
    {
      mediator.AbandonMission();
      txt.text = "START";
      img.color = Color.green;
    }
    
  }

  public void OnGameOver()
  {
    txt.text = "START";
    img.color = Color.green;
    isRunning = false;
  }
  
  
}
