using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisposer : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    var attch = other.transform.GetComponent<Attachable>();
    attch.blockDisposed.Invoke();
    Destroy(other.transform.gameObject);
  }
  
  private void OnTriggerStay(Collider other)
  {
    
  }
  
  private void OnTriggerExit(Collider other)
  {
    
  }
}
