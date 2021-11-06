using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMediator : MonoBehaviour
{
  [SerializeField] public GameController gameController;
  [SerializeField] public Text murphyScore;
  [SerializeField] public Text cost;
  [SerializeField] public Text flips;
  [SerializeField] public Text height;

  public void RocketStart() => gameController.CompileAndStartRocket();

  public void AbandonMission() => gameController.AbandonMission();

  public void UpdateScoreBoard()
  {
    murphyScore.text = ScoreCalculator.instance.totalFlyScore.ToString();
    cost.text = ScoreCalculator.instance.totalBuildCost.ToString();
    flips.text = ScoreCalculator.instance.flips.ToString();
    height.text = ScoreCalculator.instance.currentHeight.ToString();
  }

  public void Update()
  {
    UpdateScoreBoard();
  }
}
