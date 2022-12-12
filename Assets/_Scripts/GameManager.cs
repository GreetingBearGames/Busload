using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private AudioSource inGameMusic;
    private int _health, _totalHealth, _finishMultiplier;
    private float _passenger, _passengerIncreaseRate = 1, _money, _moneyIncreaseRate = 1;
    private static GameManager _instance;   //Create instance and make it static to be sure that only one instance exist in scene.
    private bool _isGameOver = false, _isWin = false, _isLose = false;
    public static GameManager Instance
    {     //To access GameManager, we use GameManager.Instance
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }
    }
    public float Money
    {       //Money property. You can get money from outside this script, but you can only set in this script.
        get => _money;
        private set => _money = value;
    }
    public int FinishMultiplier
    {
        get => _finishMultiplier;
        set => _finishMultiplier = value;
    }
    public bool IsLose => _isLose;

    public int Health
    {       //Health property. You can get health from outside this script, but you can only set in this script.
        get => _health;
        private set => _health = value;
    }
    public int TotalHealth
    {       //Health property. You can get health from outside this script, but you can only set in this script.
        get => _totalHealth;
        private set => _totalHealth = value;
    }
    public float Passenger
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _passenger;
        private set => _passenger = value;
    }
    public float PassengerIncreaseRate
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _passengerIncreaseRate;
        private set => _passengerIncreaseRate = value;
    }
    public float MoneyIncreaseRate
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _moneyIncreaseRate;
        private set => _moneyIncreaseRate = value;
    }
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        inGameMusic.loop = true;            //Make inGameMusic loop
        inGameMusic.Play();                 //Plays inGameMusic
    }
    public void GameOver(bool flag)
    {    //Sets the game over situation. 
        _isGameOver = flag;             //Ex. usage GameManager.Instance.Gameover(true) inside of obstacle script.
    }
    public bool isGameOver()
    {          //To check is game over
        return _isGameOver;
    }
    public void OnRestartButtonClicked()
    {          //Restart Button Clicked
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {                   //Loads Next Level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void UpdateMoney(float updateAmount)
    {     //To update money.Use positive value to increment. Use negative value to decrement.
        _money += updateAmount;
        if (_money < 0)
        {                           //To make sure that money is above 0.
            _money = 0;
        }
    }
    public void WinLevel(bool flag)
    {       //Call this function when player finish the level successfully inside of finishline.
        _isWin = flag;
    }
    public bool isWin()
    {                    //To check is the game successfully finished.
        return _isWin;
    }
    public void LoseLevel()
    {               //Call this when player can't finish the game successfully.
        gameOverCanvas.gameObject.SetActive(true);
        _isLose = true;
    }
    public void UpdateBusHealth(int updateAmount)
    {   //To update health.Use positive value to increment. Use negative value to decrement.
        if (_health + updateAmount <= 0)
        {     //To make sure that health is above 0.
            LoseLevel();
        }
        else if (_health + updateAmount > _totalHealth)
        {
            _health = _totalHealth;
        }
        else
        {                               //To make sure that health is less or equal to total health.
            _health += updateAmount;
        }
    }
    public void UpdatePassengerCount(float updateAmount)
    {
        if (_passenger + updateAmount < 1)
        {        //If passenger count is less than 1 it means it is a lose level.
            _passenger = 0;
            LoseLevel();
        }
        else
        {
            _passenger += updateAmount;
            Mathf.Floor(_passenger);
        }

    }
}
