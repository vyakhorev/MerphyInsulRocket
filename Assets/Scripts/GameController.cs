using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
  [SerializeField] public Transform rocketSpawnPlace;
  [SerializeField] public Transform physics2DGroup;
  [SerializeField] public Transform rocketModelPrefab;
  [SerializeField] public RocketFlyModel rocketFlyModel;
  [SerializeField] public CinemachineVirtualCamera cmVmCam;
  
  public UnityEvent rocketModelUpdated;
  public UnityEvent missionBeforeAbandoned;
  public UnityEvent missionAfterAbandoned;
  public UnityEvent someBlockAdded;
  public UnityEvent someBlockDestroyed;
  public UnityEvent rocketStart;
  
  public static GameController instance;

  public void Awake()
  {
    if (instance == null) instance = this;
    rocketModelUpdated = new UnityEvent();
    missionBeforeAbandoned = new UnityEvent();
    missionAfterAbandoned = new UnityEvent();
    someBlockAdded = new UnityEvent();
    someBlockDestroyed = new UnityEvent();
    rocketStart = new UnityEvent();
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
    ScoreCalculator.instance.flyingThing = rocketInstance;
    cmVmCam.Follow = rocketInstance.transform;
    rocketFlyModel = rocketInstance.GetComponent<RocketFlyModel>();
    rocketFlyModel.Initialize();
    rocketModelUpdated.Invoke();
  }
  
  public void CompileAndStartRocket()
  {
    rocketFlyModel.StartFly();
    rocketStart.Invoke();
    toggleCMNoiseOn();
  }

  public void AbandonMission()
  {
    missionBeforeAbandoned.Invoke();
    Destroy(rocketFlyModel.transform.gameObject);
    toggleCMNoiseOff();
    SpawnRocket();
    missionAfterAbandoned.Invoke();
  }

  private void toggleCMNoiseOn()
  {
    CinemachineBasicMultiChannelPerlin noise = cmVmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    noise.m_AmplitudeGain = 1f;
    noise.m_FrequencyGain = 1f;
  }
  
  private void toggleCMNoiseOff()
  {
    CinemachineBasicMultiChannelPerlin noise = cmVmCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    noise.m_AmplitudeGain = 0f;
    noise.m_FrequencyGain = 0f;
  }
  
}
