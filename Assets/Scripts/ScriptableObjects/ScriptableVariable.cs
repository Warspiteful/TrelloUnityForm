using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject
{
    [SerializeField] private T value;

    public VariableUpdated ValueUpdated;
    public T Value
    {
        set
        {
            this.value = value;
            ValueUpdated?.Invoke();
        }
        get
        {
            return this.value;
        }
    }
}
