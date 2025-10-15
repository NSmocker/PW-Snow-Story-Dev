using UnityEngine;

public class CityZone : MonoBehaviour
{
  void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamePlayModeManager gamePlayModeManager = other.transform.GetComponent<GamePlayModeManager>();
            if (gamePlayModeManager != null)
            {
                gamePlayModeManager.currentGamePlayMode = GamePlayModeManager.GamePlayModes.City;
                gamePlayModeManager.SwitchGamePlayMode(GamePlayModeManager.GamePlayModes.City);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GamePlayModeManager gamePlayModeManager = other.transform.GetComponent<GamePlayModeManager>();
            if (gamePlayModeManager != null)
            {
                gamePlayModeManager.currentGamePlayMode = GamePlayModeManager.GamePlayModes.Roaming;
                gamePlayModeManager.SwitchGamePlayMode(GamePlayModeManager.GamePlayModes.Roaming);
            }
        }
    }
}
