using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : IMovement
{
    private Rigidbody2D rigidbody2D;

    public bool IsMoving { get; private set; }
    public Vector2 Velocity => rigidbody2D.velocity;

    public UnityEvent StartMoveEvent { get; } = new();
    public UnityEvent StopMoveEvent { get; } = new();

    public Movement(Rigidbody2D rigidbody2D)
    {
        this.rigidbody2D = rigidbody2D;
    }

    public void StartMove(Vector2 velocity)
    {
        rigidbody2D.velocity = velocity;

        IsMoving = true;

        StartMoveEvent.Invoke();
    }

    public void StopMove()
    {
        rigidbody2D.velocity = Vector2.zero;

        IsMoving = false;

        StopMoveEvent.Invoke();
    }
}
