////==============================================================
////  Copyright (C) 2017 
////  Create by ChengBo at 2017/6/12 18:01:17.
////  Version 1.0
////  Administrator 
////==============================================================
//
//using XLua;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using System.Linq;
//using Game;
//using Ngame.Catnip;
//using UnityEngine;
//using UnityEngine.Events;
//
//public static class LuaConfig
//{
//    [LuaCallCSharp]
//    public static List<Type> LuaCallCSharp = new List<Type>() {
//                typeof(System.Object),
//                typeof(UnityEngine.Object),
//                typeof(UnityEngine.SceneManagement.SceneManager),
//                typeof(Vector2),
//                typeof(Vector3),
//                typeof(Vector4),
//                typeof(Quaternion),
//                typeof(Color),
//                typeof(Ray),
//                typeof(Bounds),
//                typeof(Ray2D),
//                typeof(Time),
//                typeof(GameObject),
//                typeof(Component),
//                typeof(Material),
//                typeof(Behaviour),
//                typeof(Transform),
//                typeof(Resources),
//                typeof(TextAsset),
//                typeof(Keyframe),
//                typeof(AnimationCurve),
//                typeof(AnimationClip),
//                typeof(MonoBehaviour),
//                typeof(ParticleSystem),
//                typeof(SkinnedMeshRenderer),
//                typeof(Renderer),
//                typeof(SpriteRenderer),
//                typeof(Sprite),
//                typeof(WWW),
//                typeof(IEnumerator),
//                typeof(List<int>),
//
//                typeof(Action),
//                typeof(Action<bool>),
//                typeof(Action<int>),
//                typeof(Action<float>),
//                typeof(Action<string>),
//                typeof(Action<GameObject>),
//                typeof(Action<Transform>),
//                typeof(Action<object>),
//                typeof(UnityAction),
//                typeof(UnityAction<bool>),
//                typeof(UnityAction<int>),
//                typeof(UnityAction<float>),
//                typeof(UnityAction<string>),
//                typeof(UnityAction<object>),
//
//                typeof(Debug),
//                typeof(CEventTouchDistrubtion.TouchEventDistrubtionCall),
//                typeof(AnimationEventController.OnAnimationClipEvent),
//                typeof(Ngame.Design.UnlockType),
//                typeof(NetProto.Proto.FunctionID),
//                typeof(ExtentionUnity),
//                typeof(HonJinGod.HonJinTopology),
//                typeof(Ngame.Design.DisplayType),
//                typeof(UnityEngine.RectTransform),
//                typeof(HonJinGod.ArchContext),
//                typeof(BlackFadeInPanel),
//                
//                //battle data type
//                typeof(Ngame.Fundamental.FixPoint.Fix64),
//                typeof(Ngame.Catnip.NumericContext.ModifierUnit),
//            };
//
//
//    /// <summary>
//    /// dotween的扩展方法在lua中调用
//    /// </summary>
//    [LuaCallCSharp]
//    [ReflectionUse]
//    public static List<Type> dotween_lua_call_cs_list = new List<Type>()
//    {
//        typeof(DG.Tweening.AutoPlay),
//        typeof(DG.Tweening.AxisConstraint),
//        typeof(DG.Tweening.Ease),
//        typeof(DG.Tweening.LogBehaviour),
//        typeof(DG.Tweening.LoopType),
//        typeof(DG.Tweening.PathMode),
//        typeof(DG.Tweening.PathType),
//        typeof(DG.Tweening.RotateMode),
//        typeof(DG.Tweening.ScrambleMode),
//        typeof(DG.Tweening.TweenType),
//        typeof(DG.Tweening.UpdateType),
//
//        typeof(DG.Tweening.DOTween),
//        typeof(DG.Tweening.DOVirtual),
//        typeof(DG.Tweening.EaseFactory),
//        typeof(DG.Tweening.Tweener),
//        typeof(DG.Tweening.Tween),
//        typeof(DG.Tweening.Sequence),
//        typeof(DG.Tweening.TweenParams),
//        typeof(DG.Tweening.Core.ABSSequentiable),
//
//        typeof(DG.Tweening.Core.TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions>),
//
//        typeof(DG.Tweening.TweenCallback),
//        typeof(DG.Tweening.TweenExtensions),
//        typeof(DG.Tweening.TweenSettingsExtensions),
//        typeof(DG.Tweening.ShortcutExtensions),
//        typeof(DG.Tweening.ShortcutExtensions43),
//        typeof(DG.Tweening.ShortcutExtensions46),
//        typeof(DG.Tweening.ShortcutExtensions50),
//
//        typeof(Spine.Unity.SkeletonExtensions),       
//        //dotween pro 的功能
//        //typeof(DG.Tweening.DOTweenPath),
//        //typeof(DG.Tweening.DOTweenVisualManager),
//    };
//
//    [CSharpCallLua]
//    public static List<Type> CSharpCallLua = new List<Type>()
//        {
//            typeof(Action),
//            typeof(Action<int, string>),
//            typeof(Action<string>),
//            typeof(Action<object>),
//            typeof(UnityAction<bool>),
//            typeof(Action<int>),
//            typeof(Action<string,bool,string>),
//            typeof(Action<CatnipEngine>),
//            typeof(Action<int, string, int>),
//            typeof(Action<ItemData>),
//            typeof(Action<ChapterConfig, bool, bool>),
//            typeof(Action<GameObject, string>),
//            typeof(UnityAction<int>),
//            typeof(IEnumerator),
//
//            typeof(CEvent.EventCallBack),
//            typeof(CEventTouchDistrubtion.TouchEventDistrubtionCall),
//            typeof(AnimationEventController.OnAnimationClipEvent),
//            typeof(Action<Common.Net.Simple.Command, NetProto.Proto.ErrCode>),
//            typeof(System.Action<int, int>),
//            typeof(UnityEngine.Events.UnityAction<UnityEngine.Vector2>),
//            typeof(CProcess.ProcessDelegete),
//            typeof(System.Action<SpineAnimationMono>),
//            typeof(System.Action<int, Game.LotteryDataList>),
//            typeof(Action<bool>),
//            typeof(Func<Ngame.PlayerArmory.Hero, int>),
//            typeof(Func<string, bool>),
//            typeof(Func<int>),
//            typeof(Func<string, string>),
//            typeof(UnityAction<float>),
//            typeof(Action<Ngame.Catnip.Zaku, Ngame.Catnip.ConfigDataType.MoveInfo, Ngame.Catnip.CatnipContext>),
//            typeof(StageManager.IsStageDoneDelegate),
//            typeof(ToggleItemGroup.OnToggleItemSelected),
//            typeof(Action<object, Action>),
//            typeof(Func<long, string>),
//            typeof(Func<DateTime, string>),
//            typeof(Action<Game.PlayerInfoData>),
//            typeof(Action<int, Action, int>),//商店在使用
//            typeof(Action<int, int, Action, string>),//商店在使用
//            typeof(Action<int, int, string>),//商店在使用
//        };
//
//
//    //黑名单
//    [BlackList]
//    public static List<List<string>> BlackList = new List<List<string>>()  {
//                new List<string>(){"UnityEngine.WWW", "movie"},
//                new List<string>(){"UnityEngine.WWW", "GetMovieTexture"},
//                new List<string>(){"UnityEngine.Texture2D", "alphaIsTransparency"},
//                new List<string>(){"UnityEngine.Security", "GetChainOfTrustValue"},
//                new List<string>(){"UnityEngine.CanvasRenderer", "onRequestRebuild"},
//                new List<string>(){"UnityEngine.Light", "areaSize"},
//                new List<string>(){"UnityEngine.AnimatorOverrideController", "PerformOverrideClipListCleanup"},
//    #if !UNITY_WEBPLAYER
//                new List<string>(){"UnityEngine.Application", "ExternalEval"},
//    #endif
//                new List<string>(){"UnityEngine.GameObject", "networkView"}, //4.6.2 not support
//                new List<string>(){"UnityEngine.Component", "networkView"},  //4.6.2 not support
//                new List<string>(){"System.IO.FileInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
//                new List<string>(){"System.IO.FileInfo", "SetAccessControl", "System.Security.AccessControl.FileSecurity"},
//                new List<string>(){"System.IO.DirectoryInfo", "GetAccessControl", "System.Security.AccessControl.AccessControlSections"},
//                new List<string>(){"System.IO.DirectoryInfo", "SetAccessControl", "System.Security.AccessControl.DirectorySecurity"},
//                new List<string>(){"System.IO.DirectoryInfo", "CreateSubdirectory", "System.String", "System.Security.AccessControl.DirectorySecurity"},
//                new List<string>(){"System.IO.DirectoryInfo", "Create", "System.Security.AccessControl.DirectorySecurity"},
//                new List<string>(){"UnityEngine.MonoBehaviour", "runInEditMode"},
//                new List<string>(){"UnityEditor.BuildTarget"},
//            };
//
//
//    [Hotfix]
//    public static List<Type> by_property
//    {
//        get
//        {
//            Assembly assembly = Assembly.Load("Assembly-CSharp");
//            List<Type> list = (from type in assembly.GetTypes()
//                               where (!type.Name.EndsWith("MetaMono")
//                                   && (string.IsNullOrEmpty(type.Namespace)
//                                       || (!type.Namespace.Contains("TutorialDesigner")
//                                                && !type.Namespace.Contains("Mono.Math")
//                                                && !type.Namespace.Contains("Mono.Xml")
//                                                && !type.Namespace.Contains("Mono.Math.Prime")
//                                                && !type.Namespace.Contains("Mono.Math.Prime.Generator")
//                                                && !type.Namespace.Contains("Mono.Security.Cryptography")
//                                                && !type.Namespace.Contains("Spine")
//                                                && !type.Namespace.Contains("Com.LuisPedroFonseca.ProCamera2D")
//                                                && !type.Namespace.Contains("CinemaDirector")
//                                                && !type.Namespace.Contains("TMPro")
//                                                && !type.Namespace.Contains("CriMana")
//                                                && !type.Namespace.Contains("LitJson")
//                                                && !type.Namespace.Contains("Ionic")
//                                                && !type.Namespace.Contains("GCloud")
//                                                && !type.Namespace.Contains("TDM")
//                                                && !type.Namespace.Contains("UnityEngine.AssetGraph")
//                                                && !type.Namespace.Contains("BehaviorDesigner")
//                                                && !type.Namespace.Contains("FxPro")
//                                                && !type.Namespace.Contains("B83.ExpressionParser")
//                                                && !type.Namespace.Contains("Chronos")
//                                                && !type.Namespace.Contains("CSharpZombieDetector")
//                                                && !type.Namespace.Contains("FastShadowReceiver")
//                                                && !type.Namespace.Contains("FlyingWormConsole3")
//                                                && !type.Namespace.Contains("FxProNS")
//                                                && !type.Namespace.Contains("Jing.ULiteWebView")
//                                                && !type.Namespace.Contains("NodeEditorFramework")
//                                                && !type.Namespace.Contains("Nyahoon")
//                                                && !type.Namespace.Contains("SecurityDataType.V2")
//                                                && !type.Namespace.Contains("SharpJson")
//                                                && !type.Namespace.Contains("Sproto")
//                                                && !type.Namespace.Contains("TransferGraph")
//                                                && !type.Namespace.Contains("tss.")
//                                                && !type.Namespace.Contains("UITween.")
//                                                && !type.Namespace.Contains("UnityEngine.AssetGraph")
//                                                && !type.Namespace.Contains("UnityEngine.UI")
//                                                && !type.Namespace.Contains("VerificationBattle.Data")
//                                                && !type.Namespace.Contains("Msdk.")
//                                                && !type.Namespace.Contains("ThinkingAnalytics.")
//                                                )))
//                               select type).ToList();
//
//
//            Assembly assemblyFirstpass = Assembly.Load("Assembly-CSharp-firstpass");
//            List<Type> listFirstpass = (from type in assemblyFirstpass.GetTypes()
//                                        where (!type.Name.EndsWith("MetaMono")
//                                            && (string.IsNullOrEmpty(type.Namespace)
//                                                 || (!type.Namespace.Contains("TutorialDesigner")
//                                                        && !type.Namespace.Contains("Mono.Math")
//                                                        && !type.Namespace.Contains("Mono.Xml")
//                                                        && !type.Namespace.Contains("Mono.Math.Prime")
//                                                        && !type.Namespace.Contains("Mono.Math.Prime.Generator")
//                                                        && !type.Namespace.Contains("Mono.Security.Cryptography")
//                                                        && !type.Namespace.Contains("Spine")
//                                                        && !type.Namespace.Contains("Com.LuisPedroFonseca.ProCamera2D")
//                                                        && !type.Namespace.Contains("CinemaDirector")
//                                                        && !type.Namespace.Contains("TMPro")
//                                                        && !type.Namespace.Contains("CriMana")
//                                                        && !type.Namespace.Contains("LitJson")
//                                                        && !type.Namespace.Contains("Ionic")
//                                                        && !type.Namespace.Contains("GCloud")
//                                                        && !type.Namespace.Contains("TDM")
//                                                        && !type.Namespace.Contains("UnityEngine.AssetGraph")
//                                                        && !type.Namespace.Contains("BehaviorDesigner")
//                                                        && !type.Namespace.Contains("UnityStandardAssets.Cameras")
//                                                        )))
//                                        select type).ToList();
//
//            list.AddRange(listFirstpass);
//
//            list.Remove(assembly.GetType("EditorTarget"));
//            list.Remove(assembly.GetType("TargetSelector"));
//            list.Remove(assembly.GetType("StoryMetaBaseMono"));
//            list.Remove(assembly.GetType("StoryEditorPanel"));
//            list.Remove(assembly.GetType("AVGEditorPanel"));
//            list.Remove(assembly.GetType("WwiseSetupWizard"));
//            list.Remove(assembly.GetType("WwiseSettings"));
//            list.Remove(assembly.GetType("NgAsset"));
//            list.Remove(assembly.GetType("NgSerialized"));
//            list.Remove(assembly.GetType("NgTexture"));
//            list.Remove(assembly.GetType("MersenneTwister"));
//            list.Remove(assembly.GetType("Ngame.Catnip.NumericContext.Zion"));
//            list.Remove(assembly.GetType("System.gstring"));
//            list.Remove(assembly.GetType("XMLLanguageParser"));
//            list.Remove(assembly.GetType("ExcelMakerManager"));
//            list.Remove(assembly.GetType("FindText"));
//            list.Remove(assembly.GetType("NotificationManager"));
//            list.Remove(assembly.GetType("AppsFlyer"));
//            list.Remove(assembly.GetType("AFInAppEvents"));
//            list.Remove(assembly.GetType("AppsFlyerTrackerCallbacks"));
//            list.Remove(assembly.GetType("DwonloadAbConfig"));
//            list.Remove(assembly.GetType("Tracking"));
//
//            list.Remove(assemblyFirstpass.GetType("DwonloadAbConfig"));
//            list.Remove(assemblyFirstpass.GetType("BuglyAgent"));
//
//            int index = list.Count - 1;
//            while (index >= 0)
//            {
//                string fullName = list[index].FullName;
//
//                if (fullName.StartsWith("Ak")
//                    || fullName.StartsWith("Fxm")
//                    || fullName.StartsWith("FXM")
//                    || fullName.StartsWith("Cri")
//                    || fullName.StartsWith("Amplify"))
//                {
//                    list.RemoveAt(index);
//                }
//
//                index--;
//            }
//
//            return list;
//        }
//    }
//}
//
//
