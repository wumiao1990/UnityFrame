using System.Collections.Generic;
using UnityEngine;

public class AnimationEventController : MonoBehaviour
{

    public class EventListener
    {
        public float FrameTime;
        public System.Action Call;
        private bool _isExcuted = false;
        public void Excute()
        {
            _isExcuted = true;
            if (Call == null) return;
            Call();
        }

        public void Reset()
        {
            _isExcuted = false;
        }
    }
    private Animator _anim;
    public UnityEngine.Animator Anim
    {
        get { return _anim; }
    }
    private Dictionary<string, List<EventListener>> _events = new Dictionary<string, List<EventListener>>();
    private string _curStateName;
    AnimationClip aniClip;
    //对Animation里动画进行采样
    Animation animtion;

    private void Awake()
    {
        _anim = GetComponent<Animator>();

        if(_anim != null)
        {
            RuntimeAnimatorController controller = _anim.runtimeAnimatorController;
            if (controller != null)
            {
                AnimationClip[] clips = controller.animationClips;
                aniClip = clips.Length > 0 ? clips[0] : null;
            }
        }             

        animtion = GetComponent<Animation>();
    }
    //滑动条改变事件回调函数
    public void OnSliderChanged(float value, float speed = 1)
    {
        float time = aniClip.length * value;
        _anim.speed = speed;              
        aniClip.SampleAnimation(gameObject, time);        
        _anim.enabled = false;
    }

    // Use this for initialization
    void Start()
    {
        updateCurState();

    }

    public void Play(string stateName, float ntime = 0)
    {
        if (!_anim.enabled)
        {
            _anim.enabled = true;
        }
        _anim.Play(stateName, 0, ntime);
        _curStateName = stateName;

        if (_events.ContainsKey(_curStateName))
        {
            List<EventListener> list = _events[_curStateName];
            foreach (EventListener iten in list)
            {
                iten.Reset();
            }
        }
    }

    public void SetParam<T>(string name, object o)
    {
        if (typeof(bool) == typeof(T))
        {
            _anim.SetBool(name, (bool)o);
        }
        if (typeof(float) == typeof(T))
        {
            _anim.SetFloat(name, (float)o);
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateCurState();
    }

    private void updateCurState()
    {
        if (_anim == null || _anim.runtimeAnimatorController == null)
        {
            return;
        }

        AnimatorStateInfo cur = _anim.GetCurrentAnimatorStateInfo(0);
        if (_curStateName != null && cur.IsName(_curStateName))
        {
            if (_events.ContainsKey(_curStateName))
            {
                List<EventListener> list = _events[_curStateName];
                foreach (EventListener iten in list)
                {
                    if (iten.FrameTime < cur.normalizedTime)
                    {
                        iten.Excute();
                    }
                }
            }
            return;
        }

        _curStateName = null;
        Dictionary<string, List<EventListener>>.Enumerator element = _events.GetEnumerator();
        while (element.MoveNext())
        {
            if (cur.IsName(element.Current.Key))
            {

                _curStateName = element.Current.Key;
                break;
            }
        }
    }

    public void AddFrameEvent(string stateName, float time, System.Action call)
    {
        if (!_events.ContainsKey(stateName))
        {
            _events.Add(stateName, new List<EventListener>());
        }
        EventListener listner = new EventListener();
        listner.FrameTime = time;
        listner.Call = call;
        _events[stateName].Add(listner);// += call;
    }
    public delegate void OnAnimationClipEvent(int tag);
    private OnAnimationClipEvent _animationListender;
    public void AddAnimationClipEvent(OnAnimationClipEvent call)
    {
        _animationListender += call;
    }
    public void OnReceiveAnimtionClipEvent(int eventTag)
    {
        if (_animationListender != null)
        {
            _animationListender.Invoke(eventTag);
        }
    }
    public void ClearAnimationClipEvent()
    {
        _animationListender = null;
    }
}
