using UnityEngine;

public class MagnetMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D _rigidbody;
    [SerializeField] protected Transform spriteToFlip;
    protected Vector2 targetPosition;
    protected bool hasMagnetTarget; 
    protected float moveSpeedWithModifiers;
    public float moveSpeed;
    public bool canMove;

    void OnEnable()
    {
        canMove = true;
        UpdateMoveSpeedWithModifiers();
    }

    protected virtual void UpdateMoveSpeedWithModifiers()
    {
        moveSpeedWithModifiers = moveSpeed;
    }

    private void FixedUpdate()
    {
        MoveTowardsMagnetTarget();
    }

    private void MoveTowardsMagnetTarget()
    {
        if (!hasMagnetTarget)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }
        
        if (!canMove)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }

        if (PauseResumeTimeManager.instance.isPaused)
        {
            _rigidbody.linearVelocity = Vector2.zero;
            return;
        }
            
        Vector2 thisPosition = transform.position;
        Vector2 targetDirection = (targetPosition - thisPosition).normalized; 

        _rigidbody.MovePosition(thisPosition + (targetDirection * moveSpeedWithModifiers * Time.fixedDeltaTime));
        FlipIfFacingTheWrongDirection(targetDirection.x);
    }

    public void SetMagnetTarget(Vector2 position)
    { 
        targetPosition = position;
        hasMagnetTarget = true; 
    }
    
    private void FlipIfFacingTheWrongDirection(float horizontalMovement)
    {
        if (spriteToFlip == null)
            return;

        bool isNotMovingHorizontally = horizontalMovement == 0;
        if (isNotMovingHorizontally)
            return;

        Vector3 currentScale = spriteToFlip.localScale;
        bool isMovingAndFacingTheSameDirection = (horizontalMovement > 0 && currentScale.x > 0) || (horizontalMovement < 0 && currentScale.x < 0);
        if (isMovingAndFacingTheSameDirection)
            return;

        currentScale.x *= -1;
        spriteToFlip.localScale = currentScale;
    }
}