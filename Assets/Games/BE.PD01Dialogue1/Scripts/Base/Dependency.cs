using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    public abstract T GetStateData<T>();
    
}
