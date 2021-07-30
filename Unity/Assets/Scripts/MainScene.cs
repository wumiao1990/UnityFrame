using System.Collections;
using System.Collections.Generic;
using SUIFW;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {      
        UIManager.Instance.ShowUIForms(ProConst.MAIN_CITY_UIFORM);		
        UIManager.Instance.ShowUIForms(ProConst.HERO_INFO_UIFORM);		
    }
}
