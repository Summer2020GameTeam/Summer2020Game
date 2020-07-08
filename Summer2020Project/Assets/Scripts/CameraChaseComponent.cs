using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaseComponent : MonoBehaviour
{
    [SerializeField]
    private Transform Target;
    [SerializeField]
    private bool DoesLerp;
    [SerializeField]
    private float LerpValue = .2f;
    [SerializeField]
    private Vector3 PositionOffset;

    private void LateUpdate()
    {
        MoveCamToPosition();
    }

    private void MoveCamToPosition()
    {
        if (DoesLerp)
        {;
            LerpCamPosition();
        }
        else
        {
            transform.position = Target.position + PositionOffset;
        }
    }

    void LerpCamPosition()
    {
            Vector3 newPosition = Vector3.Lerp(transform.position, Target.position, LerpValue);
            transform.position = newPosition + PositionOffset;
    }
}
