using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BoostersConfig", menuName = "IntegrationUI/BoostersConfig")]
public class BoostersConfig : ScriptableObject
{
    public List<Booster> boosters = new List<Booster>();

    public float thirdBoosterRefreshProbability = 0.5f;
}

public enum BoostersType
{
    Shield,
    Watch,
    Bomb,
    BlueSpell,
    RedSpell,
    GreenSpell
}

[Serializable]
public class Booster
{
    public BoostersType type;
    public string name;
    public Sprite icon;
}
