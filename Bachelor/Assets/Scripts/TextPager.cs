using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPager : MonoBehaviour
{
    List<GameObject> textGOs = new List<GameObject>();
    Text progressIndicator;
    int textIndex = 0;
    int textCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        Transform textContainerTransform =  transform.parent.Find("TextContainer");
        foreach (Transform tf in textContainerTransform)
        {
            textGOs.Add(tf.gameObject);
        }

        progressIndicator = transform.Find("ProgressText").gameObject.GetComponent<Text>();

        textCount = textGOs.Count;
        UpdateProgress();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Hides all the texts
    public void HideAll()
    {
        foreach (GameObject go in textGOs)
        {
            go.SetActive(false);
        }
    }

    // Displays the next text, if already displaying the last then do nothing
    public void ShowNext()
    {
        if (textIndex+1 < textGOs.Count)
        {
            HideAll();
            textIndex++;
            textGOs[textIndex].SetActive(true);

            UpdateProgress();
        }
    }

    // Displays the previous text, if already displaying the first then do nothing
    public void ShowPrevious()
    {
        if (textIndex > 0)
        {
            HideAll();
            textIndex--;
            textGOs[textIndex].SetActive(true);
            
            UpdateProgress();
        }
    }


    private void UpdateProgress()
    {
        progressIndicator.text = $"{textIndex+1}/{textCount}";
    }
}
