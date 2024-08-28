using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class PlayerPrefsResetEditor
{
    static PlayerPrefsResetEditor()
    {
        // Reset PlayerPrefs when entering play mode in the Editor
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("All PlayerPrefs have been reset from the Editor.");
    }
}

