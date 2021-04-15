using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BranchProgressIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Update text based on active scene index
        SetProgressText();
    }

    // Used to update the text for indicating how far along a branch the user currently is
    private void SetProgressText()
    {
        int currentSceneIndex = currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Text progress = gameObject.GetComponent<Text>();

        // Hardcoded for learn YDS, credits and resources only for now
        switch(currentSceneIndex)
        {
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
                        progress.text = $"{currentSceneIndex}/5";
                        break;
            default:
                        Debug.LogError("Accessed scene not supported in branch progress indicator");
                        break;
        }
    }
}
