using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class SoundManager : MonoBehaviour
{
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
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BgmSlinder;
    [SerializeField] Slider SeSlinder;

    AudioSource bgm1AudioSource;
    AudioSource bgm2AudioSource;
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
        bgm2AudioSource = bgmObj.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        SelectSeAudioSource = SeObj.transform.GetChild(0).gameObject.GetComponent<AudioSource>();

        SetBGMVolume(BgmSlinder.value);
        SetSEVolume(SeSlinder.value);
        BgmSlinder.onValueChanged.AddListener(SetBGMVolume);
        SeSlinder.onValueChanged.AddListener(SetSEVolume);
        Startbgm2();
    }
    public void SetBGMVolume(float volume)
    {
        audioMixer.SetFloat("BGM", Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f));
    }
    public void SetSEVolume(float volume)
    {
        audioMixer.SetFloat("SE", Mathf.Clamp(Mathf.Log10(volume) * 20f, -80f, 0f));
    }
    public void StopBGM()
    {
        bgm1AudioSource.Stop();
        bgm2AudioSource.Stop();
    }
    // ã»ÇÃàÍéûí‚é~
    public void SoundPause()
    {
        bgm1AudioSource.Pause(); 
        bgm2AudioSource.Pause();
    }

    // ã»ÇÃçƒäJ
    public void SoundUnPause()
    {
        bgm1AudioSource.UnPause();
        bgm2AudioSource.UnPause();
    }
    public void StopSE()
    {
        SelectSeAudioSource.Stop();
    }
    public void Startbgm1()
    {
        bgm1AudioSource.Play();
    }
    public void Startbgm2()
    {
        bgm2AudioSource.Play();
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
