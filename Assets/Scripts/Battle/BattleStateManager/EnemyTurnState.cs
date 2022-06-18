using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnState : BattleBaseState
{
    BattleStateManager battle;
    public float timer = 3f;
    public override void EnterState(BattleStateManager battle)
    {

    }
    public override void UpdateState(BattleStateManager battle)
    {
        if (battle.turnNum == battle.maxTurn)
        {
          //  battle.inBattle = false;
            battle.SwitchState(battle.battleless);
        }
        else if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Debug.Log("Enemy choice");
            battle.SwitchState(battle.pTurn);
            timer = 3f;
        }

    }
}
