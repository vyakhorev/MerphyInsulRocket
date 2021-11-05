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

  public Transform blockView;
  
  private Vector3 mOffset;
  private float mZCoord;

  public Vector3 intendedBlockPosition;
  public Vector3 snappedBlockPosition;
  public Vector2Int snappedBlockGridPosition;
  public bool canBeSnapped;
  
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
    intendedBlockPosition = GetMouseWorldPos() + mOffset;
    transform.position = intendedBlockPosition;
    (Vector3, Vector2Int)? ans = TrySnapToShip(intendedBlockPosition);
    if (ans.HasValue)
    {
      var snappedPos = ans.Value.Item1;
      var snappedGridPos = ans.Value.Item2;
      canBeSnapped = true;
      snappedBlockPosition = snappedPos;
      snappedBlockGridPosition = snappedGridPos;
    }
    else
    {
      canBeSnapped = false;
    }

    //var view = GetComponentInChildren<ViewComponent>();
    if (canBeSnapped)
    {
      blockView.transform.position = snappedBlockPosition;
    }
    else
    {
      blockView.transform.position = intendedBlockPosition;
    }

  }

  private void OnMouseUp()
  {
    if (canBeSnapped)
    {
      Debug.Log("Snapped!");
      
      GameController.instance.rocketFlyModel.AddNewBlock(snappedBlockGridPosition,
                                                         transform.GetComponent<Attachable>().spawnableModel);
      blockAttached.Invoke();
      blockAttached.RemoveAllListeners();
      blockDisposed.RemoveAllListeners();
      blockSnapped.RemoveAllListeners();
      Destroy(blockView.gameObject);
      Destroy(this.gameObject);
    }
  }

  private (Vector3, Vector2Int)? TrySnapToShip(Vector3 origin)
  {
    // Cast a circle on physics 2D layer
    Collider2D other = Physics2D.OverlapCircle(origin, 0.5f);
    if (other == null) return null;
    
    RocketFlyModel rocketModel = other.transform.GetComponent<RocketFlyModel>();
    if (rocketModel != null)
    {
      return rocketModel.QuerySnappingPosition(origin);
    }
    
    return null;
  }
  
}
