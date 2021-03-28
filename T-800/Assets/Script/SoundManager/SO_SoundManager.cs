//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Audio;

//[CreateAssetMenu(fileName = "NewSoundManager", menuName = "Sound")]
//public class SO_SoundManager : ScriptableObject
//{
//    //public SoundScriptable[] scriptableSounds;
//    private ScriptableObject script;

//    public Dictionary<SoundScriptable, AudioSource> m_SoundManager = new Dictionary<SoundScriptable, AudioSource>();

//    [SerializeField]
//    string namesound = null;

//    GameObject m_SoundManagerInstance = null;

//    private void OnEnable()
//    {
//        if(m_SoundManagerInstance == null)
//        {
//            m_SoundManagerInstance = new GameObject("SoundManager");
//        }
//    }

//    //private void Awake()
//    //{   
//    //    foreach (SoundScriptable s in scriptableSounds)
//    //    {
//    //        s.source = m_SoundManagerInstance.AddComponent<AudioSource>();
//    //        s.source.volume = s.volume;
//    //        s.source.pitch = s.pitch;
//    //        s.source.loop = s.loop;
//    //        s.source.outputAudioMixerGroup = s.mixer;
//    //    }
//    //}

//    private void Start()
//    {
//        //PlaySound();
//    }

//    public static void GetSound()
//    {

//    }
//    public void PlaySound(SoundScriptable p_Sound)
//    {
//        AudioSource l_AudioSource = null;
//        if (!m_SoundManager.TryGetValue(p_Sound, out l_AudioSource))
//        {
//            l_AudioSource = m_SoundManagerInstance.AddComponent<AudioSource>();
//            p_Sound.source.volume = l_AudioSource.volume;
//            p_Sound.source.pitch = l_AudioSource.pitch;
//            p_Sound.source.loop = l_AudioSource.loop;
//            p_Sound.source.clip = p_Sound.clip[Random.Range(0, p_Sound.clip.Length)];
//            p_Sound.source.Play();
//        }
//        m_SoundManager.Add(p_Sound, l_AudioSource);
//    }

//    //public void Play(string name)
//    //{
//    //    SoundScriptable s = System.Array.Find(scriptableSounds, sound => sound.naming == name);

//    //    if (s.source.isPlaying)
//    //    {
//    //        return;
//    //    }
//    //    else
//    //    {
//    //        s.source.clip = s.clip[Random.Range(0, s.clip.Length)];
//    //        s.source.pitch = Random.Range(.8F, 1.2F);
//    //        s.source.Play();
//    //    }
//    //}

//    //public void Stop(string name)
//    //{
//    //    SoundScriptable s = System.Array.Find(scriptableSounds, sound => sound.naming == name);
//    //    s.source.Stop();
//    //}
//}
