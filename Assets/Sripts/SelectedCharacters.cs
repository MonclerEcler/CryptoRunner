using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectedCharacters : MonoBehaviour
{
    SelectedCharacters.Data data = new SelectedCharacters.Data();

    private int i;
    public GameObject[] AllCharacters;
    public GameObject ArrowToLeft;
    public GameObject ArrowToRight;
    public GameObject ButtonBuyCharacter;
    public GameObject ButtonSelectCharacter;
    public GameObject TextSelectCharacter;
    
    private string statusCheck;
    private int check;
    public Text TextPrice;
    

    [System.Serializable]
    public class Data
    {
        public string currentCharacter = "Biker";
        public List<string> haveCharacters = new List<string> { "Biker" };
        public int money;
        
    }

    
    private void Start()
    {
        if (PlayerPrefs.HasKey("SaveGame"))
        {
            data = JsonUtility.FromJson<SelectedCharacters.Data>(PlayerPrefs.GetString("SaveGame"));
        }
        else
        {
            data.money = 500;
            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));
        }

        AllCharacters[i].SetActive(true);

        if (data.currentCharacter == AllCharacters[i].name)
        {
            ButtonBuyCharacter.SetActive(false);
            ButtonSelectCharacter.SetActive(false);
            TextSelectCharacter.SetActive(true);
        }
        else if (data.currentCharacter != AllCharacters[i].name)
        {
            StartCoroutine(CheckHaveCharacter());
        }

        if (i > 0)
        {
            ArrowToLeft.SetActive(true);
        }

        if (i == AllCharacters.Length)
        {
            ArrowToRight.SetActive(false);
        }
    }

    public IEnumerator CheckHaveCharacter()
    {
        while (statusCheck != "Check")
        {
            if (data.haveCharacters.Count != check)
            {
                if (AllCharacters[i].name != data.haveCharacters[check])
                {
                    check++;
                }
                else if (AllCharacters[i].name == data.haveCharacters[check])
                {
                    TextSelectCharacter.SetActive(false);
                    ButtonBuyCharacter.SetActive(false);
                    ButtonSelectCharacter.SetActive(true);
                    check = 0;
                    statusCheck = "Check";
                }
            }
            else if (data.haveCharacters.Count == check)
            {
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(false);
                ButtonBuyCharacter.SetActive(true);
                TextPrice.text = AllCharacters[i].GetComponent<Item>().priceCharacter.ToString();
                check = 0;
                statusCheck = "Check";
            }
        }
        statusCheck = "";

        yield return null;
    }
    public void ArrowRight()
    {
        if (i < AllCharacters.Length)
        {
            if (i == 0)
            {
                ArrowToLeft.SetActive(true);
            }

            AllCharacters[i].SetActive(false);
            i++;
            AllCharacters[i].SetActive(true);

            if (data.currentCharacter == AllCharacters[i].name)
            {
                ButtonBuyCharacter.SetActive(false);
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(true);
            }
            else if (data.currentCharacter != AllCharacters[i].name)
            {
                StartCoroutine(CheckHaveCharacter());
            }

            if (i + 1 == AllCharacters.Length)
            {
                ArrowToRight.SetActive(false);

            }
        }
    }

    public void ArrowLeft()
    {
        if (i < AllCharacters.Length)
        {
            AllCharacters[i].SetActive(false);
            i--;
            AllCharacters[i].SetActive(true);
            ArrowToRight.SetActive(true);

            if (data.currentCharacter == AllCharacters[i].name)
            {
                ButtonBuyCharacter.SetActive(false);
                ButtonSelectCharacter.SetActive(false);
                TextSelectCharacter.SetActive(true);
            }
            else if (data.currentCharacter != AllCharacters[i].name)
            {
                StartCoroutine(CheckHaveCharacter());
            }

            if (i == 0)
            {
                ArrowToLeft.SetActive(false);
            }

        }
    }

    public void SelectCharacter()
    {
        data = JsonUtility.FromJson<SelectedCharacters.Data>(PlayerPrefs.GetString("SaveGame"));
        data.currentCharacter = AllCharacters[i].name;
        PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));

        ButtonSelectCharacter.SetActive(false);
        TextSelectCharacter.SetActive(true);
    }

    public void BuyCharacter()
    {
        if (data.money >= AllCharacters[i].GetComponent<Item>().priceCharacter)
        {
            data = JsonUtility.FromJson<SelectedCharacters.Data>(PlayerPrefs.GetString("SaveGame"));

            data.money = data.money - AllCharacters[i].GetComponent<Item>().priceCharacter;
            data.haveCharacters.Add(AllCharacters[i].name);
           
            PlayerPrefs.SetString("SaveGame", JsonUtility.ToJson(data));

            ButtonBuyCharacter.SetActive(false);
            ButtonSelectCharacter.SetActive(true);
        }

    }

    public void ChangeScence()
    {
        SceneManager.LoadScene(1);
    }
}
