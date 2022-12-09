using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Button restartButton;
    [SerializeField] private AudioSource inGameMusic;
    private int _money, _health, _totalHealth;
    private static GameManager _instance;   //Create instance and make it static to be sure that only one instance exist in scene.
    private bool _isGameOver = false;
    public static GameManager Instance{     //To access GameManager, we use GameManager.Instance
        get{
            if(_instance == null){
                Debug.LogError("Game Manager is null");
            }
            return _instance;
        }
    }
    public int Money{       //Money property. You can get money from outside this script, but you can only set in this script.
        get => _money;
        private set => _money = value;
    }
    public int Health{       //Health property. You can get health from outside this script, but you can only set in this script.
        get => _health;
        private set => _health = value;
    }
    public int TotalHealth{       //Health property. You can get health from outside this script, but you can only set in this script.
        get => _totalHealth;
        private set => _totalHealth = value;
    }
    private void Awake() {
        _instance = this;
    }
    private void Start() {
        inGameMusic.loop = true;    //Make inGameMusic loop
        inGameMusic.Play();         //Plays inGameMusic
    }
    public void GameOver(bool flag){    //Sets the game over situation. 
        _isGameOver = flag;             //Ex. usage GameManager.Instance.Gameover(true) inside of obstacle script.
    }
    public bool isGameOver(){   //To check is game over
        return _isGameOver;
    }
    public void OnRestartButtonClicked(){   //Restart Button Clicked
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel(){    //Loads Next Level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void UpdateMoney(int updateAmount){    //To update money.Use positive value to increment. Use negative value to decrement.
        _money += updateAmount;
        if(_money < 0){         //To make sure that money is above 0.
            _money = 0;
        }
    }
    public void WinLevel(){       //Call this function when player finish the level successfully.
        //Do Something
        LoadNextLevel();
    }
    public void LoseLevel(){    //Call this when player can't finish the game successfully.
        //Do Something
    }
    public void UpdateBusHealth(int updateAmount){   //To update health.Use positive value to increment. Use negative value to decrement.
        _health += updateAmount;
        if(_health < 0){                //To make sure that health is above 0.
            _health = 0;
        }
        if(_health > _totalHealth){     //To make sure that health is less or equal to total health.
            _health = _totalHealth;
        }
    }
}
