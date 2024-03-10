using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxSource;                            
    public AudioClip gameOverFx, gameCompleteFx, shotFx;    

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


    public void PlayFx(FxTypes fxTypes)
    {
        switch (fxTypes)                                
        {
            case FxTypes.GAMEOVERFX:                    
                fxSource.PlayOneShot(gameOverFx);       
                break;
            case FxTypes.GAMECOMPLETEFX:               
                fxSource.PlayOneShot(gameCompleteFx);   
                break;
            case FxTypes.SHOTFX:
                fxSource.PlayOneShot(shotFx);
                break;
        }

    }
}

public enum FxTypes
{
    GAMEOVERFX, 
    GAMECOMPLETEFX, 
    SHOTFX
}
