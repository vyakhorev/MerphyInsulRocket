using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewComponent : MonoBehaviour
{
  [SerializeField] public Transform prefab;
  [SerializeField] public bool scaleToModel = false;
  public Transform spawnedInstace;

  private void OnDestroy()
  {
    if (spawnedInstace != null)
    {
      if (!spawnedInstace.IsDestroyed())
      {
        Destroy(spawnedInstace.gameObject);        
      }
    }
    
    
  }
}
