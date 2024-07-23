using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [field:SerializeField] public PlayerInputManager InputManager { get; private set; }
    [field:SerializeField] public PlayerLocomotion Locomotion { get; private set; }
    [field:SerializeField] public PlayerAnimationController AnimationController { get; private set; }
    [field:SerializeField] public PlayerInteractions Interactions { get; private set; }
    [field:SerializeField] public PlayerEmotion Emotion { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
