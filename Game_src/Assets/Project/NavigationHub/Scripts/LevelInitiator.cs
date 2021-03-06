using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * LevelInitiator
 * Script for controlling the players interactions with Portals and Guides. 
 */
public class LevelInitiator : MonoBehaviour
{
    public GameObject guideDialogue;
    public GameObject[] guideDialogueUI;
    public string[] guidePhrase;
    private bool startTwentiesLevel, startFiftiesLevel, startEightiesLevel, _alreadyTalked;

    public void Update()
    {
        StartSelectedLevel();
    }

    public void OnTriggerEnter(Collider other)
    {
        _alreadyTalked = false;
        ActivePortal(other, true, true);
    }

    public void OnTriggerExit(Collider other)
    {
        _alreadyTalked = true;
        ActivePortal(other, false, false);
    }

    // For switching Portals depending on the collider the player has interacted with.
    // Also switches input for the Guide's dialogue.
    private void ActivePortal(Component other, bool level, bool dActive)
    {
        if (other.CompareTag($"1920sCollider"))
        {
            if (!_alreadyTalked)
                guideDialogue.GetComponent<NavHubGuideDialogues>().Talk(guidePhrase[0], 0);
            
            startTwentiesLevel = level;
            guideDialogueUI[0].SetActive(dActive);
        }
        else if (other.CompareTag($"1950sCollider"))
        {
            if (!_alreadyTalked)
                guideDialogue.GetComponent<NavHubGuideDialogues>().Talk(guidePhrase[1], 1);
            
            startFiftiesLevel = level;
            guideDialogueUI[1].SetActive(dActive);
        }
        else if (other.CompareTag($"1980sCollider"))
        {
            if (!_alreadyTalked)
                guideDialogue.GetComponent<NavHubGuideDialogues>().Talk(guidePhrase[2], 2);
            
            startEightiesLevel = level;
            guideDialogueUI[2].SetActive(dActive);
        }
    }

    // For Loading levels based on collider and key input.
    private void StartSelectedLevel()
    {
        if (startTwentiesLevel && Input.GetKey(KeyCode.G))
        {
            startTwentiesLevel = false;
            // Fade out Audio, Scene and call LoadLevel.
            Fader.CallFader(false, true);
            NavHubAudio.FadeMusic(false, true);
            StartCoroutine(LoadLevel("03_TwentiesOpeningScene"));
        }
        else if (startFiftiesLevel && Input.GetKey(KeyCode.G))
        {
            startFiftiesLevel = false;
            Fader.CallFader(false, true);
            NavHubAudio.FadeMusic(false, true);
            StartCoroutine(LoadLevel("05_RetrofuturismLevel"));
        }
        else if (startEightiesLevel && Input.GetKey(KeyCode.G))
        {
            startEightiesLevel = false;
            Fader.CallFader(false, true);
            NavHubAudio.FadeMusic(false, true);
            StartCoroutine(LoadLevel("1980s_Intro")); // TODO - Update to main project.
        }
    }

    // IEnumerator with string param to change scene in StartSelectedLevel.
    private IEnumerator LoadLevel(string levelName)
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(levelName, LoadSceneMode.Single);
    }
}