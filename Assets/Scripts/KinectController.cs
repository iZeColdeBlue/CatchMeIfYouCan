using System.Collections.Generic;
//using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Windows.Kinect;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using Joint = Windows.Kinect.Joint;

public class PlayerMover : MonoBehaviour
{
    public BodySourceManager mBodySourceManager;

    public Text LeanX;
    public Text LeanZ;
    public Text Direction;

    private Dictionary<ulong, GameObject> mBodies = new Dictionary<ulong, GameObject>();
    private List<JointType> mJoints = new List<JointType>
    {
        JointType.SpineBase,
        JointType.Head,
    };

    private Vector3 LeanDirection;
    private Vector3 currentPosition;

    private Vector3 HeadPos;
    private Vector3 HipPos;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        #region Get Kinect Data
        Body[] data = mBodySourceManager.GetData();
        if (data == null)
            return;

        List<ulong> trackedIds = new List<ulong>();
        foreach (var body in data)
        {
            if (body == null)
                continue;

            if (body.IsTracked)
                trackedIds.Add(body.TrackingId);
        }
        #endregion

        foreach(var body in data)
    {
            if (body != null && body.IsTracked)
            {
                UpdateJointPosition(body);
            }
        }

    }

    private void UpdateJointPosition(Body body)
    {
        foreach (JointType mJoints in mJoints)
        {
            Joint sourceJoint = body.Joints[mJoints];
            currentPosition = GetVector3FromJoint(sourceJoint);

            if(mJoints.ToString() == "Head")
            {
                HeadPos = currentPosition;
            } else
            {
                HipPos = currentPosition;
            }

            SetPositionText();

        }

        LeanDirection = new Vector3(HipPos.x - HeadPos.x, HipPos.y - HeadPos.y, HipPos.z - HeadPos.z );

    }

    private static Vector3 GetVector3FromJoint(Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }


    private void SetPositionText()
    {
        LeanX.text = "Head Pos X: " + HeadPos.x + "Hip Pos X: " + HipPos.x;
        LeanZ.text = "Head Pos Z: " + HeadPos.z + "Hip Pos Z: " + HipPos.z;
        Direction.text = "Direction: " + LeanDirection.x + LeanDirection.y + LeanDirection.z;
    }
}



