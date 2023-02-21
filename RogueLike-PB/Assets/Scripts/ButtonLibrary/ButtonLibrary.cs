using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLibrary : Singleton<ButtonLibrary>
{

#region Variables & Properties



#endregion

#region Methods

public void OpenGame()
{
   SceneManager.LoadScene("MapScene");
}

public void OpenMainMenu()
{
    SceneManager.LoadScene("MenùScene");
}

public void ExitGame()
{
#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
}

#endregion

}
