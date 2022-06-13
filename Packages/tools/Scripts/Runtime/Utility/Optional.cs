using System;
using System.Runtime.Serialization;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public struct Optional<T>
{
    public T value;
    public bool shouldBeUsed;
}