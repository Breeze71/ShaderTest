using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class StartScene : MonoBehaviourPunCallbacks
{
    public void OnclickStart()
    {
        PhotonNetwork.ConnectUsingSetting();    // 連到帳號的伺服器 id
    
    }

    public override void OnConnectedToMaster()
    {
        print("Connected!");
    }
}
