using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    SelectedCharacters.Data data = new SelectedCharacters.Data();

    private int i;
    public GameObject[] AllCharacters;

    private void Start()
    {
        data = JsonUtility.FromJson<SelectedCharacters.Data>(PlayerPrefs.GetString("SaveGame"));
        StartCoroutine(LoadCharacter());
    }

    public IEnumerator LoadCharacter()
    {
        i = 0;

        while (AllCharacters[i].name != data.currentCharacter)
        {
            i++;
        }

        AllCharacters[i].SetActive(true);

        yield return null;
    }
}
