using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public static AudioClip jump, hit, walk, killEnemy, takingdamage, bounce;
    static AudioSource audioSrc;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float masterVolume = 1.0f;


    //Inicialização dos clipes de áudio
     void Start()
    {
        jump = Resources.Load<AudioClip>("jump");
        hit = Resources.Load<AudioClip>("hit");
        walk = Resources.Load<AudioClip>("walk");
        killEnemy= Resources.Load<AudioClip>("killEnemy");
        takingdamage= Resources.Load<AudioClip>("takingdamage");
        bounce = Resources.Load<AudioClip>("bounce");
        audioSrc = GetComponent<AudioSource>();

    }
    //Escolha de qual som dá play
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "jump":
                audioSrc.PlayOneShot(jump);
                break;
            case "hit":
                audioSrc.PlayOneShot(hit);
                break;
            case "walk":
                audioSrc.PlayOneShot(walk);
                break;
            case "killEnemy":
                audioSrc.PlayOneShot(killEnemy);
                break;
            case "takingdamage":
                audioSrc.PlayOneShot(takingdamage);
                break;
            case "bounce":
                audioSrc.PlayOneShot(bounce);
                break;
            default:
                break;
        }
    }
    //Volume do som
     void Update()
    {
        AudioListener.volume = masterVolume;
    }
    //Slider do volume do jogo
    public void SetVolume(float vol)
    {
        masterVolume = vol;
    }

}
