using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


// BGM & SE For AudioManager
public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    //Save key BGM & SE
    private const string BGM_Volume_Key = "BGM_VOLUME_KEY";
    private const string SE_Volume_Key = "SE_VOLUME_KEY";
    private float BGM_Volume_Default = 0.51f;
    private float SE_Volume_Default = 1.0f;

    //BGM fade 
    public const float BGM_Fade_Speed_Rate_High = 0.9f;
    public const float BGM_Fade_Speed_Rate_Low = 0.3f;
    private float _bgmFadeSpeedRate = BGM_Fade_Speed_Rate_High;

    //Next clip for BGM or SE
    private string _nextBGMName;
    private string _nextSEName;

    //Whether audio is fading out
    private bool _isFadeOut = false;
   
    //BGM and SE get component from audio source
    [SerializeField] public AudioSource AttachBGMSource, AttachSESource;

    //Using dictionary for database BGM and SE
    private Dictionary<string, AudioClip> _bgmDic, _seDic;

    //=================================================================================
    //FUNCTION AUDIO MANAGER
    //=================================================================================

    private void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //Make a resource file to read 
        _bgmDic = new Dictionary<string, AudioClip>();
        _seDic = new Dictionary<string, AudioClip>();

        object[] bgmList = Resources.LoadAll("Audio/BGM");
        object[] seList = Resources.LoadAll("Audio/SE");

        foreach (AudioClip bgm in bgmList)
        {
            _bgmDic[bgm.name] = bgm;
        }
        foreach (AudioClip se in seList)
        {
            _seDic[se.name] = se;
        }
    }

    private void Start()
    {
        AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_Volume_Key, BGMSource);
        AttachSESource.volume = PlayerPrefs.GetFloat(SE_Volume_Key, SESource);
    }

    //=================================================================================
    //SE
    //=================================================================================

    public void PlaySE(string seName, float delay = 0.0f)
    {
        if (!_seDic.ContainsKey(seName))
        {
            Debug.Log(seName + "can't found that SE song");
            return;
        }

        _nextSEName = seName;
        Invoke("DelayPlaySE", delay);
    }

    private void DelayPlaySE()
    {
        AttachSESource.PlayOneShot(_seDic[_nextSEName] as AudioClip);
    }

    //=================================================================================
    //BGM
    //=================================================================================

    public void PlayBGM(string bgmName, float fadeSpeedRate = BGM_Fade_Speed_Rate_High)
    {
        if (!_bgmDic.ContainsKey(bgmName))
        {
            Debug.Log(bgmName + "can't found that BGM song");
            return;
        }

        //isplaying
        if (!AttachBGMSource.isPlaying)
        {
            _nextBGMName = "";
            AttachBGMSource.clip = _bgmDic[bgmName] as AudioClip;
            AttachBGMSource.Play();
        }
        //BGM song is false
        else if (AttachBGMSource.clip.name != bgmName)
        {
            _nextBGMName = bgmName;
            FadeOutBGM(fadeSpeedRate);
        }

    }


    //=================================================================================
    //FADE OUT
    //=================================================================================

    public void FadeOutBGM(float fadeSpeedRate = BGM_Fade_Speed_Rate_Low)
    {
        _bgmFadeSpeedRate = fadeSpeedRate;
        _isFadeOut = true;
    }

    private void Update()
    {
        if (!_isFadeOut)
        {
            return;
        }

        //if volume BGM is going down
        AttachBGMSource.volume -= Time.deltaTime * _bgmFadeSpeedRate;
        if (AttachBGMSource.volume <= 0)
        {
            AttachBGMSource.Stop();
            AttachBGMSource.volume = PlayerPrefs.GetFloat(BGM_Volume_Key, BGM_Volume_Default);
            _isFadeOut = false;

            if (!string.IsNullOrEmpty(_nextBGMName))
            {
                PlayBGM(_nextBGMName);
            }
        }

    }

    //=================================================================================
    //Change VOLUME 
    //=================================================================================

    public void ChangeVolume(float BGMVolume, float SEVolume)
    {
        AttachBGMSource.volume = BGMVolume;
        AttachSESource.volume = SEVolume;

        PlayerPrefs.SetFloat(BGM_Volume_Key, BGMVolume);
        PlayerPrefs.SetFloat(SE_Volume_Key, SEVolume);
    }

    public void StopSE()
    {
        AttachSESource.Stop();
    }

    public AudioSource GetAttachSESource
    {
        get
        {
            return GetAttachSESource;
        }
    }

    public float BGMSource
    {
        get
        {
            return PlayerPrefs.GetFloat(BGM_Volume_Key);
        }
        set
        {
            BGM_Volume_Default=value;
            PlayerPrefs.SetFloat(BGM_Volume_Key, value);
        }
    }
    
    public float SESource
    {
        get
        {
            return PlayerPrefs.GetFloat(SE_Volume_Key);
        }
        set
        {
            SE_Volume_Default = value;
            PlayerPrefs.SetFloat(SE_Volume_Key, value);
        }
    }
}