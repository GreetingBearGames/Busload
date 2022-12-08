using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour {
    [SerializeField] private float forwardSpeed, touchThreshold, horizontalSpeed, horizontalMoveMultiplier, rotateSpeed, rotateBackToSpeed;
    [SerializeField] private GameObject groundObj;
    private Vector3 horizontalMove;
    private Touch touch;
    private float deltaPosX, groundBoundsX, busBoundsX;
    private void Start() {
        groundBoundsX = groundObj.GetComponent<Renderer>().bounds.size.x;
        busBoundsX = GetComponent<Renderer>().bounds.size.x;
    }
    private void FixedUpdate() {
        MoveForward();
        MoveLeftAndRight();
    }
    private void MoveForward() {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
    }
    private void MoveLeftAndRight() {
        if (Input.touchCount > 0) {
            touch = Input.GetTouch(0);
            switch (touch.phase) {
                case TouchPhase.Began:
                    break;
                case TouchPhase.Moved:
                    deltaPosX = touch.deltaPosition.x;
                    if (deltaPosX > touchThreshold) {     //Move to right
                        horizontalMove = new Vector3(transform.position.x + deltaPosX * horizontalMoveMultiplier, transform.position.y, transform.position.z);
                        transform.position = Vector3.Lerp(transform.position, horizontalMove, horizontalSpeed * Time.deltaTime);
                        var limitX = transform.position;
                        limitX.x = Mathf.Clamp(transform.position.x, -groundBoundsX / 2 + busBoundsX / 2, groundBoundsX / 2 - busBoundsX / 2);
                        transform.position = limitX;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -20), Time.deltaTime * rotateSpeed);
                    } else if (deltaPosX < -touchThreshold) {
                        horizontalMove = new Vector3(transform.position.x + deltaPosX * horizontalMoveMultiplier, transform.position.y, transform.position.z);
                        transform.position = Vector3.Lerp(transform.position, horizontalMove, horizontalSpeed * Time.deltaTime);
                        var limitX = transform.position;
                        limitX.x = Mathf.Clamp(transform.position.x, -groundBoundsX / 2 + busBoundsX / 2, groundBoundsX / 2 - busBoundsX / 2);
                        transform.position = limitX;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 20), Time.deltaTime * rotateSpeed);
                    }
                    break;
                case TouchPhase.Ended:
                    break;
            }
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), Time.deltaTime * rotateBackToSpeed);
        }
    }
}