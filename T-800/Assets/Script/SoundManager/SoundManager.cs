using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{
    private static Dictionary<SoundScriptable, AudioSource> s_Source = new Dictionary<SoundScriptable, AudioSource>();
    private static GameObject s_SourcesGameObject = null;
    public static AudioSource GetSource(SoundScriptable p_Sound)
    {
        //chercher dans le dico si la source existe pour le son donné
        //Appliquer les réglages
        //Renvoyer la source
        if(s_SourcesGameObject == null)
        {
            s_SourcesGameObject = new GameObject("SoundManager");
        }

        if(s_Source.TryGetValue(p_Sound, out AudioSource l_AudioSource))
        {
            l_AudioSource.volume = p_Sound.volume;
            l_AudioSource.pitch = p_Sound.pitch;
            l_AudioSource.loop = p_Sound.loop;
        }
        else
        {
            l_AudioSource = s_SourcesGameObject.AddComponent<AudioSource>();
            l_AudioSource.volume = p_Sound.volume;
            l_AudioSource.pitch = p_Sound.pitch;
            l_AudioSource.loop = p_Sound.loop;
            s_Source.Add(p_Sound, l_AudioSource);
        }
        return l_AudioSource;
    }
}
