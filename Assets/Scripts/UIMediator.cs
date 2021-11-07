using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMediator : MonoBehaviour
{
  [SerializeField] public GameController gameController;
  [SerializeField] public Greeter greeterScript;
  [SerializeField] public Text murphyScore;
  [SerializeField] public Text cost;
  [SerializeField] public Text flips;
  [SerializeField] public Text height;
  [SerializeField] public Text lastScore;
  [SerializeField] public Text bestScore;
  

  public void RocketStart() => gameController.CompileAndStartRocket();

  public void AbandonMission() => gameController.AbandonMission();

  public void StartGame() => greeterScript.StartInGameUI();

  public void UpdateScoreBoard()
  {
    murphyScore.text = ScoreCalculator.instance.totalFlyScore.ToString();
    cost.text = ScoreCalculator.instance.totalBuildCost.ToString();
    flips.text = ScoreCalculator.instance.flips.ToString();
    height.text = ScoreCalculator.instance.currentHeight.ToString();
    lastScore.text = ScoreCalculator.instance.lastScore.ToString();
    bestScore.text = ScoreCalculator.instance.bestScore.ToString();
  }

  public void Update()
  {
    UpdateScoreBoard();
  }
}
