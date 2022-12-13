using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Slider levelProgressBar;
    [SerializeField] private GameObject finishLine;
    private float maxDistancetoFinish;
    [SerializeField] private BusController busController;
    private void Awake()
    {
        maxDistancetoFinish = finishLine.transform.position.z - busController.transform.position.z;
    }

    void Update(){
        float distance = finishLine.transform.position.z - busController.transform.position.z;
        levelProgressBar.value = 1 - (distance / maxDistancetoFinish);
    }
}
