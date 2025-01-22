using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Windows.Kinect;
using Joint = Windows.Kinect.Joint;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public int counter = 0;
    public BodySourceManager mBodySourceManager;

    public Text LeanX;
    public Text LeanZ;
    public Text Direction;
    public Text ScoreText;

    public float realX;
    public float realZ;

    public GameObject catcher; // Das spielbare Objekt

    public bool isActive = false;

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

    private Vector2 gameAreaX = new Vector2 (-5f, 5f);
    private Vector2 gameAreaZ = new Vector2(-5f, 5f);
    private Vector2 realAreaX = new Vector2(-5, 5);
    private Vector2 realAreaZ = new Vector2(10, 23);

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
       

        foreach (var body in data)
        {
            if (body != null && body.IsTracked)
            {
                UpdateJointPosition(body);
            }
        }
        #endregion
        

        MovePlayer(HipPos);
        realX = HipPos.x;
        realZ = HipPos.z;

        /*
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveX, 0, moveZ);

        //move player
        transform.Translate(movement * speed * Time.deltaTime);
        */

        if (HipPos.z != 0 && HipPos.x !=0)
        {
            isActive = true;
        }else if (HipPos.x == 0 && HipPos.z == 0)
        {
            isActive = false;
        }
    }

    private void UpdateJointPosition(Body body)
    {
        foreach (JointType mJoints in mJoints)
        {
            Joint sourceJoint = body.Joints[mJoints];
            currentPosition = GetVector3FromJoint(sourceJoint);

            if (mJoints.ToString() == "Head")
            {
                HeadPos = currentPosition;
            }
            else
            {
                HipPos = currentPosition;
            }

           // SetPositionText();

        }

        LeanDirection = new Vector3(HipPos.x - HeadPos.x, HipPos.y - HeadPos.y, HipPos.z - HeadPos.z);

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

    private void MovePlayer(Vector3 hipPosition)
    {
        // Skalieren der realen Position auf den Spielbereich
        float scaledX = Map(hipPosition.x, realAreaX.x, realAreaX.y, gameAreaX.x, gameAreaX.y);

        float scaledZ = Map(hipPosition.z, realAreaZ.y, realAreaZ.x, gameAreaZ.x, gameAreaZ.y);
       // float scaledZ = Map(hipPosition.z, realAreaZ.x, realAreaZ.y, gameAreaZ.x, gameAreaZ.y);

        // Spielerposition aktualisieren
        catcher.transform.position = new Vector3(scaledX, 0.069f, scaledZ);
        catcher.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    private float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }

    public void IncreaseScore()
    {
        counter++;
        SetScore();
    }

    private void SetScore()
    {
        ScoreText.text = "Score: " + counter;
    }
}

