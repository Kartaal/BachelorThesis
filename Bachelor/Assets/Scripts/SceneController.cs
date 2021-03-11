using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    /*
    [SerializeField] int timeToWait = 4;
    int currentSceneIndex;
    */

    // Use this for initialization
    void Start () {
        /*
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
        */
    }
    

    /*
    IEnumerator WaitForTime()
    {
        yield return new WaitForSeconds(timeToWait);
        LoadNextScene();
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    */

    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void LoadLearnTransitionYDS()
    {
        SceneManager.LoadScene("Learn Transition YDS");
    }

    public void LoadTermsYDS()
    {
        SceneManager.LoadScene("Introduce Terms YDS");
    }

    public void LoadLockedYDS()
    {
        SceneManager.LoadScene("Locked Walkthrough YDS");
    }

    public void LoadMultipleChoiceYDS()
    {
        SceneManager.LoadScene("Multiple Choice YDS");
    }

    public void LoadDIYYDS()
    {
        SceneManager.LoadScene("DIY YDS");
    }

    public void LoadLearnTransitionOA()
    {
        SceneManager.LoadScene("Learn Transition OA");
    }

    public void LoadTermsOA()
    {
        SceneManager.LoadScene("Introduce Terms OA");
    }

    public void LoadLockedOA()
    {
        SceneManager.LoadScene("Locked Walkthrough OA");
    }

    public void LoadMultipleChoiceOA()
    {
        SceneManager.LoadScene("Multiple Choice OA");
    }

    public void LoadDIYOA()
    {
        SceneManager.LoadScene("DIY OA");
    }

    public void LoadExploreTransitionYDS()
    {
        SceneManager.LoadScene("Explore Transition YDS");
    }

    public void LoadExploreSetupYDS()
    {
        SceneManager.LoadScene("Explore Setup YDS");
    }

    public void LoadExploreRunYDS()
    {
        SceneManager.LoadScene("Explore Run YDS");
    }

    public void LoadExploreTransitionOA()
    {
        SceneManager.LoadScene("Explore Transition OA");
    }

    public void LoadExploreSetupOA()
    {
        SceneManager.LoadScene("Explore Setup OA");
    }

    public void LoadExploreRunOA()
    {
        SceneManager.LoadScene("Explore Run OA");
    }

    public void LoadCompareTransition()
    {
        SceneManager.LoadScene("Compare Transition");
    }

    public void LoadCompare()
    {
        SceneManager.LoadScene("Compare Main");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadResources()
    {
        SceneManager.LoadScene("Resources");
    }
    
    public void QuitApp()
    {
        Application.Quit();
    }

}