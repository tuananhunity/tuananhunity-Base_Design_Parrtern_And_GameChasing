using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BEIV01DinosaurDependency : Dependency
{
    [SerializeField] private BEIV01DinosaurScriptableObject bEIV01DinosaurScriptableObject;
    [SerializeField] private GameObject objListItem;
    [SerializeField] private GameObject prefabItem;
    [SerializeField] private GameObject canvasBG;
    [SerializeField] private GameObject canvasIntro;
    [SerializeField] private SkeletonGraphic dinosaur;
    [SerializeField] private SkeletonGraphic dinosaurMom;
    [SerializeField] private SkeletonGraphic robotIntro;
    [SerializeField] private SkeletonGraphic maxIntro;
    [SerializeField] private SkeletonGraphic ellieIntro;
    [SerializeField] private SkeletonGraphic elliePlay;
    [SerializeField] private SkeletonGraphic robotPlay;
    [SerializeField] private SkeletonGraphic maxPlay;
    [SerializeField] private GameObject egg;
    [SerializeField] private RectTransform threeCharacter;
    [SerializeField] private RectTransform threeCharacterPlay;
    [SerializeField] private RectTransform bG1;
    [SerializeField] private RectTransform pointBG1;
    [SerializeField] private RectTransform itemIntro;
    [SerializeField] private RectTransform pointItemIntroAppear;
    [SerializeField] private GameObject listItemStudied;
    [SerializeField] private GameObject eggEndGame;
    [SerializeField] private RectTransform pointReturnLeft;
    [SerializeField] private RectTransform pointReturnThreeCharacter;
    [SerializeField] private RectTransform pointCharacterPlayTarget;
    [SerializeField] private RectTransform pointMaxPlay;
    [SerializeField] private RectTransform pointItemGuiding;
    [SerializeField] private RectTransform pointDinosaurAttack;
    [SerializeField] private RectTransform pointDinosaurAppear;
    [SerializeField] private RectTransform pointDinosaurMomAppear;

    [SerializeField] private BEIV01Instantiate bEIV01Intantiate;
    [SerializeField] private LoopBackGround loopBackGround;
    [SerializeField] private BEIV01UserInput bEIV01UserInput;
    [SerializeField] private BEIV01Trigger bEIV01Trigger;

    public override T GetStateData<T>()
    {
        T data;

        Type listType = typeof(T);

        if (listType == typeof(BEIV01DinosaurInitStateDataObjectDependecy))
        {
            BEIV01DinosaurInitStateDataObjectDependecy initStateData = new();
            initStateData.objListItem = objListItem;
            initStateData.prefabItem = prefabItem;
            initStateData.bEIV01Intantiate = bEIV01Intantiate;
            data = ConvertToType<T>(initStateData);
        }
        else if (listType == typeof(BEIV01DinosaurIntroStateDataObjectDependecy))
        {
            BEIV01DinosaurIntroStateDataObjectDependecy introStateData = new();
            introStateData.canvasIntro = canvasIntro;
            introStateData.canvasBG = canvasBG;
            introStateData.dinosaur = dinosaur;
            introStateData.RectDinosaur = dinosaur.GetComponent<RectTransform>();
            introStateData.robotIntro = robotIntro;
            introStateData.ellieIntro = ellieIntro;
            introStateData.maxIntro = maxIntro;
            introStateData.robotPlay = robotPlay;
            introStateData.maxPlay = maxPlay;
            introStateData.RectMaxPlay = maxPlay.GetComponent<RectTransform>();
            introStateData.elliePlay = elliePlay;
            introStateData.threeCharacter = threeCharacter;
            introStateData.threeCharacterPlay = threeCharacterPlay;
            introStateData.egg = egg;
            introStateData.itemIntro = itemIntro;
            introStateData.BG1 = bG1;
            introStateData.PointBG1 = pointBG1;
            introStateData.pointItemIntroAppear = pointItemIntroAppear;
            introStateData.objListItem = objListItem;
            introStateData.pointReturnThreeCharacter = pointReturnThreeCharacter;
            introStateData.pointCharacterPlayTarget = pointCharacterPlayTarget;
            introStateData.pointDinosaurAppear = pointDinosaurAppear;
            introStateData.pointReturnLeft = pointReturnLeft;
            introStateData.loopBackGround = loopBackGround;
            introStateData.bEIV01UserInput = bEIV01UserInput;
            introStateData.pointMaxPlay = pointMaxPlay;
            introStateData.pointItemGuiding = pointItemGuiding;
            introStateData.introDataConfig = bEIV01DinosaurScriptableObject.introDataConfig;
            data = ConvertToType<T>(introStateData);
        }
        else if (listType == typeof(BEIV01DinosaurGuidingStateDataObjectDependecy))
        {
            BEIV01DinosaurGuidingStateDataObjectDependecy guidingStateData = new();
            guidingStateData.guidingDataConfig = bEIV01DinosaurScriptableObject.guidingDataConfig;
            guidingStateData.bEIV01UserInput = bEIV01UserInput;
            data = ConvertToType<T>(guidingStateData);
        }
        else if (listType == typeof(BEIV01DinosaurTriggerStateDataObjectDependecy))
        {
            BEIV01DinosaurTriggerStateDataObjectDependecy triggerStateData = new();
            triggerStateData.objListItem = objListItem;
            triggerStateData.maxPlay = maxPlay;
            triggerStateData.dinosaur = dinosaur;
            triggerStateData.RectDinosaur = dinosaur.GetComponent<RectTransform>();
            triggerStateData.listItemStudied = listItemStudied;
            triggerStateData.pointReturnLeft = pointReturnLeft;
            triggerStateData.pointDinosaurAttack = pointDinosaurAttack;
            triggerStateData.bEIV01Trigger = bEIV01Trigger;
            triggerStateData.bEIV01UserInput = bEIV01UserInput;
            triggerStateData.triggerDataConfig = bEIV01DinosaurScriptableObject.triggerDataConfig;
            data = ConvertToType<T>(triggerStateData);
        }
        else if (listType == typeof(BEIV01DinosaurTriggerDoneStateDataObjectDependecy))
        {
            BEIV01DinosaurTriggerDoneStateDataObjectDependecy triggerDoneStateData = new();
            triggerDoneStateData.objListItem = objListItem;
            triggerDoneStateData.pointItemGuiding = pointItemGuiding;
            triggerDoneStateData.loopBackGround = loopBackGround;
            triggerDoneStateData.bEIV01UserInput = bEIV01UserInput;
            triggerDoneStateData.bEIV01Trigger = bEIV01Trigger;
            data = ConvertToType<T>(triggerDoneStateData);
        }
        else if (listType == typeof(BEIV01DinosaurEndGameStateDataObjectDependecy))
        {
            BEIV01DinosaurEndGameStateDataObjectDependecy endGameStateData = new();
            endGameStateData.dinosaur = dinosaur;
            endGameStateData.rectDinosaur = dinosaur.GetComponent<RectTransform>();
            endGameStateData.dinosaurMom = dinosaurMom;
            endGameStateData.rectDinosaurMom = dinosaurMom.GetComponent<RectTransform>();
            endGameStateData.maxPlay = maxPlay;
            endGameStateData.rectMaxPlay = maxPlay.GetComponent<RectTransform>(); 
            endGameStateData.eggEndGame = eggEndGame;
            endGameStateData.pointReturnLeft = pointReturnLeft;
            endGameStateData.pointCharacterPlayTarget = pointCharacterPlayTarget;
            endGameStateData.pointDinosaurAttack = pointDinosaurAttack;
            endGameStateData.pointDinosaurMomAppear = pointDinosaurMomAppear;
            endGameStateData.loopBackGround = loopBackGround;
            endGameStateData.bEIV01UserInput = bEIV01UserInput;
            endGameStateData.bEIV01Trigger = bEIV01Trigger;
            endGameStateData.endGameDataConfig = bEIV01DinosaurScriptableObject.endGameDataConfig;
            data = ConvertToType<T>(endGameStateData);
        }
        //else if (listType == typeof(BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy))
        //{
        //    BEIV01DinosaurPrepareNextTurnStateDataObjectDependecy prepareNextTurnStateData = new();
        //    prepareNextTurnStateData.objListItem = objListItem;
        //    prepareNextTurnStateData.loopBackGround = loopBackGround;
        //    prepareNextTurnStateData.bEIV01UserInput = bEIV01UserInput;
        //    prepareNextTurnStateData.bEIV01Trigger = bEIV01Trigger;
        //    data = ConvertToType<T>(prepareNextTurnStateData);
        //}
        else
        {
            data = ConvertToType<T>(null);
        }

        return data;
    }


    private T ConvertToType<T>(object data)
    {
        return (T)Convert.ChangeType(data, typeof(T));
    }
}
