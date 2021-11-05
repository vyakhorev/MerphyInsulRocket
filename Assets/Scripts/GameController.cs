using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
  [SerializeField] public RocketFlyModel rocketFlyModel;
  
  public UnityEvent rocketModelUpdated;
  
  public static GameController instance;

  public void Awake()
  {
    if (instance == null) instance = this;
    rocketModelUpdated = new UnityEvent();
  }

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
