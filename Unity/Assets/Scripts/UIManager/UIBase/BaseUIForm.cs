﻿/***
 * 
 *     
 *           主题: UI窗体的父类
 *    Description: 
 *           功能：定义所有UI窗体的父类。
 *           定义四个生命周期
 *           
 *           1：Display 显示状态。
 *           2：Hiding 隐藏状态
 *           3：ReDisplay 再显示状态。
 *           4：Freeze 冻结状态。
 *           
 *                  
 *     
 *     
 *    
 *   
 */
using UITools;
using UnityEngine;
using UnityEngine.UI;

namespace SUIFW
{
	public class BaseUIForm : ICBSource<GameObject>, IBindableUI {
		
		private GameObject _gameObject;
		public GameObject gameObject
		{
			get
			{
				return _gameObject;
			}
		}
		public GameObject Source
		{
			get
			{
				return _gameObject;
			}
			set
			{
				_gameObject = value;
			}
		}
		
        /*字段*/
        private UIType _CurrentUIType=new UIType();

        /* 属性*/
        //当前UI窗体类型
	    public UIType CurrentUIType
	    {
	        get { return _CurrentUIType; }
	        set { _CurrentUIType = value; }
	    }

        public string PanelName;

        #region  窗体的五种(生命周期)状态

        /// <summary>
        /// 准备状态
        /// </summary>
        public virtual void OnReady(){}
        
        /// <summary>
        /// 显示状态
        /// </summary>
	    public virtual void Display()
	    {
	        this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIForms_Type==UIFormType.PopUp)
            {
                UIMaskMgr.Instance.SetMaskWindow(this.gameObject,_CurrentUIType.UIForm_LucencyType);
            }
	    }

        /// <summary>
        /// 隐藏状态
        /// </summary>
	    public virtual void Hiding()
	    {
            this.gameObject.SetActive(false);
            //取消模态窗体调用
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.Instance.CancelMaskWindow();
            }
        }

        /// <summary>
        /// 重新显示状态
        /// </summary>
	    public virtual void Redisplay()
	    {
            this.gameObject.SetActive(true);
            //设置模态窗体调用(必须是弹出窗体)
            if (_CurrentUIType.UIForms_Type == UIFormType.PopUp)
            {
                UIMaskMgr.Instance.SetMaskWindow(this.gameObject, _CurrentUIType.UIForm_LucencyType);
            }
        }

        /// <summary>
        /// 冻结状态
        /// </summary>
	    public virtual void Freeze()
	    {
            this.gameObject.SetActive(true);
        }
        
        /// <summary>
        /// 销毁
        /// </summary>
        protected virtual void OnDispose()
        {
            
        }

        #endregion

        #region 封装子类常用的方法

        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
	    protected void RigisterButtonObjectEvent(string buttonName,EventTriggerListener.VoidDelegate  delHandle)
	    {
            Component goButton = UnityHelper.FindTheChildNode(this.gameObject, buttonName);
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }	    
        }
        
        /// <summary>
        /// 注册按钮事件
        /// </summary>
        /// <param name="buttonName">按钮节点名称</param>
        /// <param name="delHandle">委托：需要注册的方法</param>
        protected void RigisterButtonObjectEvent(Component goButton,EventTriggerListener.VoidDelegate  delHandle)
        {
            //给按钮注册事件方法
            if (goButton != null)
            {
                EventTriggerListener.Get(goButton).onClick = delHandle;
            }	    
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormName"></param>
	    protected void OpenUIForm(string uiFormName)
        {
            UIManager.Instance.ShowUIForms(uiFormName);
        }

        /// <summary>
        /// 关闭当前UI窗体
        /// </summary>
        protected void CloseUIForm()
	    {
	        string strUIFromName = string.Empty;            //处理后的UIFrom 名称
	        int intPosition = -1;
            strUIFromName = PanelName;//GetType().ToString();             //命名空间+类名
            intPosition=strUIFromName.IndexOf('.');
            if (intPosition!=-1)
            {
                //剪切字符串中“.”之间的部分
                strUIFromName = strUIFromName.Substring(intPosition + 1);
            }

            UIManager.Instance.CloseUIForms(strUIFromName);
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="msgType">消息的类型</param>
        /// <param name="msgName">消息名称</param>
        /// <param name="msgContent">消息内容</param>
	    protected void SendMessage(string msgType,string msgName,object msgContent)
	    {
            KeyValuesUpdate kvs = new KeyValuesUpdate(msgName,msgContent);
            MessageCenter.SendMessage(msgType, kvs);	    
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="messagType">消息分类</param>
        /// <param name="handler">消息委托</param>
	    public void ReceiveMessage(string messagType,MessageCenter.DelMessageDelivery handler)
	    {
            MessageCenter.AddMsgListener(messagType, handler);
	    }

        /// <summary>
        /// 显示语言
        /// </summary>
        /// <param name="id"></param>
	    public string Show(string id)
        {
            string strResult = string.Empty;

            strResult = LauguageMgr.GetInstance().ShowText(id);
            return strResult;
        }

	    #endregion

	    #region Update实现
		private bool _isActiveUpdate = false;
        protected bool ActiveUpdate
        {
            set
            {
                if (value)
                {
                    if (_isActiveUpdate) return;
                    UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                    if (hd == null)
                    {
                        hd = Source.AddComponent<UpdateHandle>();
                    }
                    else
                    {
                        hd.enabled = true;
                    }
                    hd.UpdateDelegete = Update;
                    _isActiveUpdate = value;
                }
                else
                {
                    UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                    if (hd != null)
                    {
                        hd.enabled = false;
                    }
                    _isActiveUpdate = value;
                }
            }
        }
        
        private bool _isActiveLateUpdate = false;
        protected bool ActiveLateUpdate
        {
            set
            {
                if (value)
                {
                    if (_isActiveLateUpdate) return;
                    UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                    if (hd == null)
                    {
                        hd = Source.AddComponent<UpdateHandle>();
                    }
                    else
                    {
                        hd.enabled = true;
                    }
                    hd.LateUpdateDelegete = LateUpdate;
                    _isActiveLateUpdate = value;
                }
                else
                {
                    UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                    if (hd != null)
                    {
                        hd.enabled = false;
                    }
                    _isActiveLateUpdate = value;
                }
            }
        }


        private bool _isActiveFixedUpdate = false;
        protected bool ActiveFixedUpdate
        {
            set
            {
                if (value)
                {
                    if (!_isActiveFixedUpdate)
                    {
                        UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                        if (hd == null)
                        {
                            hd = Source.AddComponent<UpdateHandle>();
                        }
                        else
                        {
                            hd.enabled = true;
                        }
                        hd.FixedUpdateDelegete = FixedUpdate;

                    }

                    _isActiveFixedUpdate = value;
                }
                else
                {
                    UpdateHandle hd = Source.GetComponent<UpdateHandle>();
                    if (hd != null)
                    {
                        hd.enabled = false;
                    }
                    _isActiveFixedUpdate = value;
                }
            }
        }

        protected void Update()
        {
            onUpdate();
        }

        protected virtual void onUpdate()
        {

        }

        protected void LateUpdate()
        {
            onLateUpdate();
        }

        protected virtual void onLateUpdate()
        {

        }


        protected void FixedUpdate()
        {
            onFixedUpdate();
        }

        protected virtual void onFixedUpdate()
        {

        }

	    #endregion
    }
}