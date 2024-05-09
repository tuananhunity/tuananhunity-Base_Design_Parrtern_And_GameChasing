using Base.Observer;
public struct BEIV01DinosaurEvent: EventListener<BEIV01DinosaurEvent>
{
    public string EventName;
    public object Data;

    public BEIV01DinosaurEvent(string nameEvent, object data)
    {
        this.EventName = nameEvent;
        this.Data = data;
    }

    public void OnMMEvent(BEIV01DinosaurEvent eventType)
    {
        throw new System.NotImplementedException();
    }
    public const string INIT_STATE_START = "INIT_STATE_START";
    public const string INIT_STATE_FINISH = "INIT_STATE_FINISH";

    public const string INTRO_STATE_START = "INTRO_STATE_START";
    public const string INTRO_STATE_FINISH = "INTRO_STATE_FINISH";

    public const string GUIDING_STATE = "GUIDING_STATE";
    public const string TRIGGER_STATE = "TRIGGER_STATE";

    public const string TRIGGER_ITEM_FINISH_STATE = "TRIGGER_ITEM_FINISH_STATE";
    public const string TRIGGER_OBSTACLE_FINISH_STATE = "TRIGGER_OBSTACLE_FINISH_STATE";

    public const string TRIGGER_DONE_STATE_START = "TRIGGER_DONE_STATE_START";
    public const string TRIGGER_DONE_STATE_FINISH = "TRIGGER_DONE_STATE_FINISH";

    public const string PREPARE_NEXTTURN_STATE_START = "PREPARE_NEXTTURN_STATE_START";
    public const string PREPARE_NEXTTURN_STATE_FINISH = "PREPARE_NEXTTURN_STATE_FINISH";

    public const string ENDGAME_STATE = "ENDGAME_STATE";
}


