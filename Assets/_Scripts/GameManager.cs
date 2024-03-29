using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    private int _health, _totalHealth, _finishMultiplier, _savedLevel;
    private float _passenger, _passengerIncreaseRate = 1, _money, _moneyIncreaseRate = 1, _moneyCountPerLevel;
    private static GameManager _instance;   //Create instance and make it static to be sure that only one instance exist in scene.
    private bool _isGameOver = false, _isWin = false, _isLose = false, _isGameStarted = false;
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
    public bool IsGameStarted
    {
        get => _isGameStarted;
        set => _isGameStarted = value;
    }

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
        set => _passengerIncreaseRate = value;
    }
    public float MoneyIncreaseRate
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _moneyIncreaseRate;
        set => _moneyIncreaseRate = value;
    }
    public float MoneyCountPerLevel
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _moneyCountPerLevel;
        set => _moneyCountPerLevel = value;
    }
    public int SavedLevel
    {       //Passenger property. You can get passenger from outside this script, but you can only set in this script.
        get => _savedLevel;
        set => _savedLevel = value;
    }
    private void Awake()
    {
        _instance = this;

        _money = PlayerPrefs.GetFloat("MoneyAmount", 0f);
        _passengerIncreaseRate = PlayerPrefs.GetFloat("PassengerParameter", 1f);
        _moneyIncreaseRate = PlayerPrefs.GetFloat("MoneyParameter", 1f);
        _savedLevel = PlayerPrefs.GetInt("SavedLeved", 1);
    }

    public void GameOver(bool flag)
    {    //Sets the game over situation. 
        _isGameOver = flag;             //Ex. usage GameManager.Instance.Gameover(true) inside of obstacle script.
    }
    public bool isGameOver()
    {          //To check is game over
        return _isGameOver;
    }
    // public void OnRestartButtonClicked()
    // {          //Restart Button Clicked
    //     LevelLoader.Current.ChangeLevel("Level " + PlayerPrefs.GetInt("SavedLeved"));    //aynı bölümü tekrar yükle
    // }
    // public void LoadNextLevel()
    // {                   //Loads Next Level
    //     LevelLoader.Current.ChangeLevel("Level " + PlayerPrefs.GetInt("SavedLeved")); //SavedLeved pref'i güncellendiydi zaten.
    // }
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
        SoundManager.instance.Play("LevelWin Sound");
    }
    public bool isWin()
    {                    //To check is the game successfully finished.
        return _isWin;
    }
    public void EndLevel()
    {
        SoundManager.instance.Stop("LevelWin Sound");
        SoundManager.instance.Play("Game Win Money Collect");
    }
    public void NextLevel()
    {
        PlayerPrefs.SetFloat("MoneyAmount", _money);
        PlayerPrefs.SetFloat("PassengerParameter", _passengerIncreaseRate);
        PlayerPrefs.SetFloat("MoneyParameter", _moneyIncreaseRate);
        PlayerPrefs.SetInt("SavedLeved", _savedLevel + 1);

        LevelLoader.Current.ChangeLevel("Level " + PlayerPrefs.GetInt("SavedLeved")); //SavedLeved pref'i güncellendiydi zaten.
    }
    public void LoseLevel()
    {               //Call this when player can't finish the game successfully.
        if (!_isWin)
        {
            gameOverCanvas.gameObject.SetActive(true);
            SoundManager.instance.Play("Game Over");
            _isLose = true;
        }
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
