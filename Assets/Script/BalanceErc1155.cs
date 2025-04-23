using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Web3Unity.Scripts.Library.ETHEREUEM.EIP;

public class BalanceErc1155 : MonoBehaviour
{
     public string contract = "0x2c1867bc3026178a47a677513746dcc6822a137a";

    public InputField tokenIdInput;
    public Button buttonBalance;
   public Text  balanceText;
    public  Text statusText;
   
    private string tokenId;
    string account;

    async void Start()
    {


        buttonBalance.onClick.AddListener(Balance);
        account = PlayerPrefs.GetString("Account");

    }

    private async void Balance()
    {

        if (string.IsNullOrEmpty(tokenIdInput.text))
        {
            statusText.text = "Please enter a Token ID";
            return;
        }
        string tokenId = tokenIdInput.text;
        try
        {
            // Show loading state
            buttonBalance.interactable = false;
            statusText.text = "Checking balance...";
            balanceText.text = "---";

            // Call blockchain
            BigInteger balance = await ERC1155.BalanceOf(contract, account, tokenId);

            balanceText.text = balance.ToString();
            statusText.text = "Balance loaded!";
            statusText.color = Color.green;
            buttonBalance.interactable = true;
             statusText.text = $"Balance check successful for token {tokenId}: {balance}";
        }
        catch (System.Exception e)
        {
            // Handle errors
            statusText.text = "Balance check failed";
            
            
    }
        

    }
}
