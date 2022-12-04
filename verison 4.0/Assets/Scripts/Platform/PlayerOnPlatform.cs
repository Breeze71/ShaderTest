using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    private Collider2D cd;
    private bool _playerOnPlatform;

    private void Start(){

        cd = GetComponent<Collider2D>();

    }

    ////
    private void Update(){

        if(_playerOnPlatform && Input.GetAxisRaw("Vertical") < 0){
            // 如果踩在平台上又按下，則取消平台 Collider且開始協程(角色無法在一偵內掉下去
            cd.enabled = false;
            StartCoroutine(EnableCollider());

        }
    }

    //// 0.5 秒後恢復判定
    private IEnumerator EnableCollider(){
         
        yield return new WaitForSeconds(0.5f);
        cd.enabled = true;
    }

    ////                            // 碰到的平台 , 碰到的是玩家
    private void SetPlayerOnPlatform(Collision2D other, bool value){
        
        // 判斷當前的 scripts (是否為 player)
        var player = other.gameObject.GetComponent<Movement>();

        if (player != null){

            _playerOnPlatform = value;
        }
    }

    //// 進入判定範圍
    private void OnColisioEnter2D(Collision2D other){

        SetPlayerOnPlatform(other , true);
       
    }

    //// 離開期間也要持續 T ，不然下不去
    private void OnCollisionExit2D(Collision2D other){
        
        SetPlayerOnPlatform(other , true);
      
    }

}


