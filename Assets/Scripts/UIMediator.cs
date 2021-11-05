using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMediator : MonoBehaviour
{
  [SerializeField] public GameController gameController;

  public void RocketStart() => gameController.CompileAndStartRocket();

  public void AbandonMission() => gameController.AbandonMission();
  
}
