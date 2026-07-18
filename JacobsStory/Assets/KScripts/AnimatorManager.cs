using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Reflection;
public class AnimatorManager : MonoBehaviour
{
    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");


    }
    public void UpdateAnimatorValues(float horizontalMovement, float verticalMovement)
    {
        //ANimation snapping
        //float snappedHorizontal;
       // float snappedVertical;
        //kind of unecessary but i wanted to see if including it would fix some of the rrors im getting

        // #region Snapped Horizontal
        // if(horizontal > 0 && horizontalMovement < 0.55f)
        // {
        //     snappedHorizontal = 0.5f;
        // }

        // else if (horizontalMovement > 0.55f)
        // {
        //     snappedHorizontal = 1;
        // }

        // else if(horizontalMovement < 0 && horizontalMovement > -0.55f)
        // {
        //     snappedHorizontal = 0.55f;
        // }

        // else if (horizontalMovement < 0.55f)
        // {
        //     snappedHorizontal = -1;
        // }
        // else{ snappedHorizontal = 0;}
        // #endregion
        // #region Snapped Vertical
        // if(vertical > 0 && verticalMovement < 0.55f)
        // {
        //     snappedVertical = 0.5f;
        // }

        // else if (verticalMovement > 0.55f)
        // {
        //     snappedVertical = 1;
        // }

        // else if(verticalMovement < 0 && verticalMovement > -0.55f)
        // {
        //     snappedVertical = 0.55f;
        // }

        // else if (verticalMovement < 0.55f)
        // {
        //     snappedVertical = -1;
        // }
        // else{ snappedVertical = 0;}

        //#endregion
        //animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
       // animator.SetFloat(vertical, snappedVertical, 0.1f, Time.deltaTime);
        animator.SetFloat(horizontal, horizontalMovement, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, verticalMovement, 0.1f, Time.deltaTime);
    }
}
