using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool _isJungleUnloaded;
    private bool _isJungleLoaded;
    private GameObject _jungle;
    private GameObject _desert;

    private bool _isDesertPlainUnloaded;
    private GameObject _plain;
    private GameObject _cliff;

    private void Awake()
    {
        if (!Application.isEditor)
        {
            /*SceneLoader.Instance.LoadNewSceneAdditive("Persistent");
            SceneLoader.Instance.LoadNewSceneAdditive("Jungle");
            SceneLoader.Instance.LoadNewSceneAdditive("Ocean");*/
            
            /*SceneManager.LoadScene("Persistent", LoadSceneMode.Additive);
            SceneManager.LoadScene("Jungle", LoadSceneMode.Additive);
            SceneManager.LoadScene("Ocean", LoadSceneMode.Additive);*/
        } // Unused

        switch (SceneManager.GetActiveScene().name)
        {
            case "Jungle Ocean":
                _jungle = GameObject.Find("Jungle");
                _desert = GameObject.Find("Desert");
                
                _desert.SetActive(false);
                break;
            
            case "Desert Plain":
                _plain = GameObject.Find("Plain");
                _cliff = GameObject.Find("Cliff");

                _cliff.SetActive(false);
                break;
        }
        
    }

    public void Trigger(string triggerName)
    {
        switch (triggerName)
        {
            case "Load Jungle":
                if (!_isJungleLoaded)
                {
                    StartCoroutine(ChangeScene("Scenes/Jungle Ocean"));
                }
                break;
            
            case "Unload Jungle":
                if (!_isJungleUnloaded)
                {
                    _isJungleUnloaded = true;
                    _jungle.SetActive(false);
                    _desert.SetActive(true);
                }
                break;
            
            case "Load Desert Plain":
                StartCoroutine(ChangeScene("Scenes/Desert Plain"));
                break;
            
            case "Load Cliff":
                /*if (!_isDesertPlainUnloaded)
                {
                    _isDesertPlainUnloaded = true;
                    _plain.SetActive(false);
                    _cliff.SetActive(true);
                }*/
                StartCoroutine(ChangeScene("Scenes/Desert Cliff"));
                break;
            
            case "Load Canyon":
                StartCoroutine(ChangeScene("Scenes/Desert Canyon"));
                break;
            case "Load Rebirth":
                StartCoroutine(ChangeScene("Scenes/Rebirth"));
                break;
        }
    }

    IEnumerator ChangeScene(string newScene, int delay = 0)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(newScene);
        yield return null;
    }
    
    IEnumerator ChangeScene(int sceneNr)
    {
        SceneManager.LoadScene(sceneNr);
        yield return null;
    }
}
