using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

    public class PlayFabLogin
    {
        public event Action<string> Onsuccess;
        public void Login(string email, string pass)
        {
            var request = new LoginWithEmailAddressRequest
            {
                Email = email,
                Password = pass
            };

            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
        }
        private void OnLoginSuccess(LoginResult result)
        {

            Debug.Log("Succesful login/account create!!");
            Onsuccess?.Invoke(result.PlayFabId);
            //GameManager.instance.OptionsMenu();

        }

        private void OnError(PlayFabError error)
        {
            Debug.Log("Error while logging in/creating account");
            Debug.Log(error.GenerateErrorReport());

        }

    }


