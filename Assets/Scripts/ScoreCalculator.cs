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
  public int bestScore;
  public int lastScore;
  private bool isFlying;

  public static ScoreCalculator instance;

  private Vector3 startPosition;
  
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
  }
  
  private void BuyBlockCost()
  {
    totalBuildCost += 1;
  }

  private void DestroyBlockCost()
  {
    totalBuildCost += 2;
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
    currentHeight = 0;
  }
  
  
  private void FixedUpdate()
  {
    if (isFlying)
    {
      Vector3 p = flyingThing.position;
      currentHeight = (int)Math.Round(p.y - startPosition.y);

      // TODO: account for flips and build cost
      totalFlyScore = currentHeight;

      bool doCancelFligh = checkFlyIsOver();
      if (doCancelFligh)
      {
        gameOver.Invoke();
      }

    }
  }

  private bool checkFlyIsOver()
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
  
  
  
}
