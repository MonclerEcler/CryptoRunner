using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    [SerializeField] GameObject settingMenu;
    public void setting()
    {
        settingMenu.SetActive(true);
    }

    public void back()
    {
        settingMenu.SetActive(false);
    }
}
