using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class DelayObject
{
    [SerializeField]
    public float delayTime;
    public GameObject delayObject;
}
public class SetDelayTime : MonoBehaviour {
    public List<DelayObject> delayObj = new List<DelayObject>(0);
    float t = 0;
    // Use this for initialization
    void OnEnable () {
        t = 0;
        for(int i = 0; i < delayObj.Count; i++)
        {
            if (delayObj[i].delayObject)
            {
                delayObj[i].delayObject.SetActive(false);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        t += Time.deltaTime;
        for(int i = 0; i < delayObj.Count; i++)
        {
            if(delayObj[i].delayTime > 0 && delayObj[i].delayObject)
            {
                if (t > delayObj[i].delayTime)
                {
                    delayObj[i].delayObject.SetActive(true);
                }
            }
        }
	}
}
