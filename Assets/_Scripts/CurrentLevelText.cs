using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CurrentLevelText : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    private void Start()
    {
        var currentLevel = GameManager.Instance.SavedLevel;
        currentLevelText.text = currentLevel.ToString();
    }
}
