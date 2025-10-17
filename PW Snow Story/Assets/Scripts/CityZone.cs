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
                gamePlayModeManager.currentGamePlayMode = GamePlayModes.City;
                gamePlayModeManager.SwitchGamePlayMode(GamePlayModes.City);
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
                gamePlayModeManager.currentGamePlayMode = GamePlayModes.Roaming;
                gamePlayModeManager.SwitchGamePlayMode(GamePlayModes.Roaming);
            }
        }
    }
}
