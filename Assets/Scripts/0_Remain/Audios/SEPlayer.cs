using CriWare;
using UnityEngine;

[RequireComponent(typeof(CriAtomSource))]
public class SEPlayer : SingletonMonoBehaviour {
    private CriAtomSource atomSource;

    protected override void Start() {
        base.Start();
    }
    protected override void OnStart() {
        atomSource = GetComponent<CriAtomSource>();
        GameEvents.onSETime.AddListener((cueName) => PlaySE(cueName));
    }

    private void PlaySE(string cueName) {
        atomSource.cueName = cueName;
        atomSource.Play();
    }
}
