using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [SerializeField]
    private AudioSource bgmSource;
    private Dictionary<string, AudioClip> bgmDic, seDic;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        //リソースフォルダから全SE&BGMのファイルを読み込みセット
        seDic = new Dictionary<string, AudioClip>();
        bgmDic = new Dictionary<string, AudioClip>();

        object[] seList = Resources.LoadAll("Audio/SE");
        object[] bgmList = Resources.LoadAll("Audio/BGM");

        foreach (AudioClip se in seList)
        {
            seDic[se.name] = se;
        }
        foreach (AudioClip bgm in bgmList)
        {
            bgmDic[bgm.name] = bgm;
        }
    }

    public void PlayBGM(string fileName, float volume = 1f, float delay = 0f)
    {
        if (!bgmDic.ContainsKey(fileName))
        {
            Debug.Log(fileName + "という名前のBGMがありません");
            return;
        }

        volume = Mathf.Clamp(volume, 0f, 1f);
        bgmSource.volume = volume;


        bgmSource.clip = bgmDic[fileName];
        bgmSource.PlayDelayed(delay);
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public void PlaySE(string fileName, AudioSource seSource, float volume = 1f)
    {
        if (!seDic.ContainsKey(fileName))
        {
            Debug.Log(fileName + "という名前のSEがありません");
            return;
        }

        volume = Mathf.Clamp(volume, 0f, 1f);
        seSource.volume = volume;

        seSource.PlayOneShot(seDic[fileName]);
    }
}
