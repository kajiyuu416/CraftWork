using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] AudioClip HitSe;
    [SerializeField] AudioClip SynthesizeSE;
    [SerializeField] AudioClip AcquisitionSE;
    [SerializeField] AudioClip PutonSE;
    [SerializeField] AudioClip ThrowSE;
    [SerializeField] AudioClip Unlocking1SE;
    [SerializeField] AudioClip Unlocking2SE;
    [SerializeField] AudioClip CorrectSE;
    [SerializeField] AudioClip SwitchSE;
    [SerializeField] AudioClip TeleporSE;
    [SerializeField] AudioClip CrumblingSE;

    AudioSource bgm1AudioSource;
    AudioSource SelectSeAudioSource;

    GameObject bgmObj;
    GameObject SeObj;

    public static SoundManager Instance
    {
        get; private set;
    }

    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        bgmObj = transform.GetChild(0).gameObject;
        SeObj = transform.GetChild(1).gameObject;
        bgm1AudioSource = bgmObj.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        SelectSeAudioSource = SeObj.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        Startbgm1();
    }
    public void StopBGM()
    {
        bgm1AudioSource.Stop();
    }
    // ã»ÇÃàÍéûí‚é~
    public void SoundPause()
    {
        bgm1AudioSource.Pause();
    }

    // ã»ÇÃçƒäJ
    public void SoundUnPause()
    {
        bgm1AudioSource.UnPause();
    }
    public void StopSE()
    {
        SelectSeAudioSource.Stop();
    }
    public void Startbgm1()
    {
        bgm1AudioSource.Play();
    }
    public void SettingPlaySE()
    {
        SelectSeAudioSource.PlayOneShot(HitSe);
    }
    public void SettingPlaySE2()
    {
        SelectSeAudioSource.PlayOneShot(SynthesizeSE);
    }
    public void SettingPlaySE3()
    {
        SelectSeAudioSource.PlayOneShot(AcquisitionSE);
    }
    public void SettingPlaySE4()
    {
        SelectSeAudioSource.PlayOneShot(PutonSE);
    }
    public void SettingPlaySE5()
    {
        SelectSeAudioSource.PlayOneShot(ThrowSE);
    }
    public void SettingPlaySE6()
    {
        SelectSeAudioSource.PlayOneShot(Unlocking1SE);
    }
    public void SettingPlaySE7()
    {
        SelectSeAudioSource.PlayOneShot(CorrectSE);
    }
    public void SettingPlaySE8()
    {
        SelectSeAudioSource.PlayOneShot(SwitchSE);
    }
    public void SettingPlaySE9()
    {
        SelectSeAudioSource.PlayOneShot(Unlocking2SE);
    }
    public void SettingPlaySE10()
    {
        SelectSeAudioSource.PlayOneShot(TeleporSE);
    }
    public void SettingPlaySE11()
    {
        SelectSeAudioSource.PlayOneShot(CrumblingSE);
    }
    
}
