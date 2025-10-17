
using UnityEngine;
using Unity.Cinemachine;
public enum GamePlayModes
    {
        Roaming,
        City
    } 
public class GamePlayModeManager : MonoBehaviour
{

    public GamePlayModes currentGamePlayMode = GamePlayModes.Roaming;

    [Header("Cameras")]
    public CinemachineCamera roamingCamera, cityCamera;
    [Tooltip("Priority for the active camera")]
    public int activeCameraPriority = 10;
    [Tooltip("Priority for inactive cameras")]
    public int inactiveCameraPriority = 0;

    [Header("Animation")]
    public RuntimeAnimatorController roamingAnimatorController, cityAnimatorController;

    private CinemachineCamera currentActiveCamera;
    public RuntimeAnimatorController currentActiveAnimatorController;
    public Animator playerAnimator;

    private GamePlayModes lastGamePlayMode; // кеш останнього значення для виявлення змін
   
    

    public void ApplyModeSettings()
    {
        // Apply animation changes
        if (playerAnimator != null && currentActiveAnimatorController != null)
            playerAnimator.runtimeAnimatorController = currentActiveAnimatorController;

        // Update camera priorities using PrioritySettings
        if (roamingCamera != null)
        {
            var prioritySettings = roamingCamera.Priority;
            prioritySettings.Value = (currentActiveCamera == roamingCamera) ? activeCameraPriority : inactiveCameraPriority;
            roamingCamera.Priority = prioritySettings;
        }
        
        if (cityCamera != null)
        {
            var prioritySettings = cityCamera.Priority;
            prioritySettings.Value = (currentActiveCamera == cityCamera) ? activeCameraPriority : inactiveCameraPriority;
            cityCamera.Priority = prioritySettings;
        }
    }
    public void SwitchGamePlayMode(GamePlayModes newMode)
    {
       
        switch (currentGamePlayMode)
        {
            case GamePlayModes.Roaming:
                currentActiveCamera = roamingCamera;
                currentActiveAnimatorController = roamingAnimatorController;

                break;
            case GamePlayModes.City:
                currentActiveCamera = cityCamera;
                currentActiveAnimatorController = cityAnimatorController;

                break;
            default:
                Debug.LogError("Unrecognized GamePlayMode: " + currentGamePlayMode);
                break;
        }
        ApplyModeSettings();
        
    }

    public void RoamingBihaviourUpdate()
    {

    }

    public void CityBihaviourUpdate()
    {

    }

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        switch (currentGamePlayMode)
        {
            case GamePlayModes.Roaming:
                currentActiveAnimatorController = roamingAnimatorController;
                currentActiveCamera = roamingCamera;

                break;
            case GamePlayModes.City:
                currentActiveAnimatorController = cityAnimatorController;
                currentActiveCamera = cityCamera;

                break;
            default:
                Debug.LogError("Unrecognized GamePlayMode: " + currentGamePlayMode);
                break;
        }
        ApplyModeSettings();

        // Ініціалізуємо кешоване значення
        lastGamePlayMode = currentGamePlayMode;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        if (currentGamePlayMode != lastGamePlayMode)
        {
            SwitchGamePlayMode(currentGamePlayMode);
            print("Switched to GamePlayMode: " + currentGamePlayMode);
            lastGamePlayMode = currentGamePlayMode;
        }
    }
}
