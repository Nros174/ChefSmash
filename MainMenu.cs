using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("MainMenu_HowToPlay")]
    public GameObject[] Panel;
    public Image howtoplayImage;
    public Sprite[] howtoplaySprite;
    private int currentSpriteIndex = 0; // ตัวแปรสำหรับเก็บดัชนีของสไปรท์ปัจจุบัน
    private AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Panel[1].SetActive(false);
        Panel[0].SetActive(true);
    }

    public void PlayGame()
    {
        // ใช้ชื่อที่ถูกต้องตามที่ประกาศใน AudioManager
        // audioManager.PlaySFX(audioManager.take_hit_defening);
        SceneManager.LoadScene("Select_ChefSmash");
        PlayerPrefs.DeleteAll();
    }

    public void howtoplay()
    {
        Panel[0].SetActive(false);
        Panel[1].SetActive(true);
        if (howtoplaySprite.Length > 0)
        {
            howtoplayImage.sprite = howtoplaySprite[currentSpriteIndex]; // เปลี่ยนสไปรท์เป็นสไปรท์ปัจจุบัน
        }
    }

    public void NextSprite()
{
    if (howtoplaySprite.Length > 0)
    {
        currentSpriteIndex++; // เพิ่มดัชนี
        
        // ตรวจสอบว่าเป็นสไปรท์ตัวสุดท้าย
        if (currentSpriteIndex >= howtoplaySprite.Length)
        {
            // ปิด Panel[1] และกลับไปที่ Panel[0]
            Panel[1].SetActive(false);
            Panel[0].SetActive(true);
            currentSpriteIndex = 0; // รีเซ็ตดัชนีเป็น 0
        }
        else
        {
            // ถ้ายังไม่ถึงสไปรท์ตัวสุดท้าย ให้เปลี่ยนสไปรท์
            howtoplayImage.sprite = howtoplaySprite[currentSpriteIndex]; // เปลี่ยนสไปรท์เป็นสไปรท์ปัจจุบัน
        }
    }
}


    public void QuitGame()
    {
        Application.Quit();
    }
}
