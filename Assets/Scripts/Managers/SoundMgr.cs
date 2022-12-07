using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SoundMgr : MonoBehaviour
{
    #region instance
    private static SoundMgr instance = null;
    public static SoundMgr Instance { get { return instance; } }

    private void Awake()
    {
        // Scene�� �̹� �ν��Ͻ��� ���� �ϴ��� Ȯ�� �� ó��
        if (instance)
        {
            Destroy(this.gameObject);
            return;
        }
        // instance�� ���� ������Ʈ�� �����
        instance = this;

        // Scene �̵� �� ���� ���� �ʵ��� ó��
        DontDestroyOnLoad(this.gameObject);
        LoadAudio();
    }
    #endregion

    private Dictionary<string, AudioClip> BGMDictionary;   // ���ҽ��� �ҷ��� ����� Ŭ���� ������ ��ųʸ�
    private Dictionary<string, AudioClip> SFXDictionary;   // ���ҽ��� �ҷ��� ����� Ŭ���� ������ ��ųʸ�
    [SerializeField] private AudioSource BGM;    // BGM
    [SerializeField] private AudioSource SFX;    // SFX

    // bgm key ���ڿ� ������
    public string keyMain = "On the Farm";
    //public string keyCardSet = "CardSet";
    //public string keyBattle = "Battle";
    //public string keyWin = "Win";
    //public string keyLose = "Lose";
    //public string keyEnding = "Ending";

    private bool masterMute = false;
    private float masterVolume = 1;

    private bool SFXMute = false;
    private float SFXVolume = 1;

    private bool BGMMute = false;
    private float BGMVolume = 1;

    public bool SFXSoundMute { get { return (masterMute || SFXMute); } }
    public float SFXSoundVolume { get { return (SFXVolume * masterVolume); } }

    public bool BGMSoundMute { get { return (masterMute || BGMMute); } }
    public float BGMSoundVolume { get { return (BGMVolume * masterVolume); } }

    public void ChangeMasterVolume(Slider slider)
    {
        masterVolume = slider.value;
        ApplyBGMVolume();
        ApplySFXVolume();
    }
    public void MuteMasterVolume(Toggle toggle)
    {
        masterMute = toggle.isOn;
        ApplyBGMVolume();
        ApplySFXVolume();
    }
    public void ChangeSFXVolume(Slider slider)
    {
        SFXVolume = slider.value;
        ApplySFXVolume();
    }
    public void MuteSFXVolume(Toggle toggle)
    {
        SFXMute = toggle.isOn;
        ApplySFXVolume();
    }
    public void ChangeBGMVolume(Slider slider)
    {
        BGMVolume = slider.value;
        ApplyBGMVolume();
    }
    public void MuteBGMVolume(Toggle toggle)
    {
        BGMMute = toggle.isOn;
        ApplyBGMVolume();
    }

    public void LoadAudio()
    {
        BGMDictionary = DataController.Instance.SetDictionary<AudioClip>("Sounds/BGM");
        SFXDictionary = DataController.Instance.SetDictionary<AudioClip>("Sounds/SFX");
        BGM.loop = true;
        SFX.loop = false;
    }

    public void ApplyBGMVolume()
    {
        BGM.volume = BGMSoundVolume;
        BGM.mute = BGMSoundMute;
    }
    public void ApplySFXVolume()
    {
        SFX.volume = SFXSoundVolume;
        SFX.mute = SFXSoundMute;
    }

    public void OnPlayBGM(string key)
    {
        //���� ���� ����
        BGM.Stop();
        // �÷��� ���̶�� ����
        if (BGM.isPlaying) return;
        BGM.clip = BGMDictionary[key];
        // ������� ���� ��쿡�� ���
        if (!BGM.isPlaying) BGM.Play();
    }

    public void StopBGM()
    {
        BGM.Stop();
    }

    public void OnDefaultButtonSFX()
    {
        SFX.clip = SFXDictionary["1.Click"];
    }

    public void OnPlaySFX(string clipName)
    {
        //if (isFull) return;
        if (SFX) SFX.Stop();
        if (SFX.isPlaying) return;
        SFX.clip = SFXDictionary[clipName];
        if (!SFX.isPlaying) SFX.Play();
    }
    private bool isFull = false;
    public void FullPlaySFX(string clipName)
    {
        isFull = true;
        if (SFX) SFX.Stop();
        SFX.clip = SFXDictionary[clipName];
        StartCoroutine(FullSFX());
    }
    public IEnumerator FullSFX()
    {
        if (!SFX.isPlaying) SFX.Play();
        while (SFX.isPlaying) yield return null;
        isFull = false;
    }
    public AudioClip GetAudioClip(string clipName)
    {
        return SFXDictionary[clipName];
    }

    public bool IsSFXPlaying()
    {
        if (SFX.isPlaying) return true;
        else return false;
    }

    public void ToMain()
    {
        // ���� ��� ���� ����
        BGM.Stop();
        SFX.Stop();

        // ����ȭ�� BGM ���
        OnPlayBGM(keyMain);
    }
}
