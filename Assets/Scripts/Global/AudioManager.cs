using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //使用单例模式
    public static AudioManager instance;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);//保持单例不随场景改变
        }
        else
        {
            //已有实例则销毁原先的实例
            Destroy(gameObject);
        }
    }

    public AudioSource bgm;

    public List<AudioClip> bgmClips;

    public AudioSource shootEffect;
    public List<AudioClip> shootEffectClips;

    public AudioSource collect;
    public List<AudioClip> collectClips;

    public AudioSource hitEffect;
    public List<AudioClip> hitEffectClips;

    public AudioSource propEffect;
    public List<AudioClip> propEffectClips;
    
    public AudioSource deathEffect;
    public List<AudioClip> deathEffectClips;
    //激活道具使用音效
    public void TriggerPropEffect()
    {
        if(propEffect == null) return;
        //道具音效不可循环
        if(propEffect.loop) propEffect.loop = false;
        propEffect.Play();
        Debug.Log("[AudioManager]:Trigger Prop Effect Sound");
    }
    //切换道具使用音效
    //参数：name
    public void ChangePropEffectClip(String name)
    {
        if(propEffect == null) return;
        if(propEffectClips == null) return;
        if(propEffectClips.Count <= 0) return;
        for(int i = 0; i < propEffectClips.Count; i++)
        {
            if(propEffectClips[i].name.Equals(name))
            {
                propEffect.clip = propEffectClips[i];
                Debug.Log("[AudioManager]:Change Prop Effect Sound to " + propEffect.clip.name);
            }
        }
    }
    //参数：int
    public void ChangePropEffectClip(int index)
    {
        if(propEffect == null) return;
        if(propEffectClips == null) return;
        if(propEffectClips.Count <= 0) return;
        if(index >= propEffectClips.Count) index = propEffectClips.Count - 1;
        else if(index < 0) index = 0;
        propEffect.clip = propEffectClips[index];
        Debug.Log("[AudioManager]:Change Prop Effect Sound to " + propEffectClips[index].name);
    }

    //激活命中音效
    public void TriggerHitEffect()
    {
        if(hitEffect == null) return;
        //命中音效不可循环
        if(hitEffect.loop) hitEffect.loop = false;
        hitEffect.Play();
        Debug.Log("[AudioManager]:Trigger Hit Effect Sound");
    }
    //切换命中音效
    //参数：name
    public void ChangeHitEffectClip(String name)
    {
        if(hitEffect == null) return;
        if(hitEffectClips == null) return;
        if(hitEffectClips.Count <= 0) return;
        for(int i = 0; i < hitEffectClips.Count; i++)
        {
            if(hitEffectClips[i].name.Equals(name))
            {
                hitEffect.clip = hitEffectClips[i];
                Debug.Log("[AudioManager]:Change Hit Effect Sound to " + hitEffect.clip.name);
            }
        }
    }
    //参数：index
    public void ChangeHitEffectClip(int index)
    {
        if(hitEffect == null) return;
        if(hitEffectClips == null) return;
        if(hitEffectClips.Count <= 0) return;
        if(index >= hitEffectClips.Count) index = hitEffectClips.Count - 1;
        else if(index < 0) index = 0;
        hitEffect.clip = hitEffectClips[index];
        Debug.Log("[AudioManager]:Change Hit Effect Sound to " + hitEffectClips[index].name);
    }

    //激活拾取音效
    public void TriggerCollectEffect()
    {
        if(collect == null) return;
        //拾取音效不可循环
        if(collect.loop) collect.loop = false;
        collect.Play();
        Debug.Log("[AudioManager]:Trigger Collect Effect Sound");
    }
    //切换拾取音效
    //参数：name
    public void ChangeCollectClip(String name)
    {
        if(collect == null) return;
        if(collectClips == null) return;
        if(collectClips.Count <= 0) return;
        for(int i = 0; i < collectClips.Count; i++)
        {
            if(collectClips[i].name.Equals(name))
            {
                collect.clip = collectClips[i];
                Debug.Log("[AudioManager]:Change Collect Prop Sound to " + collect.clip.name);
            }
        }
    }
    //切换拾取音效
    //参数：index
    public void ChangeCollectClip(int index)
    {
        if(collect == null) return;
        if(collectClips == null) return;
        if(collectClips.Count <= 0) return;
        if(index >= collectClips.Count) index = collectClips.Count - 1;
        else if(index < 0) index = 0;
        collect.clip = collectClips[index];
        Debug.Log("[AudioManager]:Change Collect Prop Sound to " + collectClips[index].name);   
    }
    
    //播放背景音乐
    public void PlayBgm()
    {
        if(bgm == null) return;
        if(!bgm.isPlaying)
        {
            bgm.Play();
            Debug.Log("[AudioManager]:Play Bgm " + bgm.clip.name);
        }
    }
    //停止背景音乐
    public void StopBgm()
    {
        if(bgm == null) return;
        if(bgm.isPlaying)
        {
            bgm.Stop();
            Debug.Log("[AudioManager]:Bgm Stop");
        }
    }

    //切换背景音乐
    //参数：name
    public void ChangeBgm(String name)
    {
        if(bgm==null) return;
        if(bgmClips == null) return;
        if(bgmClips.Count <= 0) return;
        for(int i = 0; i < bgmClips.Count; i++)
        {
            if(bgmClips[i].name.Equals(name))
            {
                bgm.clip = bgmClips[i];
                Debug.Log("[AudioManager]:Change Bgm to " + bgm.clip.name);
            }
        }
    }
    //参数：index
    public void ChangeBgm(int index)
    {
        if(bgm==null) return;
        if(bgmClips == null) return;
        if(bgmClips.Count <= 0) return;
        if(index >= bgmClips.Count) index = bgmClips.Count - 1;
        else if(index < 0) index = 0;
        bgm.clip = bgmClips[index];
        Debug.Log("[AudioManager]:Change Bgm to " + bgmClips[index].name);
    }

    //激活射击音效
    public void TriggerShootEffect()
    {
        if(shootEffect == null) return;
        //射击音效不可循环
        if(shootEffect.loop) shootEffect.loop = false;
        shootEffect.Play();
        Debug.Log("[AudioManager]:Trigger Shoot Effect Sound");
    }

    //切换射击音效
    //参数：name
    public void ChangeShootEffectClip(String name)
    {
        if(shootEffect == null) return;
        if(shootEffectClips==null) return;
        if(shootEffectClips.Count <= 0) return;
        for(int i = 0; i < shootEffectClips.Count; i++)
        {
            if(shootEffectClips[i].name.Equals(name))
            {
                shootEffect.clip = shootEffectClips[i];
                Debug.Log("[AudioManager]:Change Shoot Effect Clip to " + shootEffect.clip.name);
            }
        }
    }
    //更改射击音效
    //参数：index
    public void ChangeShootEffectClip(int index)
    {
        if(shootEffect == null) return;
        if(shootEffectClips==null) return;
        if(shootEffectClips.Count <= 0) return;
        if(index >= shootEffectClips.Count) index = shootEffectClips.Count - 1;
        else if(index < 0) index = 0;
        shootEffect.clip = shootEffectClips[index];
        Debug.Log("[AudioManager]:Change Shoot Effect Clip to " + shootEffectClips[index].name);
    }
    //激活死亡音效
    public void TriggerDeathEffect()
    {
        //播放死亡音效
        //AudioManager.instance;
        if(deathEffect == null) return;
        if(deathEffect.loop) deathEffect.loop = false; 
        deathEffect.Play();
        Debug.Log("[AudioManager]:Trigger Death Effect Sound");
    }
    //更改死亡音效
    //参数：index
    public void ChangeDeathEffectClip(int index)
    {
        if(deathEffect == null) return;
        if(deathEffectClips==null) return;
        if(deathEffectClips.Count <= 0) return;
        if(index >= deathEffectClips.Count) index = deathEffectClips.Count - 1;
        else if(index < 0) index = 0;
        deathEffect.clip = deathEffectClips[index];
        Debug.Log("[AudioManager]:Change Shoot Effect Clip to " + deathEffectClips[index].name);
    }
    //参数：name
    public void ChangeDeathEffectClip(String name)
    {
        if(deathEffect == null) return;
        if(deathEffectClips==null) return;
        if(deathEffectClips.Count <= 0) return;
        for(int i = 0; i < deathEffectClips.Count; i++)
        {
            if(deathEffectClips[i].name.Equals(name))
            {
                deathEffect.clip = deathEffectClips[i];
                Debug.Log("[AudioManager]:Change Shoot Effect Clip to " + deathEffect.clip.name);
            }
        }
    }

    //随机触发器
    //随机受击音效
    public void TriggerRandomHitEffect(List<String> exceptClips = null)
    {
        if(hitEffect == null) return;
        if(hitEffectClips==null) return;
        if(hitEffectClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        foreach(AudioClip clip in hitEffectClips)
        {
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        hitEffect.clip = realClips[index];
        if(hitEffect.loop) hitEffect.loop = false;
        hitEffect.Play();
        Debug.Log("[AudioManager]:Trigger Random Hit Effect " + hitEffect.clip.name);
    }
    //随机切换射击音效
    public void TriggerRandomShootEffect(List<String> exceptClips = null)
    {
        if(shootEffect == null) return;
        if(shootEffectClips==null) return;
        if(shootEffectClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        foreach(AudioClip clip in shootEffectClips)
        {
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        shootEffect.clip = realClips[index];
        if(shootEffect.loop) shootEffect.loop = false;
        shootEffect.Play();
        Debug.Log("[AudioManager]:Trigger Random Shoot Effect " + shootEffect.clip.name);
    }
    //随机切换背景音乐
    public void TriggerRandomBgm(List<String> exceptClips = null)
    {
        if(bgm == null) return;
        if(bgmClips == null) return;
        if(bgmClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        foreach(AudioClip clip in bgmClips)
        {
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        bgm.clip = realClips[index];
        if(bgm.isPlaying) bgm.Stop();
        bgm.Play();
        Debug.Log("[AudioManager]:Trigger Random Bgm " + bgm.clip.name);
    }
    //随机切换拾取音效
    public void TriggerRandomCollectEffect(List<String> exceptClips = null)
    {
        if(collect == null) return;
        if(collectClips == null) return;
        if(collectClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        foreach(AudioClip clip in collectClips)
        {
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        collect.clip = realClips[index];
        if(collect.loop) collect.loop = false;
        collect.Play();
        Debug.Log("[AudioManager]:Trigger Random Collect Effect " + collect.clip.name);
    }
    //激活随机道具使用音效
    public void TriggerRandomPropEffect(List<String> exceptClips = null)
    {
        if(propEffect == null) return;
        if(propEffectClips==null) return;
        if(propEffectClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        foreach(AudioClip clip in propEffectClips)
        {
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        propEffect.clip = realClips[index];
        if(propEffect.loop) propEffect.loop = false;
        propEffect.Play();
        Debug.Log("[AudioManager]:Trigger Random Prop Effect " + propEffect.clip.name);
    }
    //激活随机死亡音效
    public void TriggerRandomDeathEffect(List<String> exceptClips = null)
    {
        if(deathEffect == null) return;
        if(deathEffectClips == null) return;
        if(deathEffectClips.Count <= 0) return;
        List<AudioClip> realClips = new List<AudioClip>();
        for(int i = 0; i < deathEffectClips.Count; i++)
        {
            AudioClip clip = deathEffectClips[i];
            if(exceptClips != null)
            {
                if(exceptClips.Contains(clip.name)) continue;
            }
            realClips.Add(clip);
        }
        if(realClips.Count <= 0) return;
        int index = UnityEngine.Random.Range(0, realClips.Count);
        deathEffect.clip = realClips[index];
        if(deathEffect.loop) deathEffect.loop = false;
        deathEffect.Play();
        Debug.Log("[AudioManager]:Trigger Random Death Effect " + deathEffect.clip.name);
    }
}
