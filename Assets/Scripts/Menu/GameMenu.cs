using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject[] MenuObject;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(MenuObject.Length != 0)
            {
                foreach (GameObject item in MenuObject)
                { 
                    if(item.name == "OptionsPanel")
                        item.SetActive(false);
                    else
                        item.SetActive(!item.activeSelf);
                }
            }
        }
    }
}
