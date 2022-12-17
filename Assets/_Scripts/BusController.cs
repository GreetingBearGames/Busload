using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour {
    [SerializeField] private float forwardSpeed, touchThreshold, horizontalSpeed, horizontalMoveMultiplier, rotateSpeed, rotateBackToSpeed;
    [SerializeField] private GameObject groundObj, finishLine;
    [SerializeField] private BusProps busProps = null;

    private float _deltaPosX, _groundBoundsX, _busBoundsX, _humanCountinScene, f = 0, _busBoundsZ, startTime;
    private bool _isEnd = false, _isContinue = true, _isFinish = false;
    private Vector3 _horizontalMove, afterFinishMove, startPos;
    private Touch _touch;
    private int frames = 5, maxFrames = 180;
    public AnimationCurve curve;

    private void Start() {
        var chassis = GameObject.FindGameObjectWithTag("Chassis");
        _groundBoundsX = groundObj.GetComponent<Renderer>().bounds.size.x;
        _busBoundsX = chassis.GetComponent<Renderer>().bounds.size.x;
        //_busBoundsZ = chassis.GetComponent<Renderer>().bounds.size.z - chassis.transform.localPosition.z;
        _busBoundsZ = GetComponent<Collider>().bounds.size.z;
        forwardSpeed = busProps.forwardSpeed;
        touchThreshold = busProps.touchThreshold;
        horizontalSpeed = busProps.horizontalSpeed;
        horizontalMoveMultiplier = busProps.horizontalMoveMultiplier;
        rotateSpeed = busProps.rotateSpeed;
        rotateBackToSpeed = busProps.rotateBackToSpeed;
        _humanCountinScene = GameObject.FindGameObjectsWithTag("Human").Length;
        finishLine = GameObject.FindGameObjectWithTag("FinishLine");
        startTime = Time.time;
    }
    private void Update() {
        MoveForward();
        MoveLeftAndRight();
        WinLevel();
        LoseLevel();
    }
    private void MoveForward() {
        if (GameManager.Instance.IsGameStarted) {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
        }
    }
    private void MoveLeftAndRight() {
        if (Input.touchCount > 0) {
            _touch = Input.GetTouch(0);
            switch (_touch.phase) {
                case TouchPhase.Moved:
                    _deltaPosX = _touch.deltaPosition.x;
                    if (_deltaPosX > touchThreshold) {     //Move to right
                        _horizontalMove = new Vector3(transform.position.x + _deltaPosX / Screen.width * horizontalMoveMultiplier, transform.position.y, transform.position.z);
                        transform.position = Vector3.Lerp(transform.position, _horizontalMove, horizontalSpeed);
                        var limitX = transform.position;
                        limitX.x = Mathf.Clamp(transform.position.x, -_groundBoundsX / 2 + _busBoundsX / 2, _groundBoundsX / 2 - _busBoundsX / 2);
                        transform.position = limitX;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, -25), rotateSpeed);
                    } else if (_deltaPosX < -touchThreshold) {
                        _horizontalMove = new Vector3(transform.position.x + _deltaPosX / Screen.width * horizontalMoveMultiplier, transform.position.y, transform.position.z);
                        transform.position = Vector3.Lerp(transform.position, _horizontalMove, horizontalSpeed);
                        var limitX = transform.position;
                        limitX.x = Mathf.Clamp(transform.position.x, -_groundBoundsX / 2 + _busBoundsX / 2, _groundBoundsX / 2 - _busBoundsX / 2);
                        transform.position = limitX;
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 25), rotateSpeed);
                    }
                    break;
            }
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), rotateBackToSpeed);
    }
    private void WinLevel() {
        if (GameManager.Instance.isWin()) {
            touchThreshold = 0;
            horizontalSpeed = 0;
            horizontalMoveMultiplier = 0;
            rotateSpeed = 0;
            rotateBackToSpeed = 0;
            transform.eulerAngles = new Vector3(0, 0, 0);

            var decreaseRate = GameManager.Instance.Passenger / (_humanCountinScene * GameManager.Instance.PassengerIncreaseRate);
            decreaseRate *= 20;
            if (decreaseRate == 0) decreaseRate = 1;
            var targetPos = GameObject.FindGameObjectsWithTag("FinishMultiplier")[(int)decreaseRate - 1].transform.position;
            targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            GameManager.Instance.FinishMultiplier = (int)decreaseRate;
            forwardSpeed = 0;
            if(!_isFinish){
                startPos = transform.position;
                _isFinish = true;
            }
            Vector3 velocity = Vector3.zero;
            transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 0.05f, 100);
            if (_isContinue && Mathf.Abs(transform.position.z - targetPos.z) < 0.2f) {
                _isEnd = true;
                _isContinue = false;
            }

        }
        if (_isEnd && !GameManager.Instance.IsLose) {
            _isEnd = false;
            GameManager.Instance.UpdateMoney(GameManager.Instance.FinishMultiplier * GameManager.Instance.Money - GameManager.Instance.Money);
            StartCoroutine(NextLevel(3.0f));
        }
    }
    private IEnumerator NextLevel(float time) {
        yield return new WaitForSeconds(time);
        GameManager.Instance.NextLevel();
    }
    private void LoseLevel() {
        if (GameManager.Instance.IsLose) {
            touchThreshold = 0;
            horizontalSpeed = 0;
            horizontalMoveMultiplier = 0;
            rotateSpeed = 0;
            rotateBackToSpeed = 0;
            forwardSpeed = 0;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    public void StartBus() {
        GameManager.Instance.IsGameStarted = true;
    }
}