using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class GUIOptions : MonoBehaviour
{
    GameObject playerDataObject;
    GameObject FwdBtn;
    GameObject BckBtn;
    GameObject LftBtn;
    GameObject RgtBtn;
    PlayerData playerData;
    PlayerOptions options;

    [SerializeField]
    GameObject ClickedBtn = null;

    /// <summary>
    /// Find and assign references to game objects and components needed by this script.
    /// </summary>
    private void Awake()
    {
        playerDataObject = GameObject.Find("PlayerDataObject");
        playerData = playerDataObject.GetComponent<PlayerData>();
        FwdBtn = GameObject.Find("ForwardBtn");
        BckBtn = GameObject.Find("BackWardBtn");
        LftBtn = GameObject.Find("LeftBtn");
        RgtBtn = GameObject.Find("RightBtn");
    }
    private void Start()
    {
        options = playerData.GetOptions();
    }

    /// <summary>
    /// Update the references to the game objects and components needed by this script, and update the key binds UI and data.
    /// </summary>
    private void Update()
    {
        if(FwdBtn == null)
            FwdBtn = GameObject.Find("ForwardBtn");
        if(BckBtn == null)
            BckBtn = GameObject.Find("BackWardBtn");
        if(LftBtn == null)
            LftBtn = GameObject.Find("LeftBtn");
        if(RgtBtn == null)
            RgtBtn = GameObject.Find("RightBtn");
        if (options != null)
            UpdateKeyBindsUI();
        UpdateKeyBinds();
    }

    /// <summary>
    /// Update the text displayed on the key bind UI buttons with the current key bind data from the options variable.
    /// </summary>
    void UpdateKeyBindsUI()
    {
        if(FwdBtn != null)
            FwdBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveForward.ToString());
        if(BckBtn != null)
            BckBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveBack.ToString());
        if (LftBtn != null)
            LftBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveLeft.ToString());
        if (RgtBtn != null)
            RgtBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveRight.ToString());
    }

    /// <summary>
    /// Update the key bind data with the last key pressed by the player, and update the UI to reflect the new key bind data.
    /// </summary>
    void UpdateKeyBinds()
    {
        if(ClickedBtn != null)
        {
            if (Input.anyKeyDown)
            {
                string keyPressed = Input.inputString;
                KeyCode key = (KeyCode)System.Enum.Parse(typeof(KeyCode), keyPressed.ToUpper());

                switch (ClickedBtn.name)
                {
                    case "ForwardBtn":
                        options.MoveForward = key;
                        ClickedBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveForward.ToString());
                        break;
                    case "BackWardBtn":
                        options.MoveBack = key;
                        ClickedBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveBack.ToString());
                        break;
                    case "LeftBtn":
                        options.MoveLeft = key;
                        ClickedBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveLeft.ToString());
                        break;
                    case "RightBtn":
                        options.MoveRight = key;
                        ClickedBtn.GetComponentInChildren<TextMeshProUGUI>().SetText(options.MoveRight.ToString());
                        break;
                    case "None":
                        break;
                }
                playerData.SaveOptions(options);
                ClickedBtn = null;
            }
        }
    }

    /// <summary>
    /// Update the text displayed on the key bind UI button to prompt the player to press a new key, and set the "ClickedBtn" variable to the specified button GameObject.
    /// </summary>
    /// <param name="Btn">The GameObject of the key bind UI button that was clicked.</param>
    public void OnChangeClick(GameObject Btn)
    {
        switch (Btn.name)
        {
            case "ForwardBtn":
                Btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Press Button to chance");
                break;
            case "BackWardBtn":
                Btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Press Button to chance");
                break;
            case "LeftBtn":
                Btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Press Button to chance");
                break;
            case "RightBtn":
                Btn.GetComponentInChildren<TextMeshProUGUI>().SetText("Press Button to chance");
                break;
        }
        ClickedBtn = Btn;

        UpdateKeyBindsUI();
    }

    public void DeleteSave()
    {
        PlayerStats st = new PlayerStats();
        playerData.SaveStats(st);
    }

    public void UpdateOptions()
    {
        playerData.SaveOptions(playerData.GetOptions());
    }
}
