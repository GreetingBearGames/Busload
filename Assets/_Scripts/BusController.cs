using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BusController : MonoBehaviour {
    [SerializeField] private float forwardSpeed, touchThreshold, horizontalSpeed, horizontalMoveMultiplier, rotateSpeed, rotateBackToSpeed, finishSpeed;
    [SerializeField] private GameObject groundObj;
    [SerializeField] private BusProps busProps = null;
    private List<GameObject> finishMultiplier;
    private float _deltaPosX, _groundBoundsX, _busBoundsX, _humanCountinScene;
    private bool _isEnd = false, _isContinue = true, _isFinish = false;
    private Vector3 _horizontalMove, startPos;
    private Touch _touch;

    private void Start() {
        var finishMultiplierArr = GameObject.FindGameObjectsWithTag("FinishMultiplier");
        finishMultiplier = finishMultiplierArr.ToList().OrderBy(x => x.transform.position.z).ToList();
        var chassis = GameObject.FindGameObjectWithTag("Chassis");
        _groundBoundsX = groundObj.GetComponent<Renderer>().bounds.size.x;
        _busBoundsX = chassis.GetComponent<Renderer>().bounds.size.x;
        forwardSpeed = busProps.forwardSpeed;
        touchThreshold = busProps.touchThreshold;
        horizontalSpeed = busProps.horizontalSpeed;
        horizontalMoveMultiplier = busProps.horizontalMoveMultiplier;
        rotateSpeed = busProps.rotateSpeed;
        rotateBackToSpeed = busProps.rotateBackToSpeed;
        finishSpeed = busProps.finishSpeed;
        _humanCountinScene = GameObject.FindGameObjectsWithTag("Human").Length;
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

            var targetPos = finishMultiplier[(int)decreaseRate - 1].transform.position;
            targetPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
            GameManager.Instance.FinishMultiplier = (int)decreaseRate;
            forwardSpeed = 0;
            if (!_isFinish) {
                startPos = transform.position;
                _isFinish = true;
            }
            StartCoroutine(ChangeObjectZandXPos(transform, targetPos.z, targetPos.x, decreaseRate / finishSpeed));
            if (_isContinue && transform.position.z == targetPos.z) {
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
    public static IEnumerator ChangeObjectZandXPos(Transform transform, float z_target, float x_target, float duration) {
        float elapsed_time = 0; //Elapsed time
        Vector3 pos = transform.position; //Start object's position
        float z_start = pos.z; //Start "y" value
        float x_start = pos.x; //Start "y" value
        while (elapsed_time <= duration){ //Inside the loop until the time expires
            pos.z = Mathf.Lerp(z_start, z_target, EaseOut(elapsed_time / duration)); //Changes and interpolates the position's "y" value
            pos.x = Mathf.Lerp(x_start, x_target, EaseOut(elapsed_time / duration));
            transform.position = pos;//Changes the object's position
            yield return null; //Waits/skips one frame
            elapsed_time += Time.deltaTime; //Adds to the elapsed time the amount of time needed to skip/wait one frame
        }
    }
    static float Flip(float x) {
        return 1 - x;
    }
    public static float EaseOut(float t) {
        return Flip(Mathf.Pow(Flip(t), 2));
    }
}