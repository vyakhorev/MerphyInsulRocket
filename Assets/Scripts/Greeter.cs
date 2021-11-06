using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Greeter : MonoBehaviour
{
  [SerializeField] public RectTransform greetingCanvas;
  [SerializeField] public RectTransform ingameCanvas;

  public void ShowGreetingCanvas()
  {
    greetingCanvas.gameObject.SetActive(true);
    ingameCanvas.gameObject.SetActive(false);
  }

  public void StartInGameUI()
  {
    greetingCanvas.gameObject.SetActive(false);
    ingameCanvas.gameObject.SetActive(true);
  }
  
  
  
  
}
