using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IMovement
{
    public void StartMove(Vector2 velocity);
    public void StopMove();
    public bool IsMoving { get; }
    public Vector2 Velocity { get; }
    public UnityEvent StartMoveEvent { get; }
    public UnityEvent StopMoveEvent { get; }
}
