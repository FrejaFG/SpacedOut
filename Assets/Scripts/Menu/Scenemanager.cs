using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{

    [SerializeField]
    GameObject[] MenuPanels;
    public void OnSceneChangeBtn(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void OnQuitClick()
    {
        Debug.Log("Closing Game");
        Application.Quit();
    }

    /// <summary>
    /// Set the active state of the specified menu panel GameObject, and deactivate all other menu panel GameObjects.
    /// </summary>
    /// <param name="id">The index of the menu panel GameObject to activate.</param>
    public void OpenPanel(int id)
    {
        for (int i = 0; i < MenuPanels.Length; i++)
        {
            if(i == id)
                MenuPanels[i].SetActive(true);
            else
                MenuPanels[i].SetActive(false);
        }
    }
}
