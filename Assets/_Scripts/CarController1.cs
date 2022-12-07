using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController1 : MonoBehaviour {
    [SerializeField] private float fwdSpeed, horizontalSpeedFactor, inputSensitivity, lerpValue;
    [SerializeField] private float rollRotSpeed, rollTurnSpeed, touchThreshold;
    [SerializeField] private GameObject floorObject;
    private Touch touch;
    private Vector2 touchPos, previousTouchPos;
    private float normalizedDeltaPosition, targetPos, finishLinePos;
    private Vector3 lastPosition;
    private GameObject planeObj;
    private float floorMinX, floorMaxX, planeMinX, planeMaxX;


    void Start() {
        planeObj = this.gameObject;
        planeMinX = planeObj.GetComponent<MeshCollider>().bounds.min.x;
        planeMaxX = planeObj.GetComponent<MeshCollider>().bounds.max.x;
        floorMinX = floorObject.GetComponent<MeshCollider>().bounds.min.x;
        floorMaxX = floorObject.GetComponent<MeshCollider>().bounds.max.x;
    }

    void Update() {
        TouchInput();
        MovewithSlide();
        RollRotation();
    }


    private void TouchInput() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                previousTouchPos = touch.position;
                touchPos = touch.position;
            }
            if (touch.phase == TouchPhase.Moved) {
                touchPos = touch.position;
            }

            normalizedDeltaPosition = ((touchPos.x - previousTouchPos.x) / Screen.width) * inputSensitivity;
        } else {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * rollRotSpeed * rollTurnSpeed);
            //transform.rotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
        targetPos = targetPos + normalizedDeltaPosition;
        targetPos = Mathf.Clamp(targetPos, floorMinX - planeMinX, floorMaxX - planeMaxX);
        previousTouchPos = touchPos;
    }

    private void MovewithSlide() {
        float horizontalSpeed = fwdSpeed * Time.deltaTime * horizontalSpeedFactor;
        float newPositionTarget = Mathf.Lerp(transform.position.x, targetPos, horizontalSpeed);
        float newPositionDifference = newPositionTarget - transform.position.x;

        newPositionDifference = Mathf.Clamp(newPositionDifference, -horizontalSpeed, horizontalSpeed);
        Vector3 desiredPos = new Vector3(transform.position.x + newPositionDifference, transform.position.y, transform.position.z + fwdSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, desiredPos, lerpValue);
    }



    private void RollRotation() {

        if (transform.position.x - lastPosition.x > 0.005)  //sağa yatıyor
        {
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -10);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rollRotSpeed);
        } else if (transform.position.x - lastPosition.x < -0.005)  //sola yatıyor
          {
            var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 10);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rollRotSpeed);
        }
        lastPosition = transform.position;
    }
}