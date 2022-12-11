using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassengerCount : MonoBehaviour
{
    [SerializeField] private BusProps busProps;
    [SerializeField] private Canvas canvas;
    private float _forwardSpeed;
    [SerializeField] GameObject _bus;
    Vector3 Offset = Vector3.zero;

    void Start()
    {
        Offset = transform.position - worldToUISpace(canvas, _bus.transform.position);
        _forwardSpeed = busProps.forwardSpeed;
    }

    void LateUpdate()
    {
        Debug.Log(worldToUISpace(canvas, _bus.transform.position));
        transform.position = worldToUISpace(this.GetComponent<Canvas>(), _bus.transform.position) + Offset;
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
