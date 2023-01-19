using System;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ServerModels;
using UnityEngine;


public  class PlayFabGetInventory
{


    public event Action<List<ItemInstance>> OnSuccess;

    public void GetInventory(string userID)
    {
        var request = new GetUserInventoryRequest
        {
            PlayFabId = userID
        };
        PlayFabServerAPI.GetUserInventory(request, OnGetUserInventorySuccess, OnGetFailure);
    }

    private void OnGetUserInventorySuccess(GetUserInventoryResult result)
    {
        Debug.Log("OnGetUserInventorySuccess");
        OnSuccess?.Invoke(result.Inventory);

    }

    private void OnGetFailure(PlayFabError error)
    {
        Debug.Log($"Here's some debug information: {error.GenerateErrorReport()}");

    }


}