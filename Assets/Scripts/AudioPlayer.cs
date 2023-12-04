using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Academic Success")]
    [SerializeField] AudioClip academicSuccessAFX;
    [SerializeField] [Range(0f, 1f)] float academicSuccessVolume = 1f;

    public void PlayAcademicSuccessClip()
    {
        PlayClip(academicSuccessAFX, academicSuccessVolume);
    }

    [Header("Academic Fail")]
    [SerializeField] AudioClip academicFailAFX;
    [SerializeField] [Range(0f, 1f)] float academicFailVolume = 1f;

    public void PlayAcademicFailClip()
    {
        PlayClip(academicFailAFX, academicFailVolume);
    }

    [Header("Social Success")]
    [SerializeField] AudioClip socialSuccessAFX;
    [SerializeField] [Range(0f, 1f)] float socialSuccessVolume = 1f;

    public void PlaySocialSuccessClip()
    {
        PlayClip(socialSuccessAFX, socialSuccessVolume);
    }

    [Header("Social Fail")]
    [SerializeField] AudioClip socialFailAFX;
    [SerializeField] [Range(0f, 1f)] float socialFailVolume = 1f;

    public void PlaySocialFailClip()
    {
        PlayClip(socialFailAFX, socialFailVolume);
    }

    void PlayClip (AudioClip clip, float volume)
    {
        if(clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
