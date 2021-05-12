using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class ResourceReader : MonoBehaviour
{
    [SerializeField]
    private GameObject entryPrefab, scrollPane;
    [SerializeField]
    private TextAsset file;

    private string resourceInfo;

    // Start is called before the first frame update
    void Start()
    {
        string fs = file.text;
        string[] fLines = Regex.Split ( fs, "\n|\r|\r\n" );
        
        for ( int i=0; i < fLines.Length; i++ )
        {
            if(fLines[i].Length > 0)
            {
                TokenReader(fLines[i]);
            }
        }

    }


    // isolated method for reading first token of line.
    private void TokenReader(string s)
    {
        switch(s[0])
        {
            case '%':
                // new entry - Should be the only symbol on line. Nothing beyond the first char will be ignored.
                // create new text  prefab and place accordingly.
                
                var newResource = Instantiate(entryPrefab, new Vector2(0,0) , Quaternion.identity);
                // Set parent of current entry 
                newResource.transform.SetParent(scrollPane.transform);
                newResource.GetComponent<TextMeshProUGUI>().SetText(resourceInfo);
                resourceInfo = "";
                break;
            case '!':
                // STOP sign
                break;
            default:
                // Assume text is a valid entry. - Add paragraph to text object.
                resourceInfo = resourceInfo + s + "\n";
                break;
        }

    }
}
