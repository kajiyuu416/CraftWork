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
    [SerializeField] AudioClip SelectSE;
    [SerializeField] AudioClip UISelectSE;
    [SerializeField] AudioClip Ignite1SE;
    [SerializeField] AudioClip Ignite2SE;
    [SerializeField] AudioClip extinguish_the_fireSE;
    [SerializeField] AudioClip arrowSE;
    [SerializeField] AudioClip BomSE;
    [SerializeField] AudioClip SummonSE;
    [SerializeField] AudioClip GravitySE;
    [SerializeField] AudioClip crushingSE;
    [SerializeField] AudioClip GameClearSE;
    [SerializeField] AudioClip LegacyItemGetSE;
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider BgmSlinder;
    [SerializeField] Slider SeSlinder;

    AudioSource bgm1AudioSource;
    AudioSource bgm2AudioSource;
    AudioSource bgm3AudioSource;
    AudioSource bgm4AudioSource;
    AudioSource bgm5AudioSource;
    AudioSource SelectSeAudioSource;

    GameObject bgmObj;
    GameObject SeObj;

    public static SoundManager Instance
    {
        get; private set;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        bgmObj = transform.GetChild(0).gameObject;
        SeObj = transform.GetChild(1).gameObject;
        bgm1AudioSource = bgmObj.transform.GetChild(0).gameObject.GetComponent<AudioSource>();
        bgm2AudioSource = bgmObj.transform.GetChild(1).gameObject.GetComponent<AudioSource>();
        bgm3AudioSource = bgmObj.transform.GetChild(2).gameObject.GetComponent<AudioSource>();
        bgm4AudioSource = bgmObj.transform.GetChild(3).gameObject.GetComponent<AudioSource>();
        bgm5AudioSource = bgmObj.transform.GetChild(4).gameObject.GetComponent<AudioSource>();
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
        bgm3AudioSource.Stop();
        bgm4AudioSource.Stop();
        bgm5AudioSource.Stop();
    }
    public void SoundPause()
    {
        bgm1AudioSource.Pause(); 
        bgm2AudioSource.Pause();
        bgm3AudioSource.Pause();
        bgm4AudioSource.Pause();
        bgm5AudioSource.Pause();

    }
    public void SoundUnPause()
    {
        bgm1AudioSource.UnPause();
        bgm2AudioSource.UnPause();
        bgm3AudioSource.UnPause();
        bgm4AudioSource.UnPause();
        bgm5AudioSource.UnPause();
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
    public void Startbgm3()
    {
        bgm3AudioSource.Play();
    }
    public void Startbgm4()
    {
        bgm4AudioSource.Play();
    }
    public void Startbgm5()
    {
        bgm5AudioSource.Play();
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
    public void SettingPlaySE12()
    {
        SelectSeAudioSource.PlayOneShot(SelectSE);
    }
    public void SettingPlaySE13()
    {
        SelectSeAudioSource.PlayOneShot(UISelectSE);
    }
    public void SettingPlaySE14()
    {
        SelectSeAudioSource.PlayOneShot(Ignite1SE);
    }
    public void SettingPlaySE15()
    {
        SelectSeAudioSource.PlayOneShot(extinguish_the_fireSE);
    }
    public void SettingPlaySE16()
    {
        SelectSeAudioSource.PlayOneShot(arrowSE);
    }
    public void SettingPlaySE17()
    {
        SelectSeAudioSource.PlayOneShot(Ignite2SE);
    }
    public void SettingPlaySE18()
    {
        SelectSeAudioSource.PlayOneShot(BomSE);
    }    
    public void SettingPlaySE19()
    {
        SelectSeAudioSource.PlayOneShot(SummonSE);
    }   
    public void SettingPlaySE20()
    {
        SelectSeAudioSource.PlayOneShot(GravitySE);
    }   
    public void SettingPlaySE21()
    {
        SelectSeAudioSource.PlayOneShot(crushingSE);
    }
    public void SettingPlaySE22()
    {
        SelectSeAudioSource.PlayOneShot(GameClearSE);
    }    public void SettingPlaySE23()
    {
        SelectSeAudioSource.PlayOneShot(LegacyItemGetSE);
    }
}
