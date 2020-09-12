using System;
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
    
    //初始化技能
    void InitSkill(SkillData data)
    {
        data.skillPrefab = Resources.Load<GameObject>(data.prefabName);
        data.owner = gameObject;
    }
    
    //技能释放条件:冷却 法力
    public SkillData prepareSkill(int id)
    {
        //根据ID查找技能数据
        SkillData data = Find(s => s.skillID == id);
        //判断条件

        //返回技能数据
        return data;
    }

    SkillData Find(Func<SkillData, bool > handler)
    {
        for (int i = 0; i < skilldatas.Length; i++)
        {
            if (handler(skilldatas[i]))
            {
                return skilldatas[i];
            }
        }

        return null;
    }
    
    //生成技能
    public void GenterateSkill(SkillData data)
    {
        //创建技能
        GameObject skillGo = Instantiate(data.skillPrefab, transform.position, transform.rotation);
        //销毁技能
        Destroy(skillGo, data.durationTime);
        //开启技能冷却
        StartCoroutine(CoolTimeDown(data));
    }

    //技能冷却
    IEnumerator CoolTimeDown(SkillData data)
    {
        data.coolRemain = data.coolTime;
        while (data.coolRemain > 0)
        {
            yield return new WaitForSeconds(1);
            data.coolRemain--;   
        }
    }
}

































