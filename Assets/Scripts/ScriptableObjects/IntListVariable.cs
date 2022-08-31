using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Variable/intList",
    fileName = "NewIntListVariable")]
public class IntListVariable : ScriptableVariable<List<int>>
{}
