using System.Collections; // นำเข้า namespace สำหรับการใช้ Collections
using System.Collections.Generic; // นำเข้า namespace สำหรับการใช้ Collections แบบ Generic
using UnityEngine; // นำเข้า namespace สำหรับ UnityEngine
using UnityEngine.UI;
public class Launcher : MonoBehaviour // สร้างคลาส Launcher ที่สืบทอดมาจาก MonoBehaviour
{
    [Header("Launcher")]
    public float maxLaunchSpeed = 15f; // ความเร็วสูงสุดที่สามารถปล่อยอาวุธได้
    public float chargeRate = 5f; // อัตราการชาร์จความเร็วในการโยน
    private float currentLaunchSpeed; // ตัวแปรสำหรับเก็บความเร็วปัจจุบันในการโยนอาวุธ
    private TimerController timerController; // อ้างอิง TimerController

    [Header("Player1")] // เพิ่ม header ใน Inspector สำหรับ Player1
    public GameObject[] projectile_player1; // สร้าง array สำหรับอาวุธของ Player1
    public Transform launchPointPlayer1; // สร้าง Transform สำหรับจุดปล่อยอาวุธของ Player1
    private int characterIndex_Player1; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player1
    private bool canLaunchPlayer1; // ตรวจสอบว่าสามารถโยนอาวุธได้หรือไม่

    [Header("Player2")] // เพิ่ม header ใน Inspector สำหรับ Player2
    public GameObject[] projectile_player2; // สร้าง array สำหรับอาวุธของ Player2
    public Transform launchPointPlayer2; // สร้าง Transform สำหรับจุดปล่อยอาวุธของ Player2
    private int characterIndex_Player2; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player2
    private bool canLaunchPlayer2; // ตรวจสอบว่าสามารถโยนอาวุธได้หรือไม่

    [Header("UI_Power")]
    public Image[] power_image; // timer_linear_image[0] สำหรับ Player1, timer_linear_image[1] สำหรับ Player2
    public GameObject[] Power_Holder;

    void Start() // ฟังก์ชันเริ่มต้น
    {
        timerController = FindObjectOfType<TimerController>(); // ค้นหา TimerController ใน Scene
    }

    void Update() // ฟังก์ชันที่ทำงานในทุก frame
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0); // ดึงค่า index ตัวละคร Player1
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0); // ดึงค่า index ตัวละคร Player2

        if (Input.GetMouseButton(0)) // ถ้าปุ่มเมาส์ถูกกดลง
        {
            if (canLaunchPlayer1 || canLaunchPlayer2) // ตรวจสอบว่ามีการอนุญาตให้โยน
            {
                timerController.StopTimer(); // หยุดนาฬิกา
                Power(canLaunchPlayer1 ? 0 : 1);

                currentLaunchSpeed += chargeRate * Time.deltaTime; // เพิ่มความเร็วการโยนตามเวลา
                currentLaunchSpeed = Mathf.Clamp(currentLaunchSpeed, 0, maxLaunchSpeed); // จำกัดความเร็วไม่ให้เกิน maxLaunchSpeed
            }
        }

        if (Input.GetMouseButtonUp(0)) // ถ้าปุ่มเมาส์ถูกปล่อย
        {


            if (canLaunchPlayer1) // ถ้าสามารถโยนของ Player1
            {
                Power_Holder[0].SetActive(false);
                LaunchProjectile(projectile_player1[characterIndex_Player1], launchPointPlayer1); // เรียกฟังก์ชันโยนอาวุธ Player1
                canLaunchPlayer1 = false; // ปิดการโยนอาวุธ Player1
            }
            else if (canLaunchPlayer2) // ถ้าสามารถโยนของ Player2
            {
                Power_Holder[1].SetActive(false);
                LaunchProjectile(projectile_player2[characterIndex_Player2], launchPointPlayer2); // เรียกฟังก์ชันโยนอาวุธ Player2
                canLaunchPlayer2 = false; // ปิดการโยนอาวุธ Player2
            }

            currentLaunchSpeed = 0; // รีเซ็ตความเร็วการโยน
        }
    }

    private void LaunchProjectile(GameObject projectilePrefab, Transform launchPoint) // ฟังก์ชันสำหรับโยนอาวุธ
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation); // สร้างอาวุธใหม่
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = currentLaunchSpeed * launchPoint.up; // ตั้งค่าความเร็วของอาวุธ

    }

    private void Power(int playerIndex)
    {
        Power_Holder[0].SetActive(playerIndex == 0);
        Power_Holder[1].SetActive(playerIndex == 1);
        power_image[playerIndex].fillAmount = currentLaunchSpeed / maxLaunchSpeed;
    }

    public void EnablePlayer1Launch() // ฟังก์ชันเปิดใช้งานการโยนของ Player1
    {
        canLaunchPlayer1 = true; // อนุญาตให้โยนอาวุธ Player1
    }

    public void EnablePlayer2Launch() // ฟังก์ชันเปิดใช้งานการโยนของ Player2
    {
        canLaunchPlayer2 = true; // อนุญาตให้โยนอาวุธ Player2
    }
    public void DisablePlayerLaunch() // ฟังก์ชันปิดใช้งานการโยนของ Player
    {
        canLaunchPlayer1 = false; // ไม่อนุญาตให้โยนอาวุธ Player1
        canLaunchPlayer2 = false; // ไม่อนุญาตให้โยนอาวุธ Player2
    }
}