using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BEIV01DinosaurAdapter : Adapter
{
    [SerializeField] bool isMookData;
    [SerializeField] private BEIV01DinosaurMookDataScriptableObject bEIV01DinosaurMookDataScriptableObject;

    private BEIV01PlayDinosaurData bEIV01PlayDinosaurData;
    public override T GetData<T>(int turn)
    {
        T data;
        
        if (isMookData)
        {
            bEIV01PlayDinosaurData = new global::BEIV01PlayDinosaurData();
            bEIV01PlayDinosaurData.listData = new List<BEIV01PlayData>();
            bEIV01PlayDinosaurData.listData = bEIV01DinosaurMookDataScriptableObject.listData;
        }

        Type listType = typeof(T);
        if (listType == typeof(BEIV01DinosaurInitStateData))
        {
            BEIV01DinosaurInitStateData bEIV01DinosaurInitStateData = new();
            bEIV01DinosaurInitStateData.bEIV01PlayData = bEIV01PlayDinosaurData.listData[turn];
            data = ConvertToType<T>(bEIV01DinosaurInitStateData);
        }
        else
        {
            data = ConvertToType<T>(null);
        }
        return data;
    }

    public override int GetMaxTurn()
    {
        return bEIV01PlayDinosaurData.listData.Count;
    }

    public override void SetData<T>(T data)
    {
        bEIV01PlayDinosaurData = (BEIV01PlayDinosaurData)Convert.ChangeType(data, typeof(BEIV01PlayDinosaurData));
    }

    private T ConvertToType<T>(object data)
    {
        return (T)Convert.ChangeType(data, typeof(T));
    }
}

//Data for play
public class BEIV01PlayDinosaurData
{
    public List<BEIV01PlayData> listData;
}

[Serializable]
public class BEIV01PlayData
{
    public List<BEIV01Item> lstItem;
}

[Serializable]
public class BEIV01Item
{
    public Sprite image;
    public AudioClip audioItem;
}

// Convert from server to local

[Serializable]
public class BEIV01DinosaurChasingDataSever
{
    public BEIV01DataPlaySever[] data;
}

[Serializable]
public class BEIV01DataPlaySever
{
    public int question_info;
}

