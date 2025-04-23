using Models;
using Web3Unity.Scripts.Library.ETHEREUEM.Connect;
using UnityEngine;
using System;
using System.Numerics;
using Newtonsoft.Json;
using UnityEngine.UI;

public class MintWebGL721 : MonoBehaviour
{

    public string contract = "0x7286Cf0F6E80014ea75Dbc25F545A3be90F4904F";
    public string tokenId;
    public  InputField tokenIdInput;
    public  InputField HashText;
    public Button mintBTn;
    public  GameObject LoadingScreen;
     private string erc721Abi = @"[{
        ""inputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""constructor""
 },
    {
        ""anonymous"": false,
        ""inputs"": [
            {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""previousOwner"",
                ""type"": ""address""
            },
            {
                ""indexed"": true,
                ""internalType"": ""address"",
                ""name"": ""newOwner"",
                ""type"": ""address""
            }
        ],
        ""name"": ""OwnershipTransferred"",
        ""type"": ""event""
    },
    {
        ""inputs"": [],
        ""name"": ""renounceOwnership"",
        ""outputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""function""
    },
    {
        ""inputs"": [
            {
                ""internalType"": ""address"",
                ""name"": ""to"",
                ""type"": ""address""
            },
            {
                ""internalType"": ""uint256"",
                ""name"": ""tokenId"",
                ""type"": ""uint256""
            }
        ],
        ""name"": ""publicMint"",
        ""outputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""function""
    },
    
    {
        ""inputs"": [
            {
                ""internalType"": ""address"",
                ""name"": ""owner"",
                ""type"": ""address""
            }
        ],
        ""name"": ""balanceOf"",
        ""outputs"": [
            {
                ""internalType"": ""uint256"",
                ""name"": """",
                ""type"": ""uint256""
            }
        ],
        ""stateMutability"": ""view"",
        ""type"": ""function""
    },
    
    {
        ""inputs"": [
            {
                ""internalType"": ""uint256"",
                ""name"": ""tokenId"",
                ""type"": ""uint256""
            }
        ],
        ""name"": ""ownerOf"",
        ""outputs"": [
            {
                ""internalType"": ""address"",
                ""name"": """",
                ""type"": ""address""
            }
        ],
        ""stateMutability"": ""view"",
        ""type"": ""function""
    },
    
    {
        ""inputs"": [
            {
                ""internalType"": ""uint256"",
                ""name"": ""tokenId"",
                ""type"": ""uint256""
            }
        ],
        ""name"": ""tokenURI"",
        ""outputs"": [
            {
                ""internalType"": ""string"",
                ""name"": """",
                ""type"": ""string""
            }
        ],
        ""stateMutability"": ""view"",
        ""type"": ""function""
    }]";



    void Start()
    {
                mintBTn.onClick.AddListener(OnSendContract);
    }


    async public void OnSendContract()
    {

        LoadingScreen .SetActive(true);
         tokenId=tokenIdInput.text;
        string to=PlayerPrefs.GetString("Account");
        // smart contract method to call
        string method = "publicMint";
        // abi in json format
      
        // array of arguments for contract
        string [] obj = {to,tokenId};
        string args =JsonConvert.SerializeObject(obj);
        // value in wei
        string value = "0";
        // gas limit OPTIONAL
        string gasLimit = "";
        // gas price OPTIONAL
        string gasPrice = "";
        // connects to user's browser wallet (metamask) to update contract state
        try
        {
            string response = await Web3GL.SendContract(method, erc721Abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);

            HashText.text=response;
             LoadingScreen .SetActive(false);
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            
             LoadingScreen .SetActive(false);
        }
    }
   
}