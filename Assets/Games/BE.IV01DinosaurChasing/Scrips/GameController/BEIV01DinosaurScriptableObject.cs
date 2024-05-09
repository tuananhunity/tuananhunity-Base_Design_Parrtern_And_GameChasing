using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BEIV01DinosaurScriptableObject", menuName = "ScriptableObjects/BEIV01Dinosaur/Setting", order = 1)]
public class BEIV01DinosaurScriptableObject: ScriptableObject
{
    public BEIV01DinosaurIntroConfig introDataConfig;
    public BEIV01DinosaurGuidingConfig guidingDataConfig;
    public BEIV01DinosaurTriggerConfig triggerDataConfig;
    public BEIV01DinosaurEndGameConfig endGameDataConfig;
}

[Serializable]
public class BEIV01DinosaurIntroConfig
{
    public AudioClip audioDelicious;
    public AudioClip audioOhno;
    public AudioClip audioOpenMount;
    public AudioClip audioDinosaurMove;
    public AudioClip audioMaxMove;
    public AudioClip soundTrack;
    public AudioClip audioGetEgg;
    public AudioClip musicGame;
    public float volumeMusic;
}

[Serializable]
public class BEIV01DinosaurGuidingConfig
{
    public List<AudioClip> lstAudioGuiding;
}
[Serializable]
public class BEIV01DinosaurTriggerConfig
{
    public AudioClip audioMaxMove;
    public AudioClip audioAttack;
    public AudioClip audioDinosaurMove;
}
[Serializable]
public class BEIV01DinosaurEndGameConfig
{
    public AudioClip audioPanic;
    public AudioClip audioLegDinosaurMom;
    public AudioClip audioBreakEgg;
    public AudioClip audioDinosaurChildBlink;
    public AudioClip audioAttack;
}