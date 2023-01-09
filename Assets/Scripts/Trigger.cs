using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] private LayerMask triggerLayer;    
    [SerializeField] private float sphereCheckSize = .15f;
    [SerializeField] private String command1;
    [SerializeField] private String command2;
    [SerializeField] private CameraOverlayFade cameraOverlay;
    [SerializeField] private bool fadeOutOnCmd1 = true;
    [SerializeField] private bool fadeOutOnCmd2 = true;
    private bool _isCommand1Given;


    private void OnTriggerEnter(Collider other)
    {
        gameManager.Trigger(other.gameObject.name);
    }
    
    void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereCheckSize, triggerLayer, QueryTriggerInteraction.Collide))
        {
            if (!_isCommand1Given)
            {
                if (fadeOutOnCmd1)
                {
                    //cameraOverlay.SetFadeOut();
                    cameraOverlay.FadeOut();
                }

                gameManager.Trigger(command1);
                _isCommand1Given = true;
            }
            else
            {
                if (fadeOutOnCmd2)
                {
                    //cameraOverlay.SetFadeOut();
                    cameraOverlay.FadeOut();
                }

                gameManager.Trigger(command2);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0f, 1f, 0f, 0.75f);
        Gizmos.DrawSphere(transform.position, sphereCheckSize);
    }
}
