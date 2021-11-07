using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreCalculator : MonoBehaviour
{

  [SerializeField] public Transform maxHeight;
  [SerializeField] public Transform maxLeft;
  [SerializeField] public Transform maxRight;
  
  public UnityEvent gameOver;
  
  public int totalFlyScore;
  public int totalBuildCost;
  
  public Transform flyingThing;
  public int flips;
  public int currentHeight;
  public int maxScoreHeight;
  public int bestScore;
  public int lastScore;
  public int flipsScore;
  private bool isFlying;

  public static ScoreCalculator instance;

  private Vector3 startPosition;
  
  // flip calc
  private bool flipHad0;
  private bool flipHad90;
  private bool flipHad180;
  private const float eps = 45;
  private float startTime;
  
  public void Awake()
  {
    lastScore = 0;
    bestScore = 0;
    if (instance == null) instance = this;
    gameOver = new UnityEvent();
  }

  public void Start()
  {
    ResetScore();
    GameController.instance.someBlockAdded.AddListener(BuyBlockCost);
    GameController.instance.someBlockDestroyed.AddListener(DestroyBlockCost);
    GameController.instance.missionAfterAbandoned.AddListener(ResetScore);
    GameController.instance.rocketStart.AddListener(StartFlyCalc);
    GameController.instance.rocketModelUpdated.AddListener(OnRocketModelUpdate);
  }

  public void StartFlyCalc()
  {
    startPosition = flyingThing.position;
    isFlying = true;
    startTime = Time.time;
  }
  
  private void BuyBlockCost()
  {
    totalBuildCost += 1;
  }

  private void DestroyBlockCost()
  {
    totalBuildCost += 10;
  }

  private void OnRocketModelUpdate()
  {
    // Calculate facts about rocket?...
  }
  
  private void ResetScore()
  {
    lastScore = totalFlyScore;
    if (lastScore > bestScore)
    {
      bestScore = lastScore;
    }
    totalFlyScore = 0;
    totalBuildCost = 0;
    isFlying = false;
    flips = 0;
    flipsScore = 0;
    currentHeight = 0;
    maxScoreHeight = 0;

    flipHad0 = false;
    flipHad90 = false;
    flipHad180 = false;
  }
  
  
  private void FixedUpdate()
  {
    if (isFlying)
    {
      Vector3 p = flyingThing.position;
      currentHeight = (int)Math.Round(p.y - startPosition.y);

      if (currentHeight > maxScoreHeight)
      {
        maxScoreHeight = currentHeight;
      }

      bool doCancelFlight = CheckFlyIsOver();
      if (doCancelFlight)
      {
        gameOver.Invoke();
      }

      bool val = CheckFlip();
      if (val)
      {
        flips += 1;
        flipsScore += currentHeight;
      }

      int secsFromFlyStart = (int)Math.Round((Time.time - startTime), 0);


      totalFlyScore = (int)Math.Round(maxScoreHeight / ((secsFromFlyStart + 1) * 5) + flipsScore*2 - totalBuildCost * secsFromFlyStart * 0.5f);  
      

      

    }
  }

  private bool CheckFlyIsOver()
  {
    if (flyingThing.position.x > maxRight.position.x)
    {
      return true;
    }
    if (flyingThing.position.x < maxLeft.position.x)
    {
      return true;
    }
    if (flyingThing.position.y > maxHeight.position.y)
    {
      return true;
    }

    return false;
  }

  private bool CheckFlip()
  {
    var a = Vector3.Angle(Vector3.up, flyingThing.up);
 
    if (a + eps > 0 && a - eps < 0)
    {
      flipHad0 = true;
    }
    else if (a + eps > 90 && a - eps < 90)
    {
      flipHad90 = true;
    }
    else if (a + eps > 180 && a - eps < 180)
    {
      flipHad180 = true;
    }

    if (flipHad0 && flipHad90 && flipHad180)
    {
      flipHad0 = false;
      flipHad90 = false;
      flipHad180 = false;
      return true;
    }
    return false;
    
  }
  
  
  
}
