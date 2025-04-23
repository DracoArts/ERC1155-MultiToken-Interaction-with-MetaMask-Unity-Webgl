using Models;
using Web3Unity.Scripts.Library.ETHEREUEM.Connect;
using UnityEngine;
using System;
using System.Numerics;
using Newtonsoft.Json;
using UnityEngine.UI;
public class MintErc1155 : MonoBehaviour
{
    // Start is called before the first frame update

    public string contract = "0x7286Cf0F6E80014ea75Dbc25F545A3be90F4904F";
    private string id;
     private string amount;
    public InputField tokenIdInput;
    public InputField amountInput;
    public InputField HashText;
    public Text statusText;

    public Button mintBTn;
    public GameObject LoadingScreen;

    private string erc1155Abi = @"[{
        ""inputs"": [
            {""internalType"": ""address"", ""name"": ""account"", ""type"": ""address""},
            {""internalType"": ""uint256"", ""name"": ""id"", ""type"": ""uint256""},
            {""internalType"": ""uint256"", ""name"": ""amount"", ""type"": ""uint256""}
        ],
        ""name"": ""mint"",
        ""outputs"": [],
        ""stateMutability"": ""nonpayable"",
        ""type"": ""function""
    },
    {
        ""inputs"": [
            {""internalType"": ""address"", ""name"": ""account"", ""type"": ""address""},
            {""internalType"": ""uint256"", ""name"": ""id"", ""type"": ""uint256""}
        ],
        ""name"": ""balanceOf"",
        ""outputs"": [
            {""internalType"": ""uint256"", ""name"": """", ""type"": ""uint256""}
        ],
        ""stateMutability"": ""view"",
        ""type"": ""function""
    }]";



    void Start()
    {
        mintBTn.onClick.AddListener(Mint);
    }


    async public void Mint()
    {
        LoadingScreen.SetActive(true);
        id = tokenIdInput.text;
        amount = amountInput.text;
        string account = PlayerPrefs.GetString("Account");
        
        // Validate inputs
        if (string.IsNullOrEmpty(id)){
            statusText.text = "Please enter a token ID";
            LoadingScreen.SetActive(false);
            return;
        }
        
        if (string.IsNullOrEmpty(amount)) {
            statusText.text = "Please enter an amount";
            LoadingScreen.SetActive(false);
            return;
        }

        string method = "mint";
        string[] obj = { account, id, amount };
        string args = JsonConvert.SerializeObject(obj);
        string value = "0";
        string gasLimit = "";
        string gasPrice = "";

        try
        {
            string response = await Web3GL.SendContract(method, erc1155Abi, contract, args, value, gasLimit, gasPrice);
            Debug.Log(response);

            HashText.text = response;
            statusText.text = "Mint successful!";
            LoadingScreen.SetActive(false);
            
            // Check balance after minting
        
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            statusText.text = "Mint failed: " + e.Message;
            LoadingScreen.SetActive(false);
        }
    }

}
