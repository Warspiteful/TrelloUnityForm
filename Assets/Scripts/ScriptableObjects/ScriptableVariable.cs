using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ScriptableVariable<T> : ScriptableObject
{
    [SerializeField] protected T value;

    public abstract void ResetVal();

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
