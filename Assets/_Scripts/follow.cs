using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow : MonoBehaviour
{
    public GameObject followObj;
    Vector3 _offset;
    [SerializeField][Range(0f, 1f)] private float _lerpTime;
    private void Start()
    {
        _offset = new Vector3(0, -7.85f, 5.9f);
    }

    private void LateUpdate()
    {
        Vector3 a = followObj.transform.position - _offset;
        a.x = 0;
        transform.position = Vector3.Lerp(transform.position, a, _lerpTime);
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