using UnityEngine;

public class LeverScript : Interactable {
    [SerializeField]
    private Animator animator;

    protected override void UpdateVisuals(bool isOn)
    {
        animator.SetBool("PullDown", isOn);
    }

}
