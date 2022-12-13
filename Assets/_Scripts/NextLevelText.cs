using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class NextLevelText : MonoBehaviour
{
    public TextMeshProUGUI currentLevelText;
    private void Awake() {
        var currentLevel = SceneManager.GetActiveScene().buildIndex + 2;
        currentLevelText.text = currentLevel.ToString();
    }
}
