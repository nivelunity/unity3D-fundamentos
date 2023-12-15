using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    [SerializeField]
    AudioClip winCombatClip, loseCombatClip, startCombatClip, choiceCombatClip, endCombatClip, winGameClip, gameOverClip;

    // Start is called before the first frame update
    private AudioSource myAudioSource;
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();   
    }

    private void PlaySFX(AudioClip newSFX)
    {
        if (myAudioSource.isPlaying) return;
            myAudioSource.PlayOneShot(newSFX, 0.9f);
    }

    public void StartCombatSFX()
    {
        PlaySFX(startCombatClip);
    }
    public void WinCombatSFX()
    {
        PlaySFX(winCombatClip);
    }
    public void LoseCombatSFX()
    {
        PlaySFX(loseCombatClip);
    }
    public void EndCombatSFX()
    {
        PlaySFX(endCombatClip);
    }

    public void GameWinSFX()
    {
        PlaySFX(winGameClip);
    }
    public void GameOverSFX()
    {
        PlaySFX(gameOverClip);
    }

    public void ChoiceCombatSFX()
    {
        PlaySFX(choiceCombatClip);
    }
}
