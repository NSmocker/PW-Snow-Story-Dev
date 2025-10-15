using UnityEngine;

public class MonsterStateController : MonoBehaviour
{
    public enum MonsterGrandState
    {
        Peace,
        Battle
    }

    public MonsterGrandState currentGrandState = MonsterGrandState.Peace;
    public MonsterPeaceState peaceState;
    public MonsterBattleState combatState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        peaceState = GetComponent<MonsterPeaceState>();
        combatState = GetComponent<MonsterBattleState>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGrandState == MonsterGrandState.Peace)
        {
            peaceState.enabled = true;
            combatState.enabled = false;
        }
        else if(currentGrandState == MonsterGrandState.Battle)
        {
            peaceState.enabled = false;
            combatState.enabled = true;
        }
    }
}
