using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    [SerializeField] private float fwdSpeed, horizontalSpeedFactor, inputSensitivity, yawSpeed;
    [SerializeField] private GameObject floorObject;
    private Touch touch;
    private Vector2 touchPos, previousTouchPos;
    private float normalizedDeltaPosition, targetPos;
    private Vector3 lastPosition;
    private Vector3 valueToLerp;


    void Update()
    {
            TouchInput();
            MovewithSlide_new();
            YawRotation();
    }


    private void TouchInput()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                previousTouchPos = touch.position;
                touchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                touchPos = touch.position;
            }

            normalizedDeltaPosition = ((touchPos.x - previousTouchPos.x) / Screen.width) * inputSensitivity;
        }
        targetPos = targetPos + normalizedDeltaPosition;
        targetPos = Mathf.Clamp(targetPos, floorObject.GetComponent<BoxCollider>().bounds.min.x, floorObject.GetComponent<BoxCollider>().bounds.max.x);

        previousTouchPos = touchPos;
    }



    private void MovewithSlide_new()
    {
        float horizontalSpeed = fwdSpeed * Time.deltaTime * horizontalSpeedFactor;
        float newPositionTarget = Mathf.Lerp(transform.position.x, targetPos, horizontalSpeed);
        float newPositionDifference = newPositionTarget - transform.position.x;

        newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);

        transform.position = new Vector3(transform.position.x + newPositionDifference, transform.position.y, transform.position.z + fwdSpeed * Time.deltaTime);
        
    }


    
    private void YawRotation()
    {   
        if (transform.position != lastPosition)
        {
            transform.forward = Vector3.Lerp(transform.forward, (transform.position - lastPosition).normalized, yawSpeed * Time.deltaTime);
        }
        lastPosition = transform.position;
    }    
}
