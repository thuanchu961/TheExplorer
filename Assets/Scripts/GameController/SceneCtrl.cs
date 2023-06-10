using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneCtrl : Singleton<SceneCtrl>
{
    [SerializeField] GameObject sceneLoad, menuInGame;
    public GameObject _sceneLoad => sceneLoad;
    [SerializeField]
    bool sceneStart = false, scene1 = false, scene2 = false, scene3 = false,
     scene4 = false/*, scene5 = false*/, GameOver = false, paused = true;
    public bool _paused => paused;
    [SerializeField] float timeLoad, lastTimeLoad = 0;
    [SerializeField] float timePreLoad, lastTimePreLoad = 0;
    [SerializeField] float timePreviousScene, lastTimePreviousScene = 0;
    [SerializeField] GameObject weapon;
    [SerializeField] Vector2 oldPosPlayer;
    int isPreviousScene;
    bool run = false;
    Scene sceneTMT;
    private void Awake()
    {
        // timeLoad = 15;
        // timePreLoad = 5;
    }

    private void OnEnable()
    {
        timeLoad = 10;
        timePreLoad = 10;
        timePreviousScene = 10;
    }
    // Start is called before the first frame update
    void Start()
    {
        sceneTMT = SceneManager.GetActiveScene();

        if (sceneTMT.buildIndex == 5)
        {
            if (PlayerPrefs.HasKey("CurrentScene"))
            {
                isPreviousScene = PlayerPrefs.GetInt("CurrentScene");
                StartCoroutine(CallPreviousScene(isPreviousScene));
            }
        }

        if (sceneTMT.buildIndex == 0)
        {
            DelAllPlayerPrefs();
            return;
        }

        TMT_ActiveSceneLoad();

        if (sceneTMT.buildIndex == 0 || sceneTMT.buildIndex == 5 || sceneTMT.buildIndex == 6)
            return;

        oldPosPlayer = PlayerCtrl._inst_singleton.transform.position;
        if (PlayerPrefs.HasKey("CurrentScene"))
        {
            PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
            isPreviousScene = PlayerPrefs.GetInt("CurrentScene");
        }
        else
        {
            PlayerPrefs.SetInt("CurrentScene", SceneManager.GetActiveScene().buildIndex);
            isPreviousScene = PlayerPrefs.GetInt("CurrentScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
        lastTimeLoad = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimeLoad);
        lastTimePreLoad = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimePreLoad);

        if (run)
            lastTimePreviousScene = CommonFunc._inst_singleton.TMT_DelayTimeCount(lastTimePreviousScene);

        LoadSceneStart();
        LoadSceneGame1();
        LoadSceneGame2();
        LoadSceneGame3();
        LoadSceneGame4();
        // LoadSceneGame5();
        LoadSceneGameOver();

        if (sceneLoad.activeSelf)
            return;

        if (sceneTMT.buildIndex == 0 || sceneTMT.buildIndex == 6)
            Cursor.visible = true;
        else
        {
            if (sceneTMT.buildIndex != 5)
                if (menuInGame.activeSelf)
                    Cursor.visible = true;
                else
                    Cursor.visible = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (sceneTMT.buildIndex == 0)
                return;

            TMT_PauseAndContinueGame();
        }

    }

    public void TMT_PauseAndContinueGame()
    {
        if (paused)
        {
            Time.timeScale = 0;
            paused = false;
            menuInGame.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            paused = true;
            menuInGame.SetActive(false);
        }
    }

    public void TMT_CallLoadScene(int index)
    {
        if (index != 5)
            sceneLoad.SetActive(true);
        StartCoroutine(CallPreLoadScene(index));
    }

    public void TMT_ActiveSceneLoad()
    {
        sceneLoad.SetActive(true);
        StartCoroutine(DeSceneLoad());
    }

    IEnumerator DeSceneLoad()
    {
        lastTimeLoad = timeLoad;
        yield return new WaitUntil(() => lastTimeLoad <= 0);
        sceneLoad.SetActive(false);
    }

    public void TMT_ExitGame()
    {
        DelAllPlayerPrefs();
        Application.Quit();
    }

    void DelAllPlayerPrefs()
    {
        string[] keyPrefs = { "staffState", "Key1", "Key2", "Key3", "enableCam", "boxKey", "boxHealth",
        "eventHealth", "CurrentScene" };
        for (int i = 0; i < keyPrefs.Length; i++)
        {
            PlayerPrefs.DeleteKey(keyPrefs[i]);
        }
    }

    public void TMT_PlayerIntoAcid()
    {
        PlayerCtrl._inst_singleton.transform.position = oldPosPlayer;
    }

    IEnumerator CallPreLoadScene(int index)
    {
        if (index == 5)
            lastTimePreLoad = 15;
        else
            lastTimePreLoad = timePreLoad;
        yield return new WaitUntil(() => lastTimePreLoad <= 0);
        if (index == 0)
            sceneStart = true;
        else if (index == 1)
            scene1 = true;
        else if (index == 2)
            scene2 = true;
        else if (index == 3)
            scene3 = true;
        else if (index == 4)
            scene4 = true;
        // else if (index == 5)
        //     scene5 = true;
        else if (index == 5)
            GameOver = true;
    }

    IEnumerator LoadSceneGame(int index)
    {
        SceneManager.LoadScene(index);
        yield return null;
    }

    void LoadSceneStart()
    {
        if (sceneStart)
        {
            sceneStart = false;
            StartCoroutine(LoadSceneGame(0));
        }
    }

    void LoadSceneGame1()
    {
        if (scene1)
        {
            scene1 = false;
            StartCoroutine(LoadSceneGame(1));
        }
    }

    void LoadSceneGame2()
    {
        if (scene2)
        {
            scene2 = false;
            StartCoroutine(LoadSceneGame(2));
        }
    }

    void LoadSceneGame3()
    {
        if (scene3)
        {
            scene3 = false;
            StartCoroutine(LoadSceneGame(3));
        }
    }

    void LoadSceneGame4()
    {
        if (scene4)
        {
            scene4 = false;
            StartCoroutine(LoadSceneGame(4));
        }
    }

    // void LoadSceneGame5()
    // {
    //     if (scene5)
    //     {
    //         scene5 = false;
    //         StartCoroutine(LoadSceneGame(5));
    //     }
    // }

    void LoadSceneGameOver()
    {
        if (GameOver)
        {
            GameOver = false;
            StartCoroutine(LoadSceneGame(5));
        }
    }

    IEnumerator CallPreviousScene(int previousScene)
    {
        run = true;
        lastTimePreviousScene = timePreviousScene;
        yield return new WaitUntil(() => lastTimePreviousScene <= 0);
        TMT_CallLoadScene(previousScene);
        run = false;
    }
}
