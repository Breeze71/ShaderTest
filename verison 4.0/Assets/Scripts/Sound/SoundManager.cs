using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*SoundManager.instance.JumpAudio();
  SoundManager.instance.HurtAudio();
  SoundManager.instance.DeadAudio();
  直接粘贴调用*/

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    //对SoundManger类实例化一个对象instance
    public AudioSource audioSource;
    //临时存放不同音效的变量
    [SerializeField] private AudioClip jumpAudio , runAudio , deadAudio , shootAudio;
    //存放跳跃音效，受攻击音效，死亡音效

    private void Awake()
    {
        instance = this;
        //对实例化对象赋值
    }
    
    public void JumpAudio()
    {
        audioSource.clip = jumpAudio;
        //将audioSource的音乐资源设置为jumpAudio
        audioSource.Play();
        //然后播放音乐资源
    }

    public void RunAudio()
    {
        audioSource.clip = runAudio;
        //将audioSource的音乐资源设置为runAudio
        audioSource.Play();
        //然后播放音乐资源
    }
 
    public void DeadAudio()
    {
        audioSource.clip = deadAudio;
        //将audioSource的音乐资源设置为deadAudio
        audioSource.Play();
        //然后播放音乐资源
    }

    public void ShootAudio()
    {
        audioSource.clip = shootAudio;
        //将audioSource的音乐资源设置为shootAudio
        audioSource.Play();
        //然后播放音乐资源
    }
}
