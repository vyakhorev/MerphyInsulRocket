using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFlyModel : MonoBehaviour
{
  [SerializeField] public bool doFly = false;
  [SerializeField] public bool isSetup = false;
  
  private Rigidbody2D thisRigidBody;
  private Vector2 massCenter;
  private List<Engine> engines;
  private Vector2 enginesCenter;
  
  public void Setup()
  {
    thisRigidBody = GetComponent<Rigidbody2D>();
    SetupEngines();
    isSetup = true;
  }
  
  private void SetupEngines()
  {
    engines = new List<Engine>();
    
    foreach (Transform child in transform)
    {
      Engine eng_i = child.GetComponent<Engine>();
      BoxCollider2D coll_i = child.GetComponent<BoxCollider2D>();

      if (eng_i != null && coll_i != null)
      {
        eng_i.colliderOffset = coll_i.offset;
        engines.Add(eng_i);
      }
      
    }
    
  }

  void FixedUpdate()
  {
    if (doFly && isSetup)
    {
      EnginesFixedUpdate();
    }
  }

  public void EnginesFixedUpdate()
  {
    // Calculate cummulative vector
    Vector2 totalVel = new Vector2(0, 0);
    enginesCenter = new Vector2(0, 0);
    int c = 0; 
    foreach (Engine item in engines)
    {
      Transform itemTransform = item.transform;
      totalVel += (Vector2)itemTransform.up * item.engineForce;
      enginesCenter += (Vector2)itemTransform.position + item.colliderOffset;
      c += 1;
    }
    // TODO account for engine power
    enginesCenter /= c;
    // Apply
    ApplyAtPosition(enginesCenter, totalVel);
  }
  
  public void ApplyAtPosition(Vector2 forcePosition, Vector2 velocityVector)
  {
    thisRigidBody.AddForceAtPosition(velocityVector, forcePosition);
    Debug.DrawLine(forcePosition, forcePosition + velocityVector, Color.green, 0.5f);
  }
  
}
