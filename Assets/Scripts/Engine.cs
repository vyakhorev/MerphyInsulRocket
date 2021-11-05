using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Engine : MonoBehaviour, IRocketBlock
{
  [SerializeField]
  public float engineForce;
  
  // Filled on rocket build
  public Vector2 colliderOffset;

}
