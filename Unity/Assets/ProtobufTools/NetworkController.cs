using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using UnityEngine.SceneManagement;
public class NetworkController
{
    #region 与服务器数据交互，通过EasyLoom类进行线程间通信
    public static void Receive(CmdEnum cmd,byte[] data)
    {
        EasyLoom.Instance.AddReceiveInfo(new LoomInfo(cmd, data));
    }

    public static void Request(CmdEnum cmd,byte[] data)
    {
        EasyLoom.Instance.AddSendInfo(new LoomInfo(cmd, data));
    }
    #endregion


    public static void SendData(CmdEnum cmd, byte[] data)
    {
        SocketManager.Instance.mSocket.SendMessage(data);
    }

    public static void ReceiveData(CmdEnum cmd, byte[] data)
    {
        if (cmd == CmdEnum.ResLogin)
        {
            ResponseLogin login = SerializeTool.Deserialize<ResponseLogin>(data);
            ResponseLogin(login);
        }
        else if (cmd == CmdEnum.ResAlive)
        {
            ResponseAlive alive = SerializeTool.Deserialize<ResponseAlive>(data);
            ResponseAlive(alive);
        }
        else if (cmd == CmdEnum.ResRegister)
        {
            ResponseRegister response = SerializeTool.Deserialize<ResponseRegister>(data);
            ResponseRegister(response);
        }
        else if (cmd == CmdEnum.ResCreateRoom)
        {
            ResponseCreateRoom response = SerializeTool.Deserialize<ResponseCreateRoom>(data);
            ResponseCreateRoom(response);
        }
        else if (cmd == CmdEnum.ResGetRooms)
        {
            ResponseGetRooms response = SerializeTool.Deserialize<ResponseGetRooms>(data);
            ResponseGetRooms(response);
        }
        else if (cmd == CmdEnum.ResDeleteRoom)
        {
            ResponseDeleteRoom response = SerializeTool.Deserialize<ResponseDeleteRoom>(data);
            ResponseDeleteRoom(response);
        }
    }

    #region 发收消息的具体逻辑处理

    //请求注册
    public static void RequestRegister(string account, string username, string password)
    {
        RequestRegister req = new RequestRegister()
        {
            Proto = (int)CmdEnum.ReqRegister,
            Account = account,
            Username = username,
            Password = password
        };
        byte[] data = ProtobufTool.CreateData(req.Proto, req);
        Request((CmdEnum)req.Proto, data);
    }

    //返回注册
    public static void ResponseRegister(ResponseRegister response)
    {
        if (string.IsNullOrEmpty(response.Error))
        {
            Debug.Log("注册成功:account" + response.PlayerInfo.Account);
            UIController.Instance.CancelRegister();
        }
    }

    //请求登录
    public static void RequestLogin(string username, string password)
    {
        RequestLogin req = new RequestLogin()
        {
            Proto = (int)CmdEnum.ReqLogin,
            Account = username,
            Password = password
        };
        byte[] data = ProtobufTool.CreateData(req.Proto, req);
        Request((CmdEnum)req.Proto, data);
    }

    //返回登录
    public static void ResponseLogin(ResponseLogin result)
    {
        #region 测试代码
        if(UIController.Instance != null)
        {
            if (result.Error != "")
            {
                Debug.LogWarning(result.Error + result.Result);
            }
            else
            {
                Debug.Log(result.Result);
                UIController.Instance.login = true;
                GameController.Instance.player = result.Player;
                SceneManager.LoadScene("sample-2");
            }
        }
        #endregion
    }

    //请求心跳
    public static void RequestAlive()
    {
        RequestAlive req = new RequestAlive()
        {
            Proto = (int)CmdEnum.ReqAlive
        };
        byte[] data = ProtobufTool.CreateData(req.Proto, req);
        Request((CmdEnum)req.Proto, data);
    }

    //返回心跳
    public static void ResponseAlive(ResponseAlive result)
    {
        SocketManager.Instance.StartAliveLink();
    }

    //请求创建房间
    public static void RequestCreateRoom(string room_name,string pass_word,bool can_watch,int count)
    {
        RequestCreateRoom req = new RequestCreateRoom()
        {
            Proto = (int)CmdEnum.ReqCreateRoom,
            RoomName = room_name,
            Password = pass_word,
            CanWatch = can_watch,
            PlayerNumber = count,
            PlayerId = GameController.Instance.player.Id
        };
        byte[] data = ProtobufTool.CreateData(req.Proto, req);
        Request((CmdEnum)req.Proto, data);
    }

    //返回创建房间
    public static void ResponseCreateRoom(ResponseCreateRoom response)
    {
        //关闭创建界面，然后再次请求房间列表
        RoomUIController.Instance.ClickCancel();
    }

    //请求房间列表
    public static void RequestGetRooms()
    {
        RequestGetRooms req = new RequestGetRooms()
        {
            Proto = (int)CmdEnum.ReqGetRooms
        };
        byte[] data = ProtobufTool.CreateData(req.Proto, req);
        Request((CmdEnum)req.Proto, data);
    }

    //返回房间列表
    public static void ResponseGetRooms(ResponseGetRooms response)
    {
        if (response != null)
        {
            //RoomUIController.Instance.CreateRoomList(response.Rooms);
        }
    }

    //请求删除房间
    public static void RequestDeleteRoom()
    { 
    }

    //返回删除房间
    public static void ResponseDeleteRoom(ResponseDeleteRoom response)
    { 
    }
    #endregion
}
