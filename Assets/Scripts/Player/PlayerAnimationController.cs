using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    [Header(" - Float Params - ")]
    [SerializeField] AnimatorParameters speedParam;
    int speedParamHash;

    [Header(" - Boolen Params - ")]
    [SerializeField] AnimatorParameters crouchParam;
    int crouchParamHash;
    [SerializeField] AnimatorParameters isSprintingParam;
    int isSprintingParamHash;

    [Header(" - Trigger Params - ")]

    [SerializeField] AnimatorParameters cryParam;
    int cryParamHash;
    [SerializeField] AnimatorParameters fallingDownParam;
    int fallingDownParamHash;
    [SerializeField] AnimatorParameters landingParam;
    int landingParamHash;

    int[] triggerParamHashes;

    [Header(" - Clip Name - ")]
    [SerializeField] AnimatorParameters fallOffClipName;
    int fallOffHash;
    [SerializeField] AnimatorParameters jumpClipName;
    int jumpHash;


    private void Awake()
    {
        InitAnimator();
    }


    private void InitAnimator()
    {
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        speedParamHash = Animator.StringToHash(speedParam.ToString());

        isSprintingParamHash = Animator.StringToHash(isSprintingParam.ToString());
        crouchParamHash = Animator.StringToHash(crouchParam.ToString());

        cryParamHash = Animator.StringToHash(cryParam.ToString());
        fallingDownParamHash = Animator.StringToHash(fallingDownParam.ToString());
        landingParamHash = Animator.StringToHash(landingParam.ToString());

        fallOffHash = Animator.StringToHash(fallOffClipName.ToString());
        jumpHash = Animator.StringToHash(jumpClipName.ToString());

        triggerParamHashes = new int[] { cryParamHash, fallingDownParamHash, landingParamHash };    }

    public void PlayAnimClip(AnimatorParameters parameter)
    {
        int hash = 0;

        switch (parameter)
        {
            case AnimatorParameters.FallOff:
                hash = fallOffHash;
                break;
            case AnimatorParameters.Jump:
                hash = jumpHash;
                break;
        }

        animator.Play(hash);
    }

    public void SetBool(AnimatorParameters parameter, bool value)
    {
        switch (parameter)
        {
            case AnimatorParameters.IsSprinting:
                animator.SetBool(isSprintingParamHash, value);
                break;
            case AnimatorParameters.IsCrouching:
                animator.SetBool(crouchParamHash, value);
                break;
            default:
                break;
        }
    }

    public void SetTrigger(AnimatorParameters parameter)
    {
        switch (parameter)
        {
            case AnimatorParameters.Cry:
                animator.SetTrigger(cryParamHash);
                break;
            case AnimatorParameters.FallingDown:
                animator.SetTrigger(fallingDownParamHash);
                break;
            case AnimatorParameters.Landing:
                animator.SetTrigger(landingParamHash);
                break;
            default:
                break;
        }
    }

     public void ResetTriggers()
    {
        foreach (var hash in triggerParamHashes)
        {
            animator.ResetTrigger(hash);
        }
    }

    public void SetFloat(AnimatorParameters parameter, float value)
    {
        switch (parameter)
        {
            case AnimatorParameters.Speed:
                animator.SetFloat(speedParamHash, value);
                break;
            default:
                break;
        }
    }

    public bool GetBool(AnimatorParameters parameter)
    {
        return parameter switch
        {
            AnimatorParameters.IsSprinting => animator.GetBool(isSprintingParamHash),
            AnimatorParameters.IsCrouching => animator.GetBool(crouchParamHash),
            _ => false,
        };
    }
}

