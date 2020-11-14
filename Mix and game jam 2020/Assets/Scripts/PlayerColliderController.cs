using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    public int colliderIndex = 0;

    private void OnTriggerStay2D(Collider2D otherCollision)
    {
        if(otherCollision.gameObject.tag == "StopMovement")
        {
            if(colliderIndex == 0)
                GetComponentInParent<PlayerController>().isCollidingLeftTop = true;
            if (colliderIndex == 1)
                GetComponentInParent<PlayerController>().isCollidingLeftBottom = true;
            if (colliderIndex == 2)
                GetComponentInParent<PlayerController>().isCollidingRightTop = true;
            if (colliderIndex == 3)
                GetComponentInParent<PlayerController>().isCollidingRightBottom = true;
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollision)
    {
        if (otherCollision.gameObject.tag == "StopMovement")
        {
            if (colliderIndex == 0)
                GetComponentInParent<PlayerController>().isCollidingLeftTop = false;
            if (colliderIndex == 1)
                GetComponentInParent<PlayerController>().isCollidingLeftBottom = false;
            if (colliderIndex == 2)
                GetComponentInParent<PlayerController>().isCollidingRightTop = false;
            if (colliderIndex == 3)
                GetComponentInParent<PlayerController>().isCollidingRightBottom = false;
        }
    }
}
