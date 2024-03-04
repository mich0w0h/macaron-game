using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGMPlayer : SingletonMonoBehaviour {
    private AudioSource audioSource;
    protected override void Start() {
        base.Start();
    }
    protected override void OnStart() {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        
        GameEvents.onAdComplete.AddListener(Play);    
        GameEvents.onAdShown.AddListener(Stop);

        GameEvents.onLastEpilogueStart.AddListener(FadeInVolume);
        GameEvents.onTransitInComplete.AddListener((currentScene) => {
            if(PlayerData.IsLastDay() && currentScene == "MainScene") {
                FadeOutVolume();
            }
        });

        Play();
    }

    private void Play() {
        audioSource.Play();
        Debug.Log("BGM: Play");
    }

    private void Stop() {
        audioSource.Stop();
        Debug.Log("BGM: Stop");
    }

    private void FadeInVolume() {
        StartCoroutine(DelayFadeIn());
    }

    private void FadeOutVolume() {
        StartCoroutine(LerpAnimation.Fade(audioSource, 1f, 0f, 4.5f));
    }

    private IEnumerator DelayFadeIn() {
        yield return new WaitForSeconds(5f);
        yield return LerpAnimation.Fade(audioSource, 0f, 1f, 15f);
    }

}
