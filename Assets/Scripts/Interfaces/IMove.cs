using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMove
{
    event Action onMoveFinalized;
    public void Move(GridNode node);
    public void Move(GridNode node, float range);
}
