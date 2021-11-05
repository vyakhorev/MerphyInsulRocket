using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFlyModel : MonoBehaviour
{
  [SerializeField] public bool doFly = false;
  [SerializeField] public bool isSetup = false;
  [SerializeField] public Transform startModelBlockPrefab; 
  [SerializeField] public Vector2Int maxSize = new Vector2Int(8, 5);
  [SerializeField] public Vector2Int zeroIndex = new Vector2Int(5, 0);
  [SerializeField] public float blockWidth = 1f;
  
  private Rigidbody2D thisRigidBody;
  private Vector2 massCenter;
  private List<Engine> engines;
  private Vector2 enginesCenter;
  private List<Building2DTag> rocketBlocks;
  private List<Vector3> freePositions;
  private List<Vector2Int> freeGridCells;

  private Building2DTag[,] rocketGrid;

  void FixedUpdate()
  {
    if (doFly && isSetup)
    {
      EnginesFixedUpdate();
    }
  }

  public void Initialize()
  {
    // Called at game start / abandon
    isSetup = false;
    doFly = false;
    InitBuilding();
    Setup();
  }
  
  public void Setup()
  {
    thisRigidBody = GetComponent<Rigidbody2D>();
    SetupEngines();
    RegisterAllBlocks();
    RecalcAttachablePositions();
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

  public void InitBuilding()
  {
    rocketGrid = new Building2DTag[maxSize[0], maxSize[1]];
    Transform startBlock = Instantiate(startModelBlockPrefab, transform.position, Quaternion.identity, transform);
    Building2DTag bldBlock = startBlock.GetComponent<Building2DTag>();
    rocketGrid[zeroIndex[0], zeroIndex[1]] = bldBlock;
    GameController.instance.rocketModelUpdated.Invoke();
  }
  
  public void AddNewBlock(Transform newAttachableBlock)
  {
    
  }

  private void RegisterAllBlocks()
  {
    rocketBlocks = new List<Building2DTag>();
    foreach (Transform child in transform)
    {
      Building2DTag building_block = child.GetComponent<Building2DTag>();
      rocketBlocks.Add(building_block);
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
    ApplyEnginesAtPosition(enginesCenter, totalVel);
  }
  
  public void ApplyEnginesAtPosition(Vector2 forcePosition, Vector2 velocityVector)
  {
    thisRigidBody.AddForceAtPosition(velocityVector, forcePosition);
    Debug.DrawLine(forcePosition, forcePosition + velocityVector, Color.green, 0.5f);
  }

  public void RecalcAttachablePositions()
  {
    freeGridCells = new List<Vector2Int>();
    
    for (int k = 0; k < rocketGrid.GetLength(0); k++)
    {
      for (int j = 0; j < rocketGrid.GetLength(1); j++)
      {
        Building2DTag block = rocketGrid[k, j];
        if (block != null)
        {
          // right
          if (k + 1 < rocketGrid.GetLength(0))
          {
            if (rocketGrid[k + 1, j] == null)
            {
              freeGridCells.Add(new Vector2Int(k + 1, j));
            }
          }  
          // left
          if (k - 1 >= 0)
          {
            if (rocketGrid[k - 1, j] == null)
            {
              freeGridCells.Add(new Vector2Int(k - 1, j));
            }
          } 
          // top
          if (j + 1 < rocketGrid.GetLength(1))
          {
            if (rocketGrid[k, j + 1] == null)
            {
              freeGridCells.Add(new Vector2Int(k, j + 1));
            }
          } 
          // down
          if (j - 1 >= 0)
          {
            if (rocketGrid[k, j - 1] == null)
            {
              freeGridCells.Add(new Vector2Int(k, j - 1));
            }
          }
        }
      }
    }

    // So id's of freePositions / freeGridCells match
    freePositions = new List<Vector3>();
    Vector3 p0 = transform.position; 
    foreach (Vector2Int gridId in freeGridCells)
    {
      float x = (gridId.x - zeroIndex.x) * blockWidth; 
      float y = (gridId.y - zeroIndex.y) * blockWidth;
      Vector3 availPos = p0 + new Vector3(x, y, 0);
      freePositions.Add(availPos);
    }
    
  }
  
  public Vector3 QuerySnappingPosition(Vector3 original)
  {
    // Loop over all available positions
    float bestDistance = 999999f;
    int bestIdx = 0;
    int idx = 0;

    Vector3 bestPosition = new Vector3(0, 0, 0);
    foreach (var position_i in freePositions)
    {
      float dst = (position_i - original).magnitude;
      if (dst <= bestDistance)
      {
        bestDistance = dst;
        bestIdx = idx;
        bestPosition = position_i;
      }
      idx += 1;
    }
    
    return bestPosition;

  }
  
  
}
