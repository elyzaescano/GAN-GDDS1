using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueCharacter : MonoBehaviour
{
    public Animator charAnim;
    public AudioSource walking;
    [SerializeField] private AudioClip[] stepclips;
    private void Start() {
        charAnim = GetComponent<Animator>();
        walking = GetComponent<AudioSource>();
    }

    void Step()
    {
        AudioClip clip = GetRandomClip();
        walking.PlayOneShot(clip);
    }
    private AudioClip GetRandomClip()
    {
        return stepclips[UnityEngine.Random.Range(0, stepclips.Length)];
    }

}