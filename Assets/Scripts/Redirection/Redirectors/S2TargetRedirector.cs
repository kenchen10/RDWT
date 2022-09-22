using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redirection;

public class S2TargetRedirector : SteerToRedirector
{
    
    private const float S2T_BEARING_ANGLE_THRESHOLD_IN_DEGREE = 160;

    //active waypoint is replacing generated temp waypoint?? lets try with it still there.
    private const float S2T_TEMP_TARGET_DISTANCE = 4;
    //active waypoint from redirectionManager.cs = public Transform targetWaypoint;

    public override void PickRedirectionTarget()
    {
        Vector3 trackingAreaPosition = Utilities.FlattenedPos3D(redirectionManager.trackedSpace.position);
        Vector3 userToTarget = trackingAreaPosition - redirectionManager.currPos;

        //Compute steering target for S2C
        float bearingToTarget = Vector3.Angle(userToTarget, redirectionManager.currDir);
        float directionToTarget = Utilities.GetSignedAngle(redirectionManager.currDir, userToTarget);
        if (bearingToTarget >= S2T_BEARING_ANGLE_THRESHOLD_IN_DEGREE)
        {
            //Generate temporary target
            if (noTmpTarget)
            {
                tmpTarget = new GameObject("S2T Temp Target");
                tmpTarget.transform.position = redirectionManager.currPos + S2T_TEMP_TARGET_DISTANCE * (Quaternion.Euler(0, directionToTarget * 90, 0) * redirectionManager.currDir);
                tmpTarget.transform.parent = transform;
                noTmpTarget = false;
            }
            currentTarget = tmpTarget.transform;
        }
        else
        {
            currentTarget = redirectionManager.trackedSpace;
            if (!noTmpTarget)
            {
                GameObject.Destroy(tmpTarget);
                noTmpTarget = true;
            }
        }
    }


}
