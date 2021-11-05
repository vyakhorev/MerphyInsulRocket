using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewComponent : MonoBehaviour
{
  [SerializeField] public Transform prefab;
  [SerializeField] public bool scaleToModel = false;
  public Transform spawnedInstace;
}
