using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewSystem : MonoBehaviour
{

  public Transform physics2DModel;
  
  private List<ViewComponent> spawnedViews;

  public void Awake()
  {
    spawnedViews = new List<ViewComponent>();
    SpawnView();
  }

  private void Update()
  {
    foreach (ViewComponent view_i in spawnedViews)
    {
      Vector3 p = view_i.transform.position;
      Quaternion r = view_i.transform.rotation;
      view_i.spawnedInstace.position = p;
      view_i.spawnedInstace.rotation = r;
    }
  }
  
  private void SpawnView()
  {
    foreach (ViewComponent view_i in physics2DModel.GetComponentsInChildren<ViewComponent>())
    {
      if (view_i.spawnedInstace == null)
      {
        Vector3 p = view_i.transform.position;
        view_i.spawnedInstace = Instantiate(view_i.prefab, p, Quaternion.identity);
        spawnedViews.Add(view_i);
      }
    }
  }
  

}
