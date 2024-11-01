using System.Collections; // นำเข้า namespace สำหรับการใช้ Collections
using System.Collections.Generic; // นำเข้า namespace สำหรับการใช้ Collections แบบ Generic
using UnityEngine; // นำเข้า namespace สำหรับ UnityEngine
using UnityEngine.UI; // นำเข้า namespace สำหรับ UI
using UnityEngine.SceneManagement; // นำเข้า namespace สำหรับการจัดการ Scene

public class CustomerManager : MonoBehaviour // สร้างคลาส CustomerManager ที่สืบทอดมาจาก MonoBehaviour
{
    [Header("Customer")]
    public int health_player1 = 5; // ตัวแปรสำหรับเก็บพลังชีวิตของ Player1
    public int health_player2 = 5; // ตัวแปรสำหรับเก็บพลังชีวิตของ Player2

    [Header("Chefs")] // เพิ่ม header ใน Inspector สำหรับ Chefs
    public Image[] chef; // Array สำหรับเก็บ Image ของเชฟ
    public Sprite[] chefImages; // Array สำหรับเก็บ Sprite ของเชฟ
    private int characterIndex_Player1; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 1
    private int characterIndex_Player2; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 2

    [Header("Customers")] // เพิ่ม header ใน Inspector สำหรับ Customers
    public Image[] customers_player1; // Array สำหรับเก็บ Image ของลูกค้า Player1
    public Image[] customers_player2; // Array สำหรับเก็บ Image ของลูกค้า Player2
    public Sprite[] fullCustomer; // Array สำหรับเก็บ Sprite ของลูกค้าที่มีสุขภาพเต็ม
    public Sprite empatyCustomer; // Sprite สำหรับลูกค้าที่ไม่มีสุขภาพ

    [Header("Victory")] // เพิ่ม header ใน Inspector สำหรับ Victory
    public GameObject VictoryPanel; // GameObject สำหรับแสดง Victory Panel
    public Sprite[] ImagesWinner; // ImagesWinner เป็นอาร์เรย์ภาพของตัวละคร
    public Image Winer; // Winer เป็นตัวแปร Image สำหรับเก็บ sprite ของตัวละครที่จะแสดง
    private TimerController timerController; // อ้างอิง TimerController
    private Launcher launcher; // อ้างอิง Launcher

    void Awake() // ฟังก์ชันที่เรียกเมื่อเริ่มต้น
    {
        timerController = FindObjectOfType<TimerController>(); // ค้นหา TimerController
        launcher = FindObjectOfType<Launcher>(); // ค้นหา Launcher
        health_player1 = 5; // ตั้งค่าเริ่มต้นพลังชีวิตของ Player1
        health_player2 = 5; // ตั้งค่าเริ่มต้นพลังชีวิตของ Player2
    }

    void Update() // ฟังก์ชันที่ทำงานในทุก frame
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0); // ดึงค่าตัวละครที่เลือกสำหรับ Player1
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0); // ดึงค่าตัวละครที่เลือกสำหรับ Player2

        chef[0].sprite = chefImages[characterIndex_Player1]; // ตั้งค่า sprite ของเชฟ Player1
        chef[1].sprite = chefImages[characterIndex_Player2]; // ตั้งค่า sprite ของเชฟ Player2

        // ตั้งค่า sprite ของทุกช่องเป็น empatyCustomer
        foreach (Image img in customers_player1)
        {
            img.sprite = empatyCustomer; // ตั้งค่า sprite ให้เป็น empatyCustomer สำหรับ Player1
        }

        // ตั้งค่า sprite ของทุกช่องเป็น empatyCustomer
        foreach (Image img in customers_player2)
        {
            img.sprite = empatyCustomer; // ตั้งค่า sprite ให้เป็น empatyCustomer สำหรับ Player2
        }

        // ตั้งค่า sprite ของช่องตามจำนวน health เป็น fullCustomer
        for (int i = 0; i < health_player1; i++)
        {
            customers_player1[i].sprite = fullCustomer[characterIndex_Player1]; // ตั้งค่า sprite ของลูกค้า Player1 ตามจำนวน health
        }

        // ตั้งค่า sprite ของช่องตามจำนวน health เป็น fullCustomer
        for (int j = 0; j < health_player2; j++)
        {
            customers_player2[j].sprite = fullCustomer[characterIndex_Player2]; // ตั้งค่า sprite ของลูกค้า Player2 ตามจำนวน health
        }

        Victory(); // ตรวจสอบสถานะการชนะ
    }

    private void Victory() // ฟังก์ชันตรวจสอบสถานะการชนะ
    {

        if (health_player1 <= 0) // ถ้าพลังชีวิตของ Player1 <= 0
        {
            timerController.StopTimer_player1(); // หยุดนาฬิกา
            timerController.StopTimer_player2(); // หยุดนาฬิกา
            launcher.DisablePlayer1Launch();
            Winer.sprite = ImagesWinner[characterIndex_Player2]; // แสดง sprite ของผู้ชนะ Player2
            VictoryPanel.SetActive(true); // แสดง VictoryPanel
        }
        else if (health_player2 <= 0) // ถ้าพลังชีวิตของ Player2 <= 0
        {
            timerController.StopTimer_player1(); // หยุดนาฬิกา
            timerController.StopTimer_player2(); // หยุดนาฬิกา
            launcher.DisablePlayer2Launch();
            Winer.sprite = ImagesWinner[characterIndex_Player1]; // แสดง sprite ของผู้ชนะ Player1
            VictoryPanel.SetActive(true); // แสดง VictoryPanel
        }
    }

    public void PlayAgain() // ฟังก์ชันสำหรับเริ่มเกมใหม่
    {
        SceneManager.LoadScene("MainMenu"); // โหลด Scene ของ MainMenu
    }
}
