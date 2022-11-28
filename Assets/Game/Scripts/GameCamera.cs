using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public float Pitch;
    public float Zoom;
    public float Yaw;
    public float Height;
    public Transform Target;

    private void LateUpdate()
    {
        UpdatePosition();
    }
    private void OnValidate()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        var targetPos = Target.position;
        transform.position = targetPos + Quaternion.Euler(Pitch, Yaw, 0) * new Vector3(0, Height, -Zoom);
    }
}