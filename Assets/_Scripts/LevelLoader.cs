using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    private Scene lastLoadedScene;
    private static LevelLoader _current;
    public static LevelLoader Current
    {     //To access LevelLoader, we use LevelLoader.Current
        get
        {
            if (_current == null)
            {
                Debug.LogError("LevelLoader is null");
            }
            return _current;
        }
    }

    private void Awake()
    {
        _current = this;
    }


    void Start()
    {
        ChangeLevel("Level " + PlayerPrefs.GetInt("SavedLeved", 1));
        //Oyun açıldığıda en son kaldığı leveli çalıştıracak. Oyun ilk ama ilk defa çalıştırıldıysa Level 1'i yükleyecek.
    }

    public void ChangeLevel(string sceneName)
    {
        if (PlayerPrefs.GetInt("SavedLeved") == 5)
        {
            PlayerPrefs.SetInt("SavedLeved", 1);
            sceneName = "Level " + PlayerPrefs.GetInt("SavedLeved");
        }
        StartCoroutine(ChangeScene(sceneName));
    }

    IEnumerator ChangeScene(string sceneName)
    {
        //EĞER VARSA AÇIK SAHNEYİ SİLME
        if (lastLoadedScene.IsValid())
        {
            SceneManager.UnloadSceneAsync(lastLoadedScene);
            bool isSceneUnloaded = false;
            while (!isSceneUnloaded)
            {
                isSceneUnloaded = !lastLoadedScene.IsValid();   //sahne valid olduğu sürece isSceneUnloaded false olacak.
                yield return new WaitForEndOfFrame();   //her oyun döngüsünün sonuna kadar bekler. Aksi halde oyun sonsuz döngüye girer.
            }
        }
        //---------------------------------------------------

        //YENİ SAHNEYİ YÜKLEME
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        bool isSceneLoaded = false;
        while (!isSceneLoaded)
        {
            lastLoadedScene = SceneManager.GetSceneByName(sceneName);
            isSceneLoaded = lastLoadedScene != null && lastLoadedScene.isLoaded;
            yield return new WaitForEndOfFrame();   //her oyun döngüsünün sonuna kadar bekler. Aksi halde oyun sonsuz döngüye girer.
        }
        //---------------------------------------------------
    }
}
