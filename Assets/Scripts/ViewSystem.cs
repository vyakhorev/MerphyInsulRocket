using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSystem : MonoBehaviour
{

  public Transform physics2DModel;
  
  private List<ViewComponent> spawnedViews;

  private bool running = true;
  
  public void Start()
  {
    spawnedViews = new List<ViewComponent>();
    GameController.instance.rocketModelUpdated.AddListener(UpdateViews);
    GameController.instance.missionBeforeAbandoned.AddListener(missionStartReset);
    GameController.instance.missionAfterAbandoned.AddListener(missionEndReset);
  }

  private void Update()
  {
    if (!running) return;
    foreach (ViewComponent view_i in spawnedViews)
    {
      Vector3 p = view_i.transform.position;
      Quaternion r = view_i.transform.rotation;
      view_i.spawnedInstace.position = p;
      view_i.spawnedInstace.rotation = r;
    }
  }

  private void missionStartReset()
  {
    // The views are destroyed in ViewComponent.onDestroy
    running = false;
    spawnedViews = new List<ViewComponent>();
  }

  private void missionEndReset()
  {
    UpdateViews();
    running = true;
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
          // Ground
          view_i.spawnedInstace.localScale = view_i.transform.localScale;
        }
        
        
        spawnedViews.Add(view_i);
      }
    }
  }
  

}
