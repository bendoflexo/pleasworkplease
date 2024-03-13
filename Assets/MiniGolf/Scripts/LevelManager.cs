using UnityEngine;
using Cinemachine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject ballPrefab;           
    public Vector3 ballSpawnPos;          

    public LevelData[] levelDatas;

    [SerializeField] public CinemachineVirtualCamera vcam;
    [SerializeField] public GameObject cameraPrefab;

    private Rigidbody ballWork;

    private int shotCount = 0;          

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
    }

    private void Start()
    {
        var vcam = GetComponent<CinemachineVirtualCamera>();
        ballWork = ballPrefab.GetComponent<Rigidbody>();
    }

    public void SpawnLevel(int levelIndex)
    {
         if (levelIndex < 0 || levelIndex >= levelDatas.Length)
    {
        Debug.LogError("Level index is out of bounds: " + levelIndex);
        UIManager.instance.ShowMainMenu(); 
        return;
    }
        GameManager.singleton.currentLevelIndex = levelIndex;
        levelDatas[levelIndex].levelPrefab.SetActive(true);
        Instantiate(levelDatas[levelIndex].levelPrefab, Vector3.zero, Quaternion.identity);
        shotCount = levelDatas[levelIndex].shotCount;                                  
        UIManager.instance.ShotText.text = shotCount.ToString();

        ballPrefab.SetActive(true);
        DontDestroyOnLoad(ballPrefab);
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(cameraPrefab);
        vcam.LookAt = ballPrefab.transform; 
     // ballPrefab = GameObject.FindGameObjectWithTag("Player").GetComponent<GameObject>();
        
                                                                   
      //GameObject ball = Instantiate(ballPrefab, ballSpawnPos, Quaternion.identity);
      //CameraFollow.instance.SetTarget(ballPrefab);                     
        GameManager.singleton.gameStatus = GameStatus.Playing;      
    }

    public void ShotTaken()
    {
        if (shotCount > 0)                                       
        {
            shotCount--;                                            
            UIManager.instance.ShotText.text = "" + shotCount;     

            if (shotCount <= 0)                                    
            {
                LevelFailed();                                         
            }
        }
    }

    public void LevelFailed()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing) 
        {
            GameManager.singleton.gameStatus = GameStatus.Failed;

            ballPrefab.transform.position = ballSpawnPos; 
              
            UIManager.instance.GameResult();  
            vcam.Follow = ballPrefab.transform;   
            ballWork.velocity = Vector3.zero;         
        }
    }

    public void LevelComplete()
    {
        if (GameManager.singleton.gameStatus == GameStatus.Playing) 
        {    
            if (GameManager.singleton.currentLevelIndex < levelDatas.Length)    
            {
                GameManager.singleton.currentLevelIndex++; 
                
                ballPrefab.transform.position = ballSpawnPos;
                vcam.Follow = ballPrefab.transform; 
            }
            else
            {
 
                GameManager.singleton.currentLevelIndex = 0;
            }
            GameManager.singleton.gameStatus = GameStatus.Complete; 
            UIManager.instance.GameResult();                        
        }
    }
    
}
