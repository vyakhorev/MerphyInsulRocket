using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewComponent : MonoBehaviour
{
  [SerializeField] public Transform prefab;
  [SerializeField] public bool scaleToModel = false;
  [SerializeField] public bool hasAnimations = false;
  [SerializeField] public bool animationIsRotation = false;
  [SerializeField] public float rotationSpeedRadSec = 0.5f;
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

  public void RunUpdateAnimation()
  {
    if (hasAnimations && animationIsRotation)
    {
      transform.Rotate(0, rotationSpeedRadSec * Time.deltaTime * 360f, 0, Space.Self);
    }
  }
  
}
