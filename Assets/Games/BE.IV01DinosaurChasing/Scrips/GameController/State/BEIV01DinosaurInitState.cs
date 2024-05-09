using Base.Observer;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurInitState : FSMState
{
    private BEIV01DinosaurInitStateDataObjectDependecy bEIV01DinosaurInitStateDataObjectDependecy;


    public override void OnEnter(object data)
    {
        base.OnEnter(data);
        BEIV01DinosaurInitStateData initStateData = (BEIV01DinosaurInitStateData)data;
        SetData(initStateData);
    }

    public override void SetUp(object data)
    {

        bEIV01DinosaurInitStateDataObjectDependecy = (BEIV01DinosaurInitStateDataObjectDependecy)data;

    }
    private void SetData(BEIV01DinosaurInitStateData initStateData)
    {
        for (int i = 0; i < initStateData.bEIV01PlayData.lstItem.Count; i++)
        {
            GameObject itemSpawn = bEIV01DinosaurInitStateDataObjectDependecy.bEIV01Intantiate.InstantiatePrefab(bEIV01DinosaurInitStateDataObjectDependecy.prefabItem, bEIV01DinosaurInitStateDataObjectDependecy.objListItem.transform);
            itemSpawn.transform.SetParent(bEIV01DinosaurInitStateDataObjectDependecy.objListItem.transform);
            itemSpawn.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        }
        for (int i = 0; i < bEIV01DinosaurInitStateDataObjectDependecy.objListItem.transform.childCount; i++)
        {
            bEIV01DinosaurInitStateDataObjectDependecy.objListItem.transform.GetChild(i).GetComponentInChildren<BEIV01Image>().SetImage(initStateData.bEIV01PlayData.lstItem[i].image);
            bEIV01DinosaurInitStateDataObjectDependecy.objListItem.transform.GetChild(i).GetComponentInChildren<AudioSource>().clip = initStateData.bEIV01PlayData.lstItem[i].audioItem;
        }
        TriggerFinishSetData();
    }

    private void TriggerFinishSetData()
    {
        BEIV01DinosaurEvent bEIV01DinosaurEvent = new BEIV01DinosaurEvent(BEIV01DinosaurEvent.INIT_STATE_FINISH, null);
        ObserverManager.TriggerEvent<BEIV01DinosaurEvent>(bEIV01DinosaurEvent);
    }
}

public class BEIV01DinosaurInitStateData
{
    public BEIV01PlayData bEIV01PlayData { get; set; }
}

public class BEIV01DinosaurInitStateDataObjectDependecy
{ 
    public GameObject objListItem { get; set; }
    public GameObject prefabItem { get; set; }
    public BEIV01Instantiate bEIV01Intantiate { get; set; }
}