using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawner : MonoBehaviour
{
  [SerializeField] public List<Transform> itemPrefabs;
  
  // TODO Animate movement from spawnPoint to displayPoint
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
   
    // View system won't work here since it's detached, so we have to spawn view
    Attachable att = new_attachment.GetComponent<Attachable>();
    ViewComponent vw = att.spawnableModel.GetComponent<ViewComponent>();
    
    Transform mesh_obj = Instantiate(vw.prefab, p, Quaternion.identity, new_attachment);
    att.blockDisposed.AddListener(OnBlockDisposed);

  }

  public void OnBlockDisposed()
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
