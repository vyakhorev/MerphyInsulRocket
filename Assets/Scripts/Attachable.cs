using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attachable : MonoBehaviour
{

  [SerializeField] public float spawnProb;
  [SerializeField] public Transform spawnableModel;

  public UnityEvent blockDisposed;
  public UnityEvent blockAttached;
  public UnityEvent blockSnapped;
  
  private Vector3 mOffset;
  private float mZCoord;

  private void Awake()
  {
    blockDisposed = new UnityEvent();
    blockAttached = new UnityEvent();
    blockSnapped = new UnityEvent();
  }

  private void OnMouseDown()
  {
    mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
    mOffset = transform.position - GetMouseWorldPos();
  }

  private Vector3 GetMouseWorldPos()
  {
    Vector3 mousePoint = Input.mousePosition;
    mousePoint.z = mZCoord;
    //mousePoint.z = 0;  // we are in 2D
    return Camera.main.ScreenToWorldPoint(mousePoint);
  }

  private void OnMouseDrag()
  {
    transform.position = GetMouseWorldPos() + mOffset;
  }
  
  private void OnTriggerEnter(Collider other)
  {
    
  }
}
