using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSystem : MonoBehaviour
{

  public Transform physics2DModel;
  
  private List<ViewComponent> spawnedViews;

  private bool runningViewSynch = true;
  private bool applyAnimations = false;
  
  public void Start()
  {
    spawnedViews = new List<ViewComponent>();
    GameController.instance.rocketModelUpdated.AddListener(UpdateViews);
    GameController.instance.missionBeforeAbandoned.AddListener(missionStartReset);
    GameController.instance.missionAfterAbandoned.AddListener(missionEndReset);
    GameController.instance.missionAfterAbandoned.AddListener(OnRocketStop);
    GameController.instance.rocketStart.AddListener(OnRocketStart);
  }

  private void Update()
  {
    if (!runningViewSynch) return;
    foreach (ViewComponent view_i in spawnedViews)
    {
      Vector3 p = view_i.transform.position;
      Quaternion r = view_i.transform.rotation;
      view_i.spawnedInstace.position = p;
      view_i.spawnedInstace.rotation = r;
      if (view_i.hasAnimations && applyAnimations)
      {
        view_i.RunUpdateAnimation();
      }
    }
  }
  
  private void OnRocketStart()
  {
    applyAnimations = true;
  }
  
  private void OnRocketStop()
  {
    applyAnimations = false;
  }

  private void missionStartReset()
  {
    // The views are destroyed in ViewComponent.onDestroy
    runningViewSynch = false;
    spawnedViews = new List<ViewComponent>();
  }

  private void missionEndReset()
  {
    UpdateViews();
    runningViewSynch = true;
  }

  private void UpdateViews()
  {
    SpawnViews();
  }
  
  private void SpawnViews()
  {
    foreach (ViewComponent view_i in physics2DModel.GetComponentsInChildren<ViewComponent>())
    {
      if (view_i.spawnedInstace == null)
      {
        Vector3 p = view_i.transform.position;
        view_i.spawnedInstace = Instantiate(view_i.prefab, 
                                            p, 
                                            Quaternion.identity,
                                            transform);
        if (view_i.scaleToModel)
        {
          // Ground, need to scale x only
          view_i.spawnedInstace.localScale = new Vector3(view_i.transform.localScale.x,
                                                         view_i.spawnedInstace.localScale.y,
                                                         view_i.spawnedInstace.localScale.z);
        }
        
        
        spawnedViews.Add(view_i);
      }
    }
  }
  

}
