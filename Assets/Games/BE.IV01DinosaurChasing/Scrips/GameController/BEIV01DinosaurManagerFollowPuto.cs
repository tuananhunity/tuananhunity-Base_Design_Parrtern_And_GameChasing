using BaseSDK;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BEIV01DinosaurManagerFollowPuto : GamePlayManagerBase
{
    protected virtual void Awake()
    {
        adapter = GetComponent<Adapter>();
        EventManagerPuto.Instance.AddListener(this, EventChanelID.GamePlay, EventChanelID.GameState);
    }


    private void OnDestroy()
    {
        EventManagerPuto.Instance?.RemoveListener(this);
    }
    public override object GetGamePlayData()
    {
        throw new System.NotImplementedException();
    }

    public override void OnBegin()
    {
        throw new System.NotImplementedException();
    }

    public override void OnComplete()
    {
        throw new System.NotImplementedException();
    }

    private List<int> listIDBundle;
    protected Adapter adapter;
    public override void SetDataInit(object ConfigGame, object JsonRuleWord)
    {
        if (string.IsNullOrEmpty(ConfigGame.ToString()) || string.IsNullOrEmpty(JsonRuleWord.ToString())) return;
        listIDBundle = new List<int>();
        BEIV01DinosaurChasingDataSever configDataGame = JsonUtility.FromJson<BEIV01DinosaurChasingDataSever>(ConfigGame.ToString());
        MKStoreDataBase.WordInforRule wordInforRule = JsonConvert.DeserializeObject<MKStoreDataBase.WordInforRule>(JsonRuleWord.ToString());

        var wordsRule = wordInforRule.wordsRule;

        BEIV01PlayDinosaurData bEIV01PlayDinosaurData = new();
        bEIV01PlayDinosaurData.listData = new();
        BEIV01PlayData bEIV01PlayData = new();
        bEIV01PlayData.lstItem = new();


        foreach (var turnData in configDataGame.data)
        {
            BEIV01Item bEIV01Item = new();
            int idWord = turnData.question_info;
            DataModel.Word wordData = MKStoreDataBase.GetDataWordById(idWord);
            listIDBundle.Add(idWord);
            bEIV01Item.audioItem = MKStoreDataBase.GetAudioClipByWordId(idWord, wordsRule, wordData.audio);
            if (wordData.image.Count > 0)
            {
                bEIV01Item.image = MKStoreDataBase.GetImageByWordId(idWord, wordsRule, wordData.image);
            }
            bEIV01PlayData.lstItem.Add(bEIV01Item);
        }
        bEIV01PlayDinosaurData.listData.Add(bEIV01PlayData);

        adapter.SetData<BEIV01PlayDinosaurData>(bEIV01PlayDinosaurData);
    }
    public override void OnReceiveEvent(EventMessage message)
    {
        if (message.eventChanelId == EventChanelID.GameState)
        {
            switch (message.eventName)
            {
                case EventName.GameState.GAME_STOP:
                    {
                        if (listIDBundle.Count <= 0) return;

                        foreach (var item in listIDBundle)
                        {
                            MKStoreDataBase.UnLoadBundleWorld(item);
                        }
                        listIDBundle.Clear();
                    }
                    break;
            }
        }
    }

    protected override void PlayNext()
    {
        throw new System.NotImplementedException();
    }
}
