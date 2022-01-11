using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static SFXPlayer Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }

    public void PlaySFX(AudioClip sfx)
    {
        AudioSource.PlayClipAtPoint(sfx, transform.position);
    }
}
