using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; //정적 메모리에 담기 위한 instance 변수 선언

    [Header("#BGM")] //시작 음악
    public AudioClip[] bgmClip; //배경음과 관련된 클립

    public float bgmVolume; //배경음과 관련된 볼륨     
    AudioSource bgmPlayer; //배경음과 관련된 오디오 소스


    [Header("#SFX")] //효과음
    public AudioClip[] sfxClips; //효과음과 관련된 클립

    public float sfxVolume; //효과음과 관련된 클립
    public int channels; //다양한 효과음을 낼 수 있는 채널 개수 변수
    AudioSource[] sfxPlayers; //효과음과 관련된 클립
    int channelIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; //instance 초기화
            Init(); //초기화 함수???
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Init()
    {
        //배경음 플레이어 초기화
        GameObject bgmobject = new GameObject("BgmPlayer");
        bgmobject.transform.parent = transform; //배경음을 담당하는 자식 오브젝트
        bgmPlayer = bgmobject.AddComponent<AudioSource>(); //AddComponent함수로 오디오 소스 생성 후 변수에 저장
        bgmPlayer.playOnAwake = false; //캐릭터 누를때 배경음악 활성화
        bgmPlayer.loop = true; //배경음악 루프
        bgmPlayer.volume = bgmVolume; //볼륨


        //효과음 플레이어 초기화
        GameObject sfxobject = new GameObject("sfxPlayer");
        sfxobject.transform.parent = transform;
        sfxPlayers = new AudioSource[channels]; //채널 값을 통해 오디오 소스 배열 초기화, 실제 내용물 초기화 X

        for (int index = 0; index < sfxPlayers.Length; index++) //모든 효과음 오디오 소스 생성하면서 저장
        {
            sfxPlayers[index] = sfxobject.AddComponent<AudioSource>();
            sfxPlayers[index].playOnAwake = false;
            sfxPlayers[index].volume = sfxVolume;
        }

    }

    private void Start()
    {
        PlayBgm(Bgm.StartBgm, true);
    }

    public void PlayBgm(Bgm bgm, bool isplay) //배경음 재생 함수
    {
        if (isplay) //상황에 따른 배경음 교체
        {
            //재생중인 오디오 스탑
            bgmPlayer.Stop();
            //오디오 클립 교체
            bgmPlayer.clip = bgmClip[(int)bgm];
            //다시 오디오 재생
            bgmPlayer.Play();
        }
        else
        {
            bgmPlayer.Stop();
        }
    }

    public void PlaySfx(Sfx sfx) //효과음 재생 함수
    {
        for (int index = 1; index < sfxPlayers.Length; index++)
        {
            int loopIndex = (index + channelIndex) % sfxPlayers.Length;

            if (sfxPlayers[loopIndex].isPlaying)
                continue;

            channelIndex = loopIndex;
            sfxPlayers[loopIndex].clip = sfxClips[(int)sfx];
            sfxPlayers[loopIndex].Play();
            break;

        }
    }

    public void PlayZapperSfx(bool isTrue)
    {
        if (isTrue)
        {
            sfxPlayers[0].loop = true;
            sfxPlayers[0].clip = sfxClips[(int)Sfx.Zapper];
            sfxPlayers[0].Play();
        }
        else
        {
            sfxPlayers[0].loop = false;
            sfxPlayers[0].Stop();
        }
    }

}
