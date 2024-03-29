﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Image powerBar;       
    [SerializeField] private Text shotText;        
    [SerializeField] private GameObject mainMenu, gameMenu, gameOverPanel, retryBtn, nextBtn;   
    [SerializeField] private GameObject lvlBtnPrefab;    

    public GameObject[] levelButtons;

    public Text ShotText { get { return shotText; } }   
    public Image PowerBar { get => powerBar; }          

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        powerBar.fillAmount = 0;                        
    }

    void Start()
    {
        if (GameManager.singleton.gameStatus == GameStatus.None)   
        {   
            CreateLevelButtons();                     
        }     
        else if (GameManager.singleton.gameStatus == GameStatus.Failed ||
            GameManager.singleton.gameStatus == GameStatus.Complete)
        {
            mainMenu.SetActive(false);                                     
            gameMenu.SetActive(true);                                       
            LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);  
        }
    }


    void CreateLevelButtons()
    {
        for (int i = 0; i < LevelManager.instance.levelDatas.Length; i++)
        {
           // GameObject buttonObj = Instantiate(container);   
           foreach(GameObject button in levelButtons)
           {
                button.SetActive(true);
           } 
          //  buttonObj.transform.GetChild(0).GetComponent<Text>().text = "" + (i + 1);   
          //  Button button = buttonObj.GetComponent<Button>();                           
          //  button.onClick.AddListener(() => OnClick(button));                          
        }
    }


   /* void OnClick(Button btn)
    {
        mainMenu.SetActive(false);                                                      
        gameMenu.SetActive(true);                                                       
        GameManager.singleton.currentLevelIndex = btn.transform.GetSiblingIndex();    
        LevelManager.instance.SpawnLevel(GameManager.singleton.currentLevelIndex);      
    }
    */
    
    

    public void GameResult()
    {
        switch (GameManager.singleton.gameStatus)
        {
            case GameStatus.Complete:                      
                gameOverPanel.SetActive(true);             
                nextBtn.SetActive(true);                   
                SoundManager.instance.PlayFx(FxTypes.GAMECOMPLETEFX);
                break;
            case GameStatus.Failed:                        
                gameOverPanel.SetActive(true);              
                retryBtn.SetActive(true);                  
                SoundManager.instance.PlayFx(FxTypes.GAMEOVERFX);
                break;
        }
    }

  
    public void HomeBtn()
    {
        GameManager.singleton.gameStatus = GameStatus.None;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

  
    public void NextRetryBtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ShowMainMenu()
    {
    mainMenu.SetActive(true);
    gameMenu.SetActive(false); 
    gameOverPanel.SetActive(false); 
    CreateLevelButtons(); 
    }

}
