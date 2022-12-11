using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deneme : MonoBehaviour
{
    public GameObject Player;
    public Canvas canvas;


    Vector3 Offset = Vector3.zero;

    void Start()
    {
        Offset = transform.position - worldToUISpace(canvas, Player.transform.position);
    }

    void Update()
    {
        //Convert the player's position to the UI space then apply the offset
        transform.position = worldToUISpace(canvas, Player.transform.position) + Offset;
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }
}
