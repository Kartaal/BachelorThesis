using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermImageManager : MonoBehaviour
{

    [SerializeField]
    private List<Image> imageList;
    // Start is called before the first frame update

    public void ActivateImage(int id)
    {
        ClearImage();
                
        switch(id)
        {
            case 0:
            // Default case. Remove all images.
            ClearImage();
            break;
            case 1:
                imageList[0].gameObject.SetActive(true);
            break;
            case 2:
                imageList[1].gameObject.SetActive(true);
            break;
            case 3:
                imageList[2].gameObject.SetActive(true);
            break;
            case 4:
                imageList[3].gameObject.SetActive(true);
            break;
            case 5:
                imageList[4].gameObject.SetActive(true);
            break;
            case 6:
                imageList[5].gameObject.SetActive(true);
                imageList[6].gameObject.SetActive(true);
                imageList[7].gameObject.SetActive(true);
                imageList[8].gameObject.SetActive(true);
            break;

            default:
                Debug.Log("ERROR: No applicable image.");
            break;
        }
    }

    // just a regular cleanup of all active images...
    // Alternative implementation would be to keep track of the active images.
    private void ClearImage(){
        foreach(Image i in imageList)
                {
                    // Just a lil check to avoid null pointer errors in case we want to make the list larger
                    if (i != null)
                    {
                            // Hides everything!
                            i.gameObject.SetActive(false);
                    }
                }

    }
}
