using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BEIV01DinosaurMookDataScriptableObject", menuName = "ScriptableObjects/BEIV01Dinosaur/MockData", order = 1)]
public class BEIV01DinosaurMookDataScriptableObject : ScriptableObject
{
    public List<BEIV01PlayData> listData;
}