using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    public BattleBaseState currentState;
    public NoBattleState battleless = new NoBattleState();
    public PlayerTurnState pTurn = new PlayerTurnState();
    public EnemyTurnState eTurn = new EnemyTurnState();

    public bool inBattle = false;
    public bool nxtTurn = false;
    public int turnNum = 0;
    public int maxTurn = 11; //Includes player + enemy turn
    void Start()
    {
        currentState = battleless;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
        turnTest();
    }

    public void SwitchState(BattleBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    void turnTest()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            inBattle = !inBattle;
        }
    }

}
