using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ParallaxBackground : MonoBehaviour
{

  [SerializeField] private Vector2 parallaxEffectMultiplier = new Vector2(.5f, .5f);
  [SerializeField] private bool infiniteHorizontal = true;
  [SerializeField] private bool infiniteVertical = true;
  
  private Transform cameraTransform;
  private Vector3 lastCameraPosition;
  private float textureUnitSizeX;
  private float textureUnitSizeY;
  private float zConstOffset;

  private void Start()
  {
    cameraTransform = Camera.main.transform;
    lastCameraPosition = cameraTransform.position;
    Sprite sprite = GetComponent<SpriteRenderer>().sprite;
    Texture2D texture = sprite.texture;
    textureUnitSizeX = texture.width / sprite.pixelsPerUnit;
    textureUnitSizeY = texture.height / sprite.pixelsPerUnit;
    zConstOffset = transform.position.z;
  }

  private void Update()
  {
    Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
    transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, 
                                      deltaMovement.y * parallaxEffectMultiplier.y, 0);
    lastCameraPosition = cameraTransform.position;

    if (infiniteHorizontal)
    {
      if (Math.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
      {
        float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
        transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y, zConstOffset);
      }
    }

    if (infiniteVertical)
    {
      if (Math.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY)
      {
        float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
        transform.position = new Vector3(transform.position.x, cameraTransform.transform.position.y + offsetPositionY, zConstOffset);
      }
    }
    
  }
}
