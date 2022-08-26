using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChangeLanguage : MonoBehaviour
{
    public void ChangeLang( Dropdown tmp)
    {
        LangsList.SetLanguage(tmp.value, true);
    }
}
