using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Adapter : MonoBehaviour
{
    public abstract T GetData<T>(int turn);
    public abstract void SetData<T>(T data);

    public abstract int GetMaxTurn();
}
