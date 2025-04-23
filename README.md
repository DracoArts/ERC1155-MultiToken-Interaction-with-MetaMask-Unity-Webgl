
# Welcome to DracoArts

![Logo](https://dracoarts-logo.s3.eu-north-1.amazonaws.com/DracoArts.png)



 # ERC-1155 Multi-Token Interaction with MetaMask

 This guide explains how to connect MetaMask to a Unity game using ChainSafe SDK to interact with ERC-1155 smart contracts for:

- Checking balances of multiple token types

- Minting new tokens (fungible and non-fungible)

# 1. Connecting MetaMask 

## How the Connection Works
- Unity initiates connection via MetaMask browser extension or WalletConnect

- User approves connection in MetaMask

- Game receives:Unity initiates connection via MetaMask browser extension or WalletConnect

- User approves connection in MetaMask

#### Game receives:

- Wallet address

- Web3 provider for transactions

## Why Use MetaMask?
### Security: 
- Private keys stay in MetaMask (never exposed to Unity).

### User-Friendly: 
- Players confirm transactions directly in MetaMask.

### Cross-Platform: 
- Works on PC (browser extension) & Mobile (WalletConnect).


# Checking ERC-1155 Balances

## Single Token Balance Check
- Game detects connected wallet

#### Calls balanceOf with:

- Wallet address

- Specific token ID

- Returns quantity owned of that token type

## Batch Balance Check
- Pass array of token IDs

- Calls balanceOfBatch

- Returns array of balances for all requested tokens


# 3. Minting ERC-1155 Tokens (If Supported by Contract)
## Minting Process

1. Game prepares transaction with:

- Recipient address

- Token ID to mint

- Quantity (can be >1 for fungible tokens)

- Optional data field


2. MetaMask prompts user to:

- Confirm gas fees

- Sign transaction

- Transaction broadcasts to blockchain

- Game awaits confirmation

# 4. Security & Best Practices
### Security Features
- Private key protection

- Transaction confirmation requirements

- Visible gas fee estimation

### Best Practices for Developers

✔ Always test on a testnet first (Sepolia, Mumbai).

✔ Handle transaction errors gracefully (show feedback if mint fails).


✔ Cache wallet data (avoid spamming RPC calls).

# Prerequisites
Unity 2021.3+

WebGL build support

ChainSafe SDK installed

Basic C# and Unity knowledge

Testnet ETH/MATIC for transactions





## Usage/Examples
Balance Erc1155

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

Mint  Erc1155

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
## Images

 Erc1155 Balance & Mint
![](https://github.com/AzharKhemta/Gif-File-images/blob/main/erc1155%20Balance.gif?raw=true)


## Authors

- [@MirHamzaHasan](https://github.com/MirHamzaHasan)
- [@WebSite](https://mirhamzahasan.com)


## 🔗 Links

[![linkedin](https://img.shields.io/badge/linkedin-0A66C2?style=for-the-badge&logo=linkedin&logoColor=white)](https://www.linkedin.com/company/mir-hamza-hasan/posts/?feedView=all/)
## Documentation

[ChainSafe Docs](https://docs.gaming.chainsafe.io/)




## Tech Stack
**Client:** Unity, C#

**Blockchain:** Ethereum/Sepolia

**Plugin:**  ChainSafe Web3.unity


