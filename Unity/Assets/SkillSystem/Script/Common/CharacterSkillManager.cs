using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSkillManager : MonoBehaviour
{
    public SkillData[] skilldatas;
    
    private void Start()
    {
        for(int i = 0; i<skilldatas.Length;i++)
        {
            InitSkill(skilldatas[i]);
        }
    }
    public void InitSkill(SkillData data)
    {
        
    }
    //生成技能
    public void GenterateSkill()
    {

    }
    
}
