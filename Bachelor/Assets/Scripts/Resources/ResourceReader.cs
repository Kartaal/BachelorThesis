using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ResourceReader : MonoBehaviour
{
    private StreamReader sr;
    [SerializeField]
    private GameObject entryPrefab,scrollPane;

    private bool isRunning; // bool to see if we have reached the end of the resources. Dictated by the char '!'

    private string resourceInfo;

    // Start is called before the first frame update
    void Start()
    {
        isRunning = true;
        sr = new StreamReader("Assets/textSource/res.txt");
        while(isRunning){
            string paragraph;
            paragraph = sr.ReadLine();
            if(paragraph == null){Debug.Log("Uhm... what?");}
            TokenReader(paragraph);
        }
    }


    // isolated method for reading first token of line.
    private void TokenReader(string s){
        Debug.Log(s);
        switch(s[0]){

            case '%':
                // new entry - Should be the only symbol on line. Nothing beyond the first char will be ignored.
                // create new text  prefab and place accordingly.
                
                var newResource = Instantiate(entryPrefab, new Vector2(0,0) , Quaternion.identity);
                // Set parent of current entry 
                newResource.transform.SetParent(scrollPane.transform);
                newResource.GetComponent<Text>().text = resourceInfo;
                resourceInfo = "";
                
                break;
            case '!':
                // STOP sign
                isRunning = false;
                break;
            default:
                // Assume text is a valid entry. - Add paragraph to text object.
                resourceInfo = resourceInfo + s + "\n"; // There is a chance this doesnt work...
                break;

        }

    }

    // isolated method to write the text element and finalize it on spot.
    private void WriteResource(string res){


    }
}
