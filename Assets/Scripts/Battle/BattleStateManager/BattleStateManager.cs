using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateManager : MonoBehaviour
{
    public BattleBaseState currentState;
    public NoBattleState battleless = new NoBattleState();
    public PlayerTurnState pTurn = new PlayerTurnState();
    public EnemyTurnState eTurn = new EnemyTurnState();

    void Start()
    {
        currentState = battleless;
        currentState.EnterState(this);
    }
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(BattleBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

}
