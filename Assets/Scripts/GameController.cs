using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
  [SerializeField] public Transform rocketSpawnPlace;
  [SerializeField] public Transform physics2DGroup;
  [SerializeField] public Transform rocketModelPrefab;
  [SerializeField] public RocketFlyModel rocketFlyModel;

  public UnityEvent rocketModelUpdated;
  public UnityEvent missionBeforeAbandoned;
  public UnityEvent missionAfterAbandoned;
  
  public static GameController instance;

  public void Awake()
  {
    if (instance == null) instance = this;
    rocketModelUpdated = new UnityEvent();
    missionBeforeAbandoned = new UnityEvent();
    missionAfterAbandoned = new UnityEvent();
  }

  public void Start()
  {
    SpawnRocket();
  }

  public void SpawnRocket()
  {
    var rocketInstance = Instantiate(rocketModelPrefab, 
                                 rocketSpawnPlace.transform.position, 
                                 Quaternion.identity, 
                                 physics2DGroup.transform);
    rocketFlyModel = rocketInstance.GetComponent<RocketFlyModel>();
    rocketFlyModel.Initialize();
    rocketModelUpdated.Invoke();
  }
  
  public void CompileAndStartRocket()
  {
    rocketFlyModel.StartFly();
  }

  public void AbandonMission()
  {
    missionBeforeAbandoned.Invoke();
    Destroy(rocketFlyModel.transform.gameObject);
    SpawnRocket();
    missionAfterAbandoned.Invoke();
  }
  
    
}
