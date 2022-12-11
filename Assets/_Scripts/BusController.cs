using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusController : MonoBehaviour {
    [SerializeField] private float forwardSpeed, touchThreshold, horizontalSpeed, horizontalMoveMultiplier, rotateSpeed, rotateBackToSpeed, passengerCount;
    [SerializeField] private GameObject groundObj;
    [SerializeField] private BusProps busProps = null;
    public float ForwardSpeed{
        get => forwardSpeed;
    }
    private float _deltaPosX, _groundBoundsX, _busBoundsX, _maxForwardSpeed;
    private bool _isWinBus = true, _isEnd = true;
    private Vector3 _horizontalMove;
    private Touch _touch;
    private void Start() {
        _groundBoundsX = groundObj.GetComponent<Renderer>().bounds.size.x;
        _busBoundsX = GetComponent<Renderer>().bounds.size.x;
        forwardSpeed = busProps.forwardSpeed;
        touchThreshold = busProps.touchThreshold;
        horizontalSpeed = busProps.horizontalSpeed;
        horizontalMoveMultiplier = busProps.horizontalMoveMultiplier;
        rotateSpeed = busProps.rotateSpeed;
        rotateBackToSpeed = busProps.rotateBackToSpeed;
    }
    private void Update() {
        MoveForward();
        MoveLeftAndRight();
        WinLevel();
        LoseLevel();
    }
    private void MoveForward() {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + transform.forward, forwardSpeed * Time.deltaTime);
    }
    private void MoveLeftAndRight() {
        if (Input.touchCount > 0) {
            _touch = Input.GetTouch(0);
            switch (_touch.phase) {
                case TouchPhase.Began:
                    break;
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
                case TouchPhase.Stationary:
                    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0), rotateBackToSpeed);
                    break;
            }
            
        }
    }
    private void WinLevel() {
        if (GameManager.Instance.isWin()) {
            touchThreshold = 0;
            horizontalSpeed = 0;
            horizontalMoveMultiplier = 0;
            rotateSpeed = 0;
            rotateBackToSpeed = 0;
            transform.eulerAngles = new Vector3(0,0,0);
            if(_isWinBus){
                _isWinBus = false;
                forwardSpeed = GameManager.Instance.Passenger;
                _maxForwardSpeed = forwardSpeed;
                GameManager.Instance.UpdateMoney(10);
                Debug.Log("Finish Line Money : " + GameManager.Instance.Money);
            }
            forwardSpeed = Mathf.Clamp(forwardSpeed + -12.5f * Time.deltaTime, 0, _maxForwardSpeed);
        }
        if(forwardSpeed <= 0 && _isEnd && !GameManager.Instance.IsLose){
            _isEnd = false;
            GameManager.Instance.UpdateMoney(GameManager.Instance.FinishMultiplier * GameManager.Instance.Money - GameManager.Instance.Money);
            StartCoroutine(NextLevel(1.0f));
        }
    }
    private IEnumerator NextLevel(float time)
    {
        yield return new WaitForSeconds(time);
        GameManager.Instance.LoadNextLevel();
    }
    private void LoseLevel(){
        if(GameManager.Instance.IsLose){
            touchThreshold = 0;
            horizontalSpeed = 0;
            horizontalMoveMultiplier = 0;
            rotateSpeed = 0;
            rotateBackToSpeed = 0;
            forwardSpeed = 0;
            transform.eulerAngles = new Vector3(0,0,0);
        }
    }
}