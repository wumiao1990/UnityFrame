using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SkillData
{
    /// <summary>技能ID</summary>
    public int skillID;
    
    /// <summary>技能名称</summary>
    public string name;
    
    /// <summary>技能描述</summary>
    public string description;
    
    /// <summary>冷却时间</summary>
    public int coolTime;
    
    /// <summary>冷却剩余时间</summary>
    public int coolRemain;
    
    /// <summary>魔法消耗</summary>
    public int costSP;
    
    /// <summary>攻击距离</summary>
    public float attackDistance;
    
    /// <summary>攻击角度</summary>
    public float attackAngle;
    
    /// <summary>攻击目标</summary>
    public string[] attackTargetTags = {"Enemy"};
    
    /// <summary>攻击目标对象组</summary>
    [HideInInspector]
    public Transform[] attackTargets;
    
    /// <summary>技能影响类型</summary>
    public string[] impactType = {"CostSP","Damage"};
    
    /// <summary>连击的下个技能ID</summary>
    public int nextBattleID;
    
    /// <summary>伤害比率</summary>
    public float atkRatio;
    
    /// <summary>持续时间</summary>
    public float durationTime;
    
    /// <summary>伤害间隔</summary>
    public float atkInterval;
    
    /// <summary>技能所属</summary>
    [HideInInspector]
    public GameObject owner;
    
    /// <summary>技能prefab</summary>
    public string prefabName;
    
    /// <summary>prefab对象</summary>
    [HideInInspector]
    public GameObject skillPrefab;
    
    /// <summary>动画名称</summary>
    public string animationName;
    
    /// <summary>受击特效名称</summary>
    public string hitFixName;
    
    /// <summary>受击特效prefab</summary>
    [HideInInspector]
    public GameObject hitFixPrefab;
    /// <summary>技能等级</summary>
    public int level;
    
    /// <summary>攻击类型 单体，群攻</summary>
    public SkillAttackType attackType;
    
    /// <summary>选择类型 扇形，园形</summary>
    public SelectorType selectorType;
}

public enum SkillAttackType
{
    Single,
    Group,
}

public enum SelectorType
{
    sector,
    rounded,
}























