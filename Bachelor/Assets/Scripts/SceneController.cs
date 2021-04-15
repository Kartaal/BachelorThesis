using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour {

    int currentSceneIndex;

    /*
     * This refers to the scene to change to, going backwards in a branch will
     * still update this variable !!
     * This variable is updated by CalculateNextSceneIndex() and CalculatePreviousSceneIndex()
     */
    private int nextSceneIndex = 19; // temp debug setting default behaviour for nav arrows to go to credits scene

    // Use this for initialization
    void Start () {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        /*
        if (currentSceneIndex == 0)
        {
            StartCoroutine(WaitForTime());
        }
        */
    }
    

    private void CalculateNextSceneIndex()
    {
        switch (currentSceneIndex)
        {
            case 5:
            case 10:
            case 13:
            case 16:
            case 18:
            case 19:
            case 20:
                        nextSceneIndex = 0; // if end of branch, go home (??)
                        break;
            default:
                        nextSceneIndex = currentSceneIndex + 1;
                        break;
        }
    }

    private void CalculatePreviousSceneIndex()
    {
        switch (currentSceneIndex)
        {
            case 1:
            case 6:
            case 11:
            case 14:
            case 17:
            case 19:
            case 20:
                        nextSceneIndex = 0; // if start of branch, go home
                        break;
            default:
                        nextSceneIndex = currentSceneIndex - 1;
                        break;
        }
    }

    public void LoadNextScene()
    {
        CalculateNextSceneIndex();
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadPreviousScene()
    {
        CalculatePreviousSceneIndex();
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void LoadHome()
    {
        SceneManager.LoadScene("Home");
    }

    public void LoadLearnTransitionYDS()
    {
        SceneManager.LoadScene("Learn Transition YDS");
    }

    public void LoadLearnTransitionOA()
    {
        SceneManager.LoadScene("Learn Transition OA");
    }

    public void LoadExploreTransitionYDS()
    {
        SceneManager.LoadScene("Explore Transition YDS");
    }

    public void LoadExploreTransitionOA()
    {
        SceneManager.LoadScene("Explore Transition OA");
    }

    public void LoadCompareTransition()
    {
        SceneManager.LoadScene("Compare Transition");
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