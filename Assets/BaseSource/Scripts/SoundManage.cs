using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class SoundManage : MonoBehaviour
{
    public static SoundManage Instance;
    public const string SOUNDKEY = "Sound";
    public const string MUSICKEY = "Music";
    public const string VIBRATEKEY = "Vibrate";
    bool _soundBool;
    bool _vibrateBool;
    bool _musicBool;
    public bool IsSoundOn { get { return _soundBool; } }
    public bool IsVibrateOn { get { return _vibrateBool; } }
    public bool IsMusicOn { get { return _musicBool; } }
    [SerializeField]
    AudioSource sound_audioSource;
    [SerializeField]
    AudioSource music_audioSource;
    [SerializeField]
    AudioSource laser_audioSource;
    [SerializeField]
    AudioClip boopClip;
    [SerializeField]
    AudioClip[] boopAudioType1;



    [SerializeField]
    AudioClip[] hurtPlayer;
    [SerializeField]
    AudioClip[] hurtEnemy;
    [SerializeField]
    AudioClip[] bgHomes;
    [SerializeField]
    AudioClip[] bgGames;
    [SerializeField]
    AudioClip danceBG;
    [SerializeField]
    AudioClip buttonclick;
    [SerializeField]
    AudioClip buttonclickOpen;
    [SerializeField]
    AudioClip buttonclickClose;
    [SerializeField]
    AudioClip playClick;
    [SerializeField]
    AudioClip fireworkClip;
    [SerializeField]
    AudioClip coinPickUpClip;
    [SerializeField]
    AudioClip coinGainClip;
    [SerializeField]
    AudioClip upgradeClip;
    [SerializeField]
    AudioClip clickPluginClip;
    [SerializeField]
    AudioClip dropCompleteClip;
    [SerializeField]
    float maxSoundVolume = 0.7f;
    [SerializeField]
    float maxMusicVolume = 0.2f;
    [SerializeField]
    private AudioClip rotationClip;
    [SerializeField]
    private AudioClip levelCompleteClip;
    [SerializeField]
    private AudioClip levelFailClip;
    [SerializeField]
    private AudioClip levelFailMusicClip;
    [SerializeField]
    private AudioClip getHintClip;
    [SerializeField]
    private AudioClip happyClip;
    [SerializeField]
    AudioClip[] listFootStep;
    [SerializeField]
    AudioClip jumpClip;
    [SerializeField]
    AudioClip unlockSkinClip;
    [SerializeField]
    AudioClip buyPackClip;
    [SerializeField]
    AudioClip[] yeahClip;
    [SerializeField]
    AudioClip exploreClip;
    [SerializeField]
    AudioClip rocketClip;
    [SerializeField]
    AudioClip doorEndOpenClip;
    [SerializeField]
    AudioClip doorOpenClip;
    [SerializeField]
    AudioClip medicineEatClip;
    [SerializeField]
    AudioClip gunReloadClip;
    float delayCoinPickUp = 0.05f;
    float delayCoinPickUpTimer = 0f;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        FetchData();
        sound_audioSource.volume = maxSoundVolume;
        Play_HomeMusic();
        canYeah = true;
    }
    public static void FirstInit()
    {
        PlayerPrefs.SetInt(SOUNDKEY, 1);
        PlayerPrefs.SetInt(MUSICKEY, 1);
        PlayerPrefs.SetInt(VIBRATEKEY, 1);
    }

    void FetchData()
    {
        _soundBool = PlayerPrefs.GetInt(SOUNDKEY, 1) == 1;
        _vibrateBool = PlayerPrefs.GetInt(VIBRATEKEY, 1) == 1;
        _musicBool = PlayerPrefs.GetInt(MUSICKEY, 1) == 1;

    }
    bool isLaserPlaying;
    internal void CheckLaser()
    {
        if (isLaserPlaying)
        {
            if (laser_audioSource.isPlaying)
            {
                laser_audioSource.Stop();
            }
            else
            {
                if (!_soundBool) return;
                laser_audioSource.Play();
            }
        }
        if (bFireWork)
        {
            bFireWork = false;
        }
    }

    private void Update()
    {
        if (delayCoinPickUpTimer > 0)
        {
            delayCoinPickUp -= Time.deltaTime;
        }
    }
    [SerializeField]
    float timeLifeFirework = 3f;
    bool bFireWork;
    float timerFirework = 0f;
    private void LateUpdate()
    {
        if (timerFirework > 0)
        {
            timerFirework -= Time.deltaTime;
        }
        else
        {
            if (bFireWork)
            {
                timerFirework = timeLifeFirework;
                Play_Firework();
            }
        }
    }
    bool tempSoundBool;
    bool tempMusicBool;
    public void SetMusicAndSoundAds(bool on)
    {
        if (!on)
        {
            tempMusicBool = _musicBool;
            tempSoundBool = _soundBool;
            SetSoundActive(false);
            SetMusicActive(false);
        }
        else
        {
            SetSoundActive(tempSoundBool);
            SetMusicActive(tempMusicBool);
        }
    }
    public void SetSoundActive(bool set)
    {
        if (set)
        {
            PlayerPrefs.SetInt(SOUNDKEY, 1);
            _soundBool = true;
        }
        else
        {
            PlayerPrefs.SetInt(SOUNDKEY, 0);
            _soundBool = false;
            laser_audioSource.Stop();
        }
    }
    public void SetMusicActive(bool set)
    {
        if (set)
        {
            PlayerPrefs.SetInt(MUSICKEY, 1);
            _musicBool = true;
            Play_HomeMusic();
        }
        else
        {
            PlayerPrefs.SetInt(MUSICKEY, 0);
            _musicBool = false;
            StopMusic();
        }
    }

    public void SetVibrateActive(bool set)
    {
        if (set)
        {
            PlayerPrefs.SetInt(VIBRATEKEY, 1);
            _vibrateBool = true;
        }
        else
        {
            PlayerPrefs.SetInt(VIBRATEKEY, 0);
            _vibrateBool = false;
        }
    }
    public void DoVibrate()
    {
        if (!_vibrateBool) return;
        Handheld.Vibrate();
    }
    public void Play_RotationSound()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(rotationClip);
    }
    public void Play_LevelComplete()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(levelCompleteClip);
    }
    internal void Play_Explore()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(exploreClip);
    }
    internal void Play_Rocket()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(rocketClip);
    }
    public void Play_LevelFail()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(levelFailClip);
    }
    public void PlayBoop()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(boopClip);
    }
    public void PlayMedicineEat()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(medicineEatClip);
    }
    public void Play_ButtonClick()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(buttonclick);

    }
    bool canYeah = false;
    public void TurnOnYeah()
    {
        canYeah = true;
    }

    public void PLay_Laser()
    {
        isLaserPlaying = true;
        if (!_soundBool) return;
        laser_audioSource.Play();
    }
    public void Stop_Laser()
    {
        laser_audioSource.Stop();
        isLaserPlaying = false;
    }
    public void Play_Yeah()
    {
        if (!_soundBool || !canYeah) return;
        canYeah = false;
        int rand = Random.Range(0, yeahClip.Length);
        sound_audioSource.PlayOneShot(yeahClip[rand]);

    }
    internal void Play_DropComplete()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(dropCompleteClip);
    }
    internal void Play_HappySound()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(happyClip);
    }


    public void Play_CoinGain()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(coinGainClip);

    }
    public void Play_CoinPickUp()
    {
        if (!_soundBool || delayCoinPickUpTimer > 0) return;
        delayCoinPickUpTimer = delayCoinPickUp;
        sound_audioSource.PlayOneShot(coinPickUpClip);

    }

    internal void Play_GainHint()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(getHintClip);
    }
    internal void Play_UnlockSkin()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(unlockSkinClip);
    }
    internal void Play_BuyPack()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(buyPackClip);
    }

    public void Play_ClickOpen()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(buttonclickClose);

    }

    internal void Play_PluginClick()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(clickPluginClip);
    }
    internal void Play_DoorOpen()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(doorOpenClip);
    }
    internal void Play_DoorEndOpen()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(doorEndOpenClip);
    }
    public void Play_ClickClose()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(buttonclickClose);

    }
    public void Play_Upgrade()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(upgradeClip);

    }
    public void Play_PlayButtonClick()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(playClick);
    }
    public void Play_FootStep()
    {
        if (!_soundBool) return;
        int rand = Random.Range(0, listFootStep.Length);
        sound_audioSource.PlayOneShot(listFootStep[rand]);

    }
    public void Play_HurtPlayer()
    {
        if (!_soundBool) return;
        int rand = Random.Range(0, hurtPlayer.Length);
        sound_audioSource.PlayOneShot(hurtPlayer[rand]);
    }
    public void Play_HurtEnemy()
    {
        if (!_soundBool) return;
        int rand = Random.Range(0, hurtEnemy.Length);
        sound_audioSource.PlayOneShot(hurtEnemy[rand]);
    }
    public void Play_Jump()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(jumpClip);
    }
    public void StartFirework()
    {
        bFireWork = true;
    }
    public void StopFireWork()
    {
        bFireWork = false;
    }
    public void Play_Firework()
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(fireworkClip);
    }

    public void PlayDanceMusic()
    {
        if (!_musicBool) return;
        music_audioSource.Stop();
        music_audioSource.loop = true;
        OnChangeMusic(0.5f, danceBG);
    }
    public void Play_HomeMusic()
    {
        if (!_musicBool) return;
        if (bgHomes.Length > 1)
        {
            music_audioSource.Stop();
            int rand = Random.Range(0, bgHomes.Length);
            music_audioSource.loop = true;
            OnChangeMusic(0.5f, bgHomes[rand]);
        }
        else if (bgHomes.Length == 1)
        {
            music_audioSource.Stop();
            music_audioSource.loop = true;
            OnChangeMusic(0.5f, bgHomes[0]);
        }
    }
    public void Play_GameMusic()
    {
        if (!_musicBool) return;
        if (bgGames.Length > 1)
        {
            int rand = Random.Range(0, bgGames.Length);
            music_audioSource.Stop();
            music_audioSource.loop = true;
            OnChangeMusic(0.5f, bgGames[rand]);
        }
        else if (bgGames.Length == 1)
        {
            music_audioSource.Stop();
            music_audioSource.loop = true;
            OnChangeMusic(0.5f, bgGames[0]);
        }
    }
    public void Play_LoseGameMusic()
    {
        if (!_musicBool) return;
        OnChangeMusic(0.5f, levelFailMusicClip);
    }

    private void OnChangeMusic(float time, AudioClip clip)
    {
        StopAllCoroutines();
        StartCoroutine(OnChangeMusicIE(time, clip));
    }
    private IEnumerator OnChangeMusicIE(float time, AudioClip clip)
    {
        float tmp = music_audioSource.volume;
        float t = 0;
        while (t < time / 2)
        {
            t += Time.deltaTime;
            tmp = Mathf.Lerp(tmp, 0, t / (time / 2));
            music_audioSource.volume = tmp;
            yield return new WaitForEndOfFrame();
        }
        music_audioSource.Stop();
        music_audioSource.clip = clip;
        music_audioSource.Play();
        while (t < time)
        {
            t += Time.deltaTime;
            tmp = Mathf.Lerp(tmp, maxMusicVolume, t / (time));
            music_audioSource.volume = tmp;
            yield return new WaitForEndOfFrame();
        }
    }
    public void PlaySound(AudioClip clip)
    {
        if (!_soundBool) return;
        sound_audioSource.PlayOneShot(clip);
    }

    public void StopMusic()
    {
        music_audioSource.Stop();
    }
}
