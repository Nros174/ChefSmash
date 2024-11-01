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
    private Skill skill;
    private GameStateController gameStateController;
    private FanRotation fan;

    [Header("Player1")] // เพิ่ม header ใน Inspector สำหรับ Player1
    public GameObject[] projectile_player1; // สร้าง array สำหรับอาวุธของ Player1
    public Transform launchPointPlayer1; // สร้าง Transform สำหรับจุดปล่อยอาวุธของ Player1
    private int characterIndex_Player1; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player1
    private bool canLaunchPlayer1; // ตรวจสอบว่าสามารถโยนอาวุธได้หรือไม่
    public GameObject[] projectile_Upsize_player1;

    [Header("Player2")] // เพิ่ม header ใน Inspector สำหรับ Player2
    public GameObject[] projectile_player2; // สร้าง array สำหรับอาวุธของ Player2
    public Transform launchPointPlayer2; // สร้าง Transform สำหรับจุดปล่อยอาวุธของ Player2
    private int characterIndex_Player2; // ตัวแปรสำหรับเก็บ index ของตัวละคร Player2
    private bool canLaunchPlayer2; // ตรวจสอบว่าสามารถโยนอาวุธได้หรือไม่
    public GameObject[] projectile_Upsize_player2;

    [Header("UI_Power")]
    public Image[] power_image; // timer_linear_image[0] สำหรับ Player1, timer_linear_image[1] สำหรับ Player2
    public GameObject[] Power_Holder;

    void Start() // ฟังก์ชันเริ่มต้น
    {
        gameStateController = FindObjectOfType<GameStateController>();
        timerController = FindObjectOfType<TimerController>(); // ค้นหา TimerController ใน Scene
        skill = FindObjectOfType<Skill>(); // ค้นหา Skill ใน Scene
        fan = FindObjectOfType<FanRotation>();
    }

    void Update() // ฟังก์ชันที่ทำงานในทุก frame
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0); // ดึงค่า index ตัวละคร Player1
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0); // ดึงค่า index ตัวละคร Player2


        if (gameStateController.state == TurnState.Player1_Turn)
        {
            // ตรวจสอบการอนุญาตให้โยนและหยุดนาฬิกาสำหรับผู้เล่น 1
            if (Input.GetKey(KeyCode.E) && canLaunchPlayer1)
            {
                timerController.StopTimer_player1(); // หยุดนาฬิกาผู้เล่น 1

                // เพิ่มความเร็วการโยนตามเวลาและจำกัดไม่ให้เกิน maxLaunchSpeed
                currentLaunchSpeed += chargeRate * Time.deltaTime;
                currentLaunchSpeed = Mathf.Clamp(currentLaunchSpeed, 0, maxLaunchSpeed);

                Power(0); // เรียกใช้ Power สำหรับผู้เล่น 1
            }


            if (Input.GetKeyUp(KeyCode.E) && canLaunchPlayer1) // ถ้าสามารถโยนของ Player1
            {
                Power_Holder[0].SetActive(false);

                if (skill.Graceskill == true)
                {
                    // เปลี่ยนขนาดของกระสุน
                    LaunchProjectile(projectile_Upsize_player1[characterIndex_Player1], launchPointPlayer1);
                }
                else if (skill.MonsherSkill == true)
                {
                    Debug.Log("รักหลง");
                }
                else
                {
                    LaunchProjectile(projectile_player1[characterIndex_Player1], launchPointPlayer1); // เรียกฟังก์ชันโยนอาวุธ Player1
                }

                canLaunchPlayer1 = false; // ปิดการโยนอาวุธ Player1
                if (skill.Graceskill == true)
                {
                    skill.disableGraceskill();

                }
                else if (fan.isRotating == false && skill.WindSkill == true)
                {
                    fan.StartRotation();
                    skill.disableWind();
                }
                else if (skill.MonsherSkill == true)
                {
                    skill.disableMonsher();
                }
                else if (skill.ShieldSkill == true)
                {
                    skill.disableShield();
                }

                currentLaunchSpeed = 0; // รีเซ็ตความเร็วการโยน
            }
        }


        if (gameStateController.state == TurnState.Player2_Turn)
        {
            // ตรวจสอบการอนุญาตให้โยนและหยุดนาฬิกาสำหรับผู้เล่น 2
            if (Input.GetKey(KeyCode.O) && canLaunchPlayer2)
            {
                timerController.StopTimer_player2(); // หยุดนาฬิกาผู้เล่น 2
                                                     // เพิ่มความเร็วการโยนตามเวลาและจำกัดไม่ให้เกิน maxLaunchSpeed
                currentLaunchSpeed += chargeRate * Time.deltaTime;
                currentLaunchSpeed = Mathf.Clamp(currentLaunchSpeed, 0, maxLaunchSpeed);

                Power(1); // เรียกใช้ Power สำหรับผู้เล่น 2
            }

            if (Input.GetKeyUp(KeyCode.O) && canLaunchPlayer2) // ถ้าสามารถโยนของ Player2
            {
                Power_Holder[1].SetActive(false);
                if (skill.Graceskill == true)
                {
                    LaunchProjectile(projectile_Upsize_player2[characterIndex_Player2], launchPointPlayer2); // เรียกฟังก์ชันโยนอาวุธ Player2
                }
                else if (skill.MonsherSkill == true)
                {
                    Debug.Log("รักหลง");
                }
                else
                {
                    LaunchProjectile(projectile_player2[characterIndex_Player2], launchPointPlayer2); // เรียกฟังก์ชันโยนอาวุธ Player1
                }

                canLaunchPlayer2 = false; // ปิดการโยนอาวุธ Player2

                if (skill.Graceskill == true)
                {
                    skill.disableGraceskill();
                }
                else if (fan.isRotating == false && skill.WindSkill == true)
                {
                    fan.StartRotation();
                    skill.disableWind();
                }
                else if (skill.MonsherSkill == true)
                {
                    skill.disableMonsher();
                }
                else if (skill.ShieldSkill == true)
                {
                    skill.disableShield();
                }

                currentLaunchSpeed = 0; // รีเซ็ตความเร็วการโยน
            }
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
    public void DisablePlayer1Launch() // ฟังก์ชันปิดใช้งานการโยนของ Player
    {
        canLaunchPlayer1 = false; // ไม่อนุญาตให้โยนอาวุธ Player1
    }
    public void DisablePlayer2Launch() // ฟังก์ชันปิดใช้งานการโยนของ Player
    {
        canLaunchPlayer2 = false; // ไม่อนุญาตให้โยนอาวุธ Player2
    }

}