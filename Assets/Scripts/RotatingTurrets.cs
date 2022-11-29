using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RotatingTurrets : MonoBehaviour
{

    Vector3 aimPoint = Vector3.zero;

    [SerializeField] private Transform turretBase = null;
    [SerializeField] private Transform turretRotors = null;
    [SerializeField] public Transform isAimedRayOrigin = null;
    static public Vector3 isAimedPoint;

    private float finalRotation = 0f;
    private float finalElevation = 0f;
    private float rotationSpeed = 50f;
    private float elevationSpeed = 50f;
    public bool drawDebugArcs;
    public float maxY;
    public float maxX;

    public Camera shipCam;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        

        Ray aimRay = new Ray(isAimedRayOrigin.position, isAimedRayOrigin.forward);
        Debug.DrawRay(isAimedRayOrigin.position, (isAimedRayOrigin.forward * 100f), Color.magenta);

        RaycastHit isAimedHit;

        if (Physics.Raycast(aimRay, out isAimedHit))
        {
            isAimedPoint = isAimedHit.point;
        }
        else
        {
            isAimedPoint = aimRay.GetPoint(100f);
        }

        Ray ray = shipCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            aimPoint = hit.point;
            //Debug.Log("Rotating turrets aimpoint hit" + aimPoint);
        }
        else
        {
            aimPoint = ray.GetPoint(100);
            //Debug.Log("Rotating turrets aimpoint 100" + aimPoint);
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

        targetAngle = Mathf.Clamp(targetAngle, -maxX, maxX);

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

        targetElevation = Mathf.Clamp(targetElevation, -maxY, maxY);

        finalElevation = Mathf.MoveTowards(finalElevation, targetElevation, elevationSpeed * Time.deltaTime);

        if (Mathf.Abs(finalElevation) > Mathf.Epsilon)
            turretRotors.localEulerAngles = Vector3.right * -finalElevation;

    }
    /*private void OnDrawGizmosSelected()
    {
        if (!drawDebugArcs)
            return;

        if (turretBase != null)
        {
            const float kArcSize = 10f;
            Color colorTraverse = new Color(1f, .5f, .5f, .1f);
            Color colorElevation = new Color(.5f, 1f, .5f, .1f);
            Color colorDepression = new Color(.5f, .5f, 1f, .1f);

            Transform arcRoot = turretRotors != null ? turretRotors : turretBase;

            // Red traverse arc
            UnityEditor.Handles.color = colorTraverse;
            
            
            UnityEditor.Handles.DrawSolidArc(arcRoot.position, turretBase.up,transform.forward, maxX, kArcSize);
            UnityEditor.Handles.DrawSolidArc(arcRoot.position, turretBase.up,transform.forward, -maxX, kArcSize);
            

            

            if (turretRotors != null)
            {
                // Green elevation arc
                UnityEditor.Handles.color = colorElevation;
                UnityEditor.Handles.DrawSolidArc(turretRotors.position, turretRotors.right,turretBase.forward, -maxY, kArcSize);

                // Blue depression arc
                UnityEditor.Handles.color = colorDepression;
                UnityEditor.Handles.DrawSolidArc(
                    turretRotors.position, turretRotors.right,
                    turretBase.forward, maxY,
                    kArcSize);
            }
        }
    }*/

}
