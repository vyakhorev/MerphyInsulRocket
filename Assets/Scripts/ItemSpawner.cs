using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
  [SerializeField] public List<Transform> itemPrefabs;
  
  [SerializeField] public Transform spawnPoint;
  [SerializeField] public Transform displayPoint;

  public void Start()
  {
    SpawnNewItem();
  }

  public void SpawnNewItem()
  {
    Vector3 p = displayPoint.position; 
    Transform prefab = ChooseItems();
    Transform new_attachment = Instantiate(prefab, p, Quaternion.identity);
   
    // Don't want to use view system here
    Attachable att = new_attachment.GetComponent<Attachable>();
    ViewComponent vw = att.spawnableModel.GetComponent<ViewComponent>();
    
    att.blockView = Instantiate(vw.prefab, p, Quaternion.identity);
    att.blockDisposed.AddListener(OnBlockDisposed);
    att.blockAttached.AddListener(OnBlockAttached);

  }

  public void OnBlockDisposed()
  {
    SpawnNewItem();
    
  }

  public void OnBlockAttached()
  {
    SpawnNewItem();
  }
  
  public Transform ChooseItems()
  {
    float p = Random.Range(0f, 1f);
    float cumm = 0f;
    foreach (Transform pr_i in itemPrefabs)
    {
      Attachable att = pr_i.GetComponent<Attachable>();
      cumm += att.spawnProb;
      if (cumm >= p)
      {
        return pr_i;
      }
    }
    return null;
  }
  
  
  
}
