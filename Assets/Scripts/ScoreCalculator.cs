using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCalculator : MonoBehaviour
{
  
  public int totalFlyScore;
  public int totalBuildCost;
  
  public Transform flyingThing;
  public int flips;
  public int currentHeight;
  private bool isFlying;

  public static ScoreCalculator instance;

  private Vector3 startPosition;
  
  public void Awake()
  {
    if (instance == null) instance = this;
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

    }
  }
  
  
  
}
