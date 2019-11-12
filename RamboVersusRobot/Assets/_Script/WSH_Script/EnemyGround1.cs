using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGround1 : EnemyBase
{

    
    protected override void AwakenBehaviour()
    {

        HoriMoveToPosition(player, speed);

    }



}
