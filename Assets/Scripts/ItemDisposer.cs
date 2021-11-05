using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisposer : MonoBehaviour
{

  private void OnTriggerEnter(Collider other)
  {
    var attch = other.transform.GetComponent<Attachable>();
    if (attch != null)
    {
      attch.blockDisposed.Invoke();
      Transform viewBlock = attch.blockView;
      Destroy(other.transform.gameObject);
      Destroy(viewBlock.transform.gameObject);
    }
  }
  
}
