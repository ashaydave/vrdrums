using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaySoundOnTrigger : MonoBehaviour
{
    private AudioSource audio;
    public AudioClip audioClip;

    private bool hasPlayed = false;
    private bool useVelocity = true;
    public float minVelocity = 0;
    public float maxVelocity = 2;
    /// <summary>
    /// 
    /// </summary>
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Drumstick"))
        {
            VelocityEstimator estimator = other.GetComponent<VelocityEstimator>();
            if(estimator && useVelocity && !hasPlayed)
            {
                float v = estimator.GetVelocityEstimate().magnitude;
                float volume = Mathf.InverseLerp(minVelocity, maxVelocity, v);
                hasPlayed = true;
                audio.PlayOneShot(audioClip, volume);
                StartCoroutine(ResetHasPlayed());
            }     
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private IEnumerator ResetHasPlayed()
    {
        yield return new WaitForSeconds(0.05f);
        hasPlayed = false;
    }
}
