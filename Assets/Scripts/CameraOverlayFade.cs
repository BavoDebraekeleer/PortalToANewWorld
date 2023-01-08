using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CameraOverlayFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float sphereCheckSize = .15f;
    [SerializeField] private LayerMask collisionLayer;
    [SerializeField] private bool playAudioOnTrigger;
    [SerializeField] private bool playVideoOnTrigger;
    [SerializeField] private bool fadeInOnAwake;
    [SerializeField] private bool nonTrigger;
    [SerializeField] private bool turnOffAudioOnTrigger;
    [SerializeField] private AudioSource[] audioToTurnOff;

    private Material _cameraFadeMat;
    private bool _isCameraFadeOut;
    private AudioSource _audioSource;
    private VideoPlayer _videoPlayer;
    private bool _externalFadeOut;

    private void Awake()
    {
        _cameraFadeMat = GetComponent<Renderer>().material;
        _audioSource = GetComponent<AudioSource>();
        _videoPlayer = GetComponent<VideoPlayer>();

        if (fadeInOnAwake)
        {
            _cameraFadeMat.SetFloat("_AlphaValue", 1f);
            FadeIn(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (nonTrigger)
        {
            if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Ignore))
                FadeOut();
            else
                FadeIn();
        }
        else
        {
            if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayer, QueryTriggerInteraction.Collide))
            {
                FadeOut();

                if (playAudioOnTrigger)
                    StartCoroutine(PlayAudio());
            
                if (playVideoOnTrigger)
                    _videoPlayer.Play();

                if (turnOffAudioOnTrigger)
                    StartCoroutine(TurnOffAudio());
            }
            else
                FadeIn();
        }

        if (_externalFadeOut)
        {
            FadeOut();

            if (playAudioOnTrigger)
                StartCoroutine(PlayAudio());
            
            if (playVideoOnTrigger)
                _videoPlayer.Play();

            if (turnOffAudioOnTrigger)
                StartCoroutine(TurnOffAudio());
        }
    }

    /*public void CameraFade(float targetAlpha)
    {
        var fadeValue =
            Mathf.MoveTowards(_cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * fadeSpeed);
        _cameraFadeMat.SetFloat("_AlphaValue", fadeValue);

        if (fadeValue <= 0.01f)
            _isCameraFadeOut = false;
    }*/
    
    IEnumerator CameraFade(float targetAlpha, int delay = 0)
    {
        yield return new WaitForSeconds(delay);
        
        var fadeValue =
            Mathf.MoveTowards(_cameraFadeMat.GetFloat("_AlphaValue"), targetAlpha, Time.deltaTime * fadeSpeed);
        _cameraFadeMat.SetFloat("_AlphaValue", fadeValue);

        /*if (fadeValue <= 0.01f)
            _isCameraFadeOut = false;
        else if (fadeValue >= 0.99f) ;
            _isCameraFadeOut = true;*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.75f);
        Gizmos.DrawSphere(transform.position, sphereCheckSize);
    }

    IEnumerator PlayAudio()
    {
        _audioSource.Play();
        playAudioOnTrigger = false;
        yield return null;
    }

    IEnumerator TurnOffAudio()
    {
        for (int i = 0; i < audioToTurnOff.Length; i++)
        {
            audioToTurnOff[i].Stop();
        }
        yield return null;
    }

    private void FadeIn(int delay = 0)
    {
        StartCoroutine(CameraFade(0f, delay));
    }

    public void FadeOut(int delay = 0)
    {
        StartCoroutine(CameraFade(1f, delay));
    }

    public void SetFadeOut()
    {
        _externalFadeOut = true;
    }
}
