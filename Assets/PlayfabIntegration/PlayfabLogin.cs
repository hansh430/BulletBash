using PlayFab;
using PlayFab.ClientModels;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayfabLogin : MonoBehaviour
{
    [Header("Login UI")]
    [SerializeField] private TMP_InputField loginUsername;
    [SerializeField] private TMP_InputField loginPassword;

    [Header("Register UI")]
    [SerializeField] private TMP_InputField username;
    [SerializeField] private TMP_InputField password;
    [SerializeField] private TMP_InputField verifyPassword;
    [SerializeField] private TMP_InputField email;

    [SerializeField] private GameObject loginInProgress;
    public void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
        loginUsername.text = PlayerPrefs.GetString(PlayFabConstants.SavedUsername, "");

        if (PlayFabClientAPI.IsClientLoggedIn())
        {
            Debug.LogWarning("User is already logged in");
        }
    }
    private void Login(ILogin loginMethod, object loginParams)
    {
        loginMethod.Login(_loginInfoParams, OnLoginSuccess, OnLoginFailure, loginParams);
        loginInProgress.SetActive(true);
    }

    #region Email Login

    public void LoginWithEmail()
    {
        if (ValidateLoginData())
        {
            Login(new EmailLogin(), new EmailLogin.EmailLoginParams(loginUsername.text, loginPassword.text));
        }
    }

    private bool ValidateLoginData()
    {
        //Validating data
        string errorMessage = "";

        if (loginUsername.text.Length < 5)
        {
            errorMessage = "Username must be at least 5 characters";
        }
        else if (loginPassword.text.Length < 8)
        {
            errorMessage = "Password must be at least 8 characters";
        }

        if (errorMessage.Length > 0)
        {
            Debug.LogError(errorMessage);
            return false;
        }

        return true;
    }

    #endregion

    #region Email Register

    public void Register()
    {
        if (!ValidateRegisterData()) return;

        var request = new RegisterPlayFabUserRequest
        {
            TitleId = PlayFabConstants.TitleID,
            Email = email.text,
            Password = password.text,
            Username = username.text,
            InfoRequestParameters = _loginInfoParams,
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnLoginFailure);
        loginInProgress.SetActive(true);
    }

    bool ValidateRegisterData()
    {
        Debug.Log("Password: " + password.text + "verify pass: " + verifyPassword.text);
        //Validating data
        string errorMessage = "";

        if (!email.text.Contains("@"))
        {
            errorMessage = "E-mail is not valid";
        }
        else if (email.text.Length < 5)
        {
            errorMessage = "E-mail is not valid";
        }
        else if (username.text.Length < 5)
        {
            errorMessage = "Username must be at least 5 characters";
        }
        else if (password.text.Length < 8)
        {
            errorMessage = "Password must be at least 8 characters";
        }
        else if (password.text != verifyPassword.text)
        {
            errorMessage = "Password doesn't match Repeat password";
        }

        if (errorMessage.Length > 0)
        {
            Debug.LogError(errorMessage);
            return false;
        }

        return true;
    }

    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Register Success!");

        PlayerPrefs.SetString("USERNAME", username.text);
        PlayerPrefs.SetString("PW", password.text);

        Debug.Log(result.PlayFabId);
        Debug.Log(result.Username);
        loginInProgress.SetActive(false);
    }

    #endregion


    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Login Success!");
        Debug.Log(result.PlayFabId);

        PlayerPrefs.SetString(PlayFabConstants.SavedUsername, username.text);

        loginInProgress.SetActive(false);

        SceneManager.LoadScene(1);
    }

    private readonly GetPlayerCombinedInfoRequestParams _loginInfoParams =
        new GetPlayerCombinedInfoRequestParams
        {
            GetUserAccountInfo = true,
            GetUserData = true,
            GetUserInventory = true,
            GetUserVirtualCurrency = true,
            GetUserReadOnlyData = true
        };

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.LogError("Login failure: " + error.Error + "  " + error.ErrorDetails + error + "  " +
                       error.ApiEndpoint + "  " + error.ErrorMessage);
        loginInProgress.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
