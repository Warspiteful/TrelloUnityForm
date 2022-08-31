using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormReset : MonoBehaviour
{

  [Header("Input Event Signal Input")]
  [SerializeField]
  private EventSignal reset;

  [Header("Information Fields to Clear")] 
  [SerializeField]
  private List<StringVariable> vars;

  private void OnEnable()
  {
    reset.onCall += ResetVar;
  }

  private void OnDisable()
  {
    reset.onCall -= ResetVar;
  }

  private void ResetVar()
  {
    foreach (StringVariable stringVar in vars)
    {
      stringVar.Reset();
    }
  }
}
