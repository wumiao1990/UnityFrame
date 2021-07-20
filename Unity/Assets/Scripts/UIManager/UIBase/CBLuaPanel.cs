using UnityEngine;
using XLua;
using System;
using System.Collections.Generic;
using SUIFW;
using UITools;

namespace SUIFW
{
    public class CBLuaPanel : BaseUIForm
    {
        public LuaTable _table = null;
        private int _layOut = -1;
        private string _file;

        private Action m_OnReady;
        private Action m_OnDispose;
        private Action m_OnHiding;
        
        public CBLuaPanel(string file, LuaTable v, UIControlData uiControlData)
        {
            _file = file;
            _table = v;
            _table.Get("OnReady", out m_OnReady);
            _table.Get("OnHiding", out m_OnHiding);
            _table.Get("OnDispose", out m_OnDispose);
            
            uiControlData.BindDataToLua(this, _table);
        }

        public override void OnReady()
        {
            base.OnReady();
            _table.Set<string, Action>("CloseUIForm", CloseUIForm);
            _table.Set<string, Action<Component, EventTriggerListener.VoidDelegate>>("RigisterButtonObjectEvent", RigisterButtonObjectEvent);
            _table.Set("Source", Source);
            _table.Set("CsharpPanel", this);

            if (m_OnReady != null)
                m_OnReady.Invoke();
        }

        public void SetUpdate(bool isActive)
        {
            ActiveUpdate = isActive;
        }
        
        protected override void onUpdate()
        {
            base.onUpdate();
            _table._DoFunction("OnUpdate");
        }
      
        protected override void OnDispose()
        {
            base.OnDispose();

            if (m_OnDispose != null)
                m_OnDispose.Invoke();

            m_OnReady = null;
            m_OnDispose = null;
            m_OnHiding = null;
           
            if (_table != null)
                _table.Dispose();
            _table = null;
        }

        /// <summary>
        /// 调用lua函数
        /// </summary>
        public void ExcAction(string funName, params object[] args)
        {
            _table._DoFunction(funName, args);
        }


        public T GetCustomFunc<T>(string funcName)
        {
            if (_table.ContainsKey(funcName))
            {
                T action;
                _table.Get(funcName, out action);
                return action;
            }
            else
            {
                return default(T);
            }
        }
        
        public override void Freeze()
        {
            base.Freeze();
            Debug.LogError("Freeze");
        }

        public override void Redisplay()
        {
            base.Redisplay();
            Debug.LogError("Redisplay");
        }

        public override void Hiding()
        {
            base.Hiding();
            if (m_OnHiding != null)
                m_OnHiding.Invoke();
        }
    }
}


