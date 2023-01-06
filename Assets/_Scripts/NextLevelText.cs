using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelText : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    private void Start()
    {
        var currentLevel = GameManager.Instance.SavedLevel + 1;
        currentLevelText.text = currentLevel.ToString();
    }
}
