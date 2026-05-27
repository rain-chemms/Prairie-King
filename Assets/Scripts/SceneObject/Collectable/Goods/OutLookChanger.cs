using System;
using UnityEngine;

public interface OutLookChanger<T> where T : Enum
{
    public void ChangeOutLook(T type);
}