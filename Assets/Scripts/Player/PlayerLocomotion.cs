using System.Collections;
using UnityEngine;


public class PlayerLocomotion : MonoBehaviour
{
    float currentSpeed;
    Vector2 tempPos;
    public void SetPos(Vector2 pos) => tempPos = pos;

    [Header(" - On Feet - ")]
    [SerializeField] float walkSpeed;

    [Header(" - Sprint - ")]
    [SerializeField] float sprintSpeed;
    [SerializeField] GameObject skateBoard;

    bool isSprinting;

    [Header(" - Jump - ")]
    [SerializeField] float jumpHeight;
    [SerializeField] float timeToReachTop;
    [SerializeField] float onAirSpeed;

    [SerializeField] float jumpBufferingTime;
    float jumpBufferTimer;

    [SerializeField] float coyoteTime = .2f;
    float coyoteTimer;

    bool isOnAir;
    bool isFalling;
    bool isLanding;

    [Header(" - Crouch - ")]
    [SerializeField] float crouchSpeed;
    [SerializeField] ColliderPresets crouchCollider;

    bool hasColliderAdjusted;
    bool isCrouching;

    [Header(" - Floating Capsule - ")]
    [SerializeField] float desireDistanceFromGround;
    [SerializeField] float maxDot;
    [SerializeField] float minDot;

    RaycastHit2D downRayHit;
    [SerializeField] Transform feetPos;
    [SerializeField] Transform headPos;
    [SerializeField] float verticalRayLength;

    [Header(" - Cam Target - ")]
    [SerializeField] Transform camTarget;
    [SerializeField] float camXPos;

    CapsuleCollider2D playerCollider;
    ColliderPresets defaultColliderPreset;

    PlayerManager playerManager;

    [ContextMenu("TEST_Method")]
    public void TEST_Method()
    {

    }


    private void Awake()
    {
        InitCollider();
        tempPos = transform.position;
    }
    private void Start()
    {
        playerManager = PlayerManager.Instance;

        ShootRay();
        tempPos.y = downRayHit.point.y + desireDistanceFromGround;
    }
    private void OnEnable()
    {
        EventsBroadcaster.OnRetry += StopAllCoroutines;
    }
    private void OnDisable()
    {
        EventsBroadcaster.OnRetry -= StopAllCoroutines;
    }

    private void Update()
    {
        transform.position = tempPos;
        ShootRay();

        if (downRayHit.distance > desireDistanceFromGround)
        {
            coyoteTimer += Time.deltaTime;
            if (coyoteTimer > coyoteTime)
            {
                FallOff(true);
                coyoteTimer = 0;
                print("Fall!!");
            }
        }

        jumpBufferTimer += Time.deltaTime;
    }

    void ShootRay()
    {
        downRayHit = Physics2D.Raycast(transform.position, Vector2.down, 1000, 1 << 6);

        /*
                print(Vector2.Dot(Vector2.right, hit.normal));
                transform.up = hit.normal;*/
    }

    public void SetPosByRay()
    {
        if (UtillityFunctions.IsApproximatelySame(desireDistanceFromGround, downRayHit.distance)) return;
        tempPos.y = downRayHit.point.y + desireDistanceFromGround;
    }

    private void InitCollider()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        defaultColliderPreset = new ColliderPresets(playerCollider.offset, playerCollider.size);
    }


    public void MoveHorizontally(Vector2 direction)
    {
        if (isLanding) return;

        currentSpeed = isSprinting ? (isCrouching ? crouchSpeed : sprintSpeed) : (isCrouching ? crouchSpeed : walkSpeed);

        currentSpeed = isOnAir ? (isSprinting ? onAirSpeed * 2 : onAirSpeed) : currentSpeed;

        if (Physics2D.Raycast(feetPos.position, direction, verticalRayLength, 1 << 6) || Physics2D.Raycast(headPos.position, direction, verticalRayLength, 1 << 6))
        {
            Debug.DrawLine(feetPos.position, feetPos.position + (Vector3)direction * verticalRayLength);
        }
        else
        {
            tempPos += currentSpeed * Time.deltaTime * direction;
        }

        if (direction.x < 0)
        {
            ChangeSprintState(false);
            camTarget.localPosition = new Vector3(-camXPos, 1.5f, 0);
        }
        else
        {
            camTarget.localPosition = new Vector3(camXPos, 1.5f, 0);
        }

        SetSpeedParam(currentSpeed * direction.x);
    }

    public void SetSpeedParam(float speed)
    {
        playerManager.AnimationController.SetFloat(AnimatorParameters.Speed, speed);
    }

    public void Jump(float jumpTimer = 1)
    {
        if (isOnAir)
        {
            jumpBufferTimer = 0;
            return;
        }

        StartCoroutine(CoJump(jumpHeight * jumpTimer, timeToReachTop * jumpTimer));
    }

    IEnumerator CoJump(float jumpHeight, float timeToReachTop)
    {
        playerManager.AnimationController.PlayAnimClip(AnimatorParameters.Jump);
        isFalling = false;
        isOnAir = true;

        float timer = 0;
        float velocity = (2 * jumpHeight) / timeToReachTop;
        float fakeGravity = (-2 * jumpHeight) / (timeToReachTop * timeToReachTop);
        float currentY = tempPos.y;


        while (timeToReachTop > timer)
        {
            timer += Time.deltaTime;
            tempPos.y = fakeGravity * timer * timer / 2 + velocity * timer + currentY;
            yield return null;
        }

        StartCoroutine(CoFallOff(velocity, fakeGravity, currentY));
    }

    public void FallOff(bool fallingFromEdge = false)
    {
        if (isOnAir || isFalling) return;

        coyoteTimer = 0;

        float velocity = (2 * jumpHeight) / timeToReachTop;
        float fakeGravity = (-2 * jumpHeight) / (timeToReachTop * timeToReachTop);
        float currentY = tempPos.y;

        PlayerManager.Instance.AnimationController.ResetTriggers();

        StartCoroutine(CoFallOff(velocity, fakeGravity, currentY, fallingFromEdge));
    }

    IEnumerator CoFallOff(float velocity, float fakeGravity, float currentY, bool fallingFromEdge = false)
    {
        isFalling = false;
        isOnAir = true;

        float timer = timeToReachTop;

        currentY = fallingFromEdge ? currentY - jumpHeight : currentY;

        while (downRayHit.distance > desireDistanceFromGround)
        {
            if (!isFalling)
            {
                playerManager.AnimationController.PlayAnimClip(AnimatorParameters.FallOff);
                isFalling = true;
            }
            timer += Time.deltaTime;

            tempPos.y = fakeGravity * timer * timer / 2 + velocity * timer + currentY;

            yield return null;

        }
        isOnAir = false;
        isFalling = false;
        SetPosByRay();

        if (jumpBufferTimer < jumpBufferingTime)
            Jump();
        else
            playerManager.AnimationController.SetTrigger(AnimatorParameters.Landing);
    }

    public void Crouch(bool isPressing)
    {
        playerManager.AnimationController.SetBool(AnimatorParameters.IsCrouching, isPressing);

        if (isPressing)
        {
            AdjustCollider(crouchCollider);
            ChangeSprintState(false);
            isCrouching = true;
        }
        else
        {
            ResetCollider();
            isCrouching = false;
        }
    }

    public void ChangeSprintState()
    {
        isSprinting = !isSprinting;
        playerManager.AnimationController.SetBool(AnimatorParameters.IsSprinting, !playerManager.AnimationController.GetBool(AnimatorParameters.IsSprinting));
    }
    public void ChangeSprintState(bool isSprinting)
    {
        this.isSprinting = isSprinting;
        playerManager.AnimationController.SetBool(AnimatorParameters.IsSprinting, isSprinting);
    }


    void AdjustCollider(ColliderPresets colliderPreset)
    {
        if (hasColliderAdjusted) return;

        playerCollider.offset = colliderPreset.offset;
        playerCollider.size = colliderPreset.size;

        hasColliderAdjusted = true;
    }

    void ResetCollider()
    {
        if (!hasColliderAdjusted) return;

        playerCollider.offset = defaultColliderPreset.offset;
        playerCollider.size = defaultColliderPreset.size;

        hasColliderAdjusted = false;
    }

    public bool IsOnGround()
    {
        return UtillityFunctions.IsApproximatelySame(desireDistanceFromGround, downRayHit.distance);
    }

    #region For Animation Event

    public void StartLanding()
    {
        isLanding = true;
    }

    public void EndLanding()
    {
        isLanding = false;
    }

    #endregion

}

