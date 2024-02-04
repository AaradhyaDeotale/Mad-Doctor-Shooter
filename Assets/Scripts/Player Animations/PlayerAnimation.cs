using System.Collections;
using UnityEngine;
using System.Collections.Generic;


public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private Vector3 tempScale;
    private int currentAnimation;
    private bool isAttackAnimationPlaying;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation(string animationName)
    {
        if (currentAnimation == Animator.StringToHash(animationName))
            return;

        anim.Play(animationName);
        currentAnimation = Animator.StringToHash(animationName);
    }

    public void SetFacingDirection(bool faceRight)
    {
        if (faceRight)
            tempScale.x = 1f;
        else
            tempScale.x = -1f;

        transform.localScale = tempScale;
    }

    public void PlayAttackAnimation()
    {
        Debug.Log("Playing Attack Animation");
        isAttackAnimationPlaying = true;
        StartCoroutine(PlayAttackAnimationCoroutine());
    }

    private IEnumerator PlayAttackAnimationCoroutine()
    {
        Debug.Log("Start Attack Animation");
        anim.Play(TagManager.ATTACK_ANIMATION_NAME);
        yield return new WaitForSeconds(anim.GetCurrentAnimatorClipInfo(0).Length);
        isAttackAnimationPlaying = false;
        anim.Play(TagManager.IDLE_ANIMATION_NAME);
        Debug.Log("End Attack Animation");
    }

    public bool IsAttackAnimationPlaying()
    {
        return isAttackAnimationPlaying;
    }

}



