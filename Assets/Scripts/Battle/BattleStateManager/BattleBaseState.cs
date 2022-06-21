using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BattleBaseState
{
    public abstract void EnterState(BattleStateManager battle, DBManager dialbtn);
    public abstract void UpdateState(BattleStateManager battle, DBManager dialbtn);

}
