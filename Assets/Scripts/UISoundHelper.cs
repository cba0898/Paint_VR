using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundHelper : MonoBehaviour
{
    private void Start()
    {
        SoundMgr.Instance.LoadAudio();
    }
    public void PlaySFX(string clipName)
    {
        if(SoundMgr.Instance) SoundMgr.Instance.OnPlaySFX(clipName);
    }
}
