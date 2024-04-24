using PlayFab;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    [SerializeField] private Button logOutButton;
    [SerializeField] private Button soundButton;
    [SerializeField] private GameObject settingPanel;
    private void Start()
    {
        settingButton.onClick.AddListener(OnClickSettingButton);
        logOutButton.onClick.AddListener(OnClickLogOutButton);
    }
    private void OnClickSettingButton()
    {
        if (settingPanel.activeInHierarchy)
        {
            settingPanel.SetActive(false);
        }
        else
        {
            settingPanel.SetActive(true);
        }
    }
    private void OnClickLogOutButton()
    {
        PlayFabClientAPI.ForgetAllCredentials();
        SceneManager.LoadSceneAsync(0);
    }
}
