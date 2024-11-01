using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanRotation : MonoBehaviour
{
    public float rotationX = 0f;
    public float rotationY = 5f;
    public float rotationZ = 0f;

    // Maximum rotation angle
    public float maxAngle = 5f;

    public bool isRotating = true; // ตัวแปรควบคุมการหมุน (เริ่มต้นเป็น true เพื่อให้หมุนได้)

    // Update is called once per frame
    void Update()
    {
        if (isRotating)
        {
            if (gameObject.name == "Fan Mask")
            {
                float angle = Mathf.PingPong(Time.time * rotationZ, maxAngle * 2) - maxAngle;
                transform.localRotation = Quaternion.Euler(rotationX, rotationY, angle);
            }

            if (gameObject.name == "Fan Blades")
            {
                transform.Rotate(rotationX, rotationY, rotationZ);
            }
        }
    }

    public void StartRotation() // ฟังก์ชันเพื่อเริ่มการหมุน
    {
        isRotating = true;
    }

    public void StopRotation() // ฟังก์ชันเพื่อหยุดการหมุน
    {
        isRotating = false;
    }
}
