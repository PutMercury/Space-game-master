using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotatingTurrets : MonoBehaviour
{
    Vector3 aimPoint = Vector3.zero;
    [SerializeField] private Transform turretBase = null;
    [SerializeField] private Transform turretRotors = null;
    private float finalRotation = 0f;
    private float finalElevation = 0f;
    private float rotationSpeed = 100f;
    private float elevationSpeed = 100f;
    public Camera shipCam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = ray.GetPoint(100);
        }

        BaseRotating(aimPoint);
        RotorsAiming(aimPoint);
    }

    private void BaseRotating(Vector3 posOfTarget)
    {
        Vector3 zeroRotation = transform.forward;
        Vector3 unflattenedVectorToTarget = posOfTarget - turretBase.position;
        Vector3 flattenedVectorToTarget = Vector3.ProjectOnPlane(unflattenedVectorToTarget, transform.up);

        float targetAngle = Vector3.SignedAngle(zeroRotation, flattenedVectorToTarget, transform.up);

        targetAngle = Mathf.Clamp(targetAngle, -50, 50);

        finalRotation = Mathf.MoveTowards(finalRotation, targetAngle, rotationSpeed * Time.deltaTime);

        if (Mathf.Abs(finalRotation) > Mathf.Epsilon)
            turretBase.localEulerAngles = Vector3.up * finalRotation;


    }

    private void RotorsAiming(Vector3 posOfTarget)
    {
        Vector3 poitionOfTargetLocal = turretBase.InverseTransformDirection(posOfTarget - turretRotors.position);
        Vector3 flattenedVectorToTarget = Vector3.ProjectOnPlane(poitionOfTargetLocal, Vector3.up);

        float targetElevation = Vector3.Angle(flattenedVectorToTarget, poitionOfTargetLocal);
        targetElevation = targetElevation * Mathf.Sign(poitionOfTargetLocal.y);

        targetElevation = Mathf.Clamp(targetElevation, -40, 40);

        finalElevation = Mathf.MoveTowards(finalElevation, targetElevation, elevationSpeed * Time.deltaTime);

        if (Mathf.Abs(finalElevation) > Mathf.Epsilon)
            turretRotors.localEulerAngles = Vector3.right * -finalElevation;

    }


}
