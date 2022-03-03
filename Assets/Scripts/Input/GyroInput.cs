using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroInput : MonoBehaviour
{
    bool gyroEnabled;
    Gyroscope gyro;
    GameObject cameraContainer;
    Quaternion rot;

    void Start()
    {
        cameraContainer = new GameObject("Camera Container");
        cameraContainer.transform.position = transform.position;
        transform.SetParent(cameraContainer.transform);

        gyroEnabled = EnableGyro();
    }

    bool EnableGyro()
    {
        if(SystemInfo.supportsGyroscope)
        {
            print("Gyro supported");
            gyro = Input.gyro;
            gyro.enabled = true;

            cameraContainer.transform.rotation = Quaternion.Euler(90f, 90f, 0);
            rot = new Quaternion(0, 0, 1, 0);

            return true;
        }
        print("Gyro not supported");
        return false;
    }

    public Quaternion GetRotationValues()
    {
        if (gyroEnabled)
            return gyro.attitude * rot;
        else
            return Quaternion.identity;
    }
}
