using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    GameObject helpUI;

    [SerializeField]
    GameObject mainMenuUI;

    [SerializeField]
    GameObject gameUI;

    [SerializeField]
    GameObject winUI;

    [SerializeField]
    TextMeshProUGUI winLoseBox;

    [SerializeField] Player player;
    [SerializeField] Wolf wolf;
    [SerializeField] GameObject gameObjects;
    [SerializeField] TextMeshProUGUI leaveText;

    private bool leaveMenu;


    enum GameMode
    {
        InGame,
        MainMenu,
        Help,
        Win

    }

    GameMode gameMode = GameMode.MainMenu;

    // Start is called before the first frame update
    void Start()
    {
        StartMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameMode)
        {
            case GameMode.MainMenu: UpdateMainMenu(); break;
            case GameMode.Help: UpdateHelpMenu(); break;
            case GameMode.InGame: UpdateMainGame(); break;
            case GameMode.Win: UpdateWinMenu(); break;
        }
    }

    void UpdateWinMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("ForestMazeMap");
        }
    }

    void UpdateMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            StartHelpUI();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene("RockThrowTest");
        }
    }

    void UpdateHelpMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StartMainMenu();
        }
    }

    void UpdateMainGame()
    {
        if (player.gameEnd == true) //if player called game end then WIN
        {
            StartWinUI(true);
        }
        if (wolf.gameEnd == true) //if wolf called game end then LOSE
        {
            StartWinUI(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            leaveText.gameObject.SetActive(true);
            leaveMenu = true;
        }

        if (leaveMenu == true)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                leaveText.gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                SceneManager.LoadScene("ForestMazeMap");
            }
        }
    }


    void StartMainMenu()
    {
        gameMode = GameMode.MainMenu;
        mainMenuUI.gameObject.SetActive(true);
        helpUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        gameObjects.gameObject.SetActive(false);
    }


    void StartHelpUI()
    {
        gameMode = GameMode.Help;
        mainMenuUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
        helpUI.gameObject.SetActive(true);
        gameUI.gameObject.SetActive(false);
    }

    void StartGame()
    {
        gameMode = GameMode.InGame;
        mainMenuUI.gameObject.SetActive(false);
        helpUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(false);
        leaveText.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(true);
        gameObjects.gameObject.SetActive(true);
        
    }

    void StartWinUI(bool win)
    {
        gameObjects.SetActive(false);
        if (!win)
        {
            winLoseBox.text = "THE WOLF CAUGHT YOU";
        }
        else
        {
            winLoseBox.text = "YOU SAVED GRANNY";
        }
        gameMode = GameMode.Win;
        mainMenuUI.gameObject.SetActive(false);
        helpUI.gameObject.SetActive(false);
        gameUI.gameObject.SetActive(false);
        winUI.gameObject.SetActive(true);
        gameObjects.gameObject.SetActive(false);
        player.gameEnd = false;
        wolf.gameEnd = false;
    }
}
