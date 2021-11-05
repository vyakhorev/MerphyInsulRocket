using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
  [SerializeField] private Canvas canvas;
  private RectTransform _rectTransform;
  private CanvasGroup _canvasGroup;
  
  private void Awake()
  {
    _rectTransform = GetComponent<RectTransform>();
    _canvasGroup = GetComponent<CanvasGroup>();
  }

  public void OnPointerDown(PointerEventData eventData)
  {
    
  }

  public void OnBeginDrag(PointerEventData eventData)
  {
    
    _canvasGroup.alpha = 0.6f;
    _canvasGroup.blocksRaycasts = false;
  }

  public void OnEndDrag(PointerEventData eventData)
  {
    
    _canvasGroup.alpha = 1f;
    _canvasGroup.blocksRaycasts = true;
  }

  public void OnDrag(PointerEventData eventData)
  {

    _rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
  }
  
}
