using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using Web3Unity.Scripts.Library.ETHEREUEM.EIP;

public class ERC1155BalanceOfExample : MonoBehaviour
{

         public string contract = "0x2c1867bc3026178a47a677513746dcc6822a137a";
        string account;
        public  InputField  tokenIdInput;

        public Button       buttonBalance;

        private string tokenId;

    async void Start()
    {
        

        buttonBalance .onClick.AddListener(Balance);
        account= PlayerPrefs.GetString("Account");
       
    }

    private async void Balance(){
        
        BigInteger balanceOf = await ERC1155.BalanceOf(contract, account, tokenId);
        print(balanceOf);

    }
}
