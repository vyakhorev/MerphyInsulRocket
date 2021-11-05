using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField] public RocketFlyModel rocketFlyModel;

  public void CompileAndStartRocket()
  {
    rocketFlyModel.Setup();
    rocketFlyModel.doFly = true;
  }

  public void AbandonMission()
  {
    
  }
  
  // Start is called before the first frame update
  void Start()
  {
      
  }

  // Update is called once per frame
  void Update()
  {
      
  }
    
    
}
