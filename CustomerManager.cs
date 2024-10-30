using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CustomerManager : MonoBehaviour
{
    public int health_player1 = 5;
    public int health_player2 = 5;

    [Header("Chefs")]
    public Image[] chef;
    public Sprite[] chefImages;
    private int characterIndex_Player1; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 1
    private int characterIndex_Player2; // ตัวแปรเก็บค่าตัวละครที่เลือกสำหรับผู้เล่น 2

    [Header("Customers")]
    public Image[] customers_player1;
    public Image[] customers_player2;
    public Sprite[] fullCustomer;
    public Sprite empatyCustomer;

    [Header("Victory")]
    public GameObject VictoryPanel;
    public Sprite[] ImagesWinner; // ImagesWinner เป็นอาร์เรย์ภาพของตัวละคร
    public Image Winer; //  Winer เป็นตัวแปร Image สำหรับเก็บ sprite ของตัวละครที่จะแสดง

    void Awake()
    {
        health_player1 = 5;
        health_player2 = 5;
    }

    // Update is called once per frame
    void Update()
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0);
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0);

        chef[0].sprite = chefImages[characterIndex_Player1];
        chef[1].sprite = chefImages[characterIndex_Player2];

        // ตั้งค่า sprite ของทุกช่องเป็น empatyCustomer
        foreach (Image img in customers_player1)
        {
            img.sprite = empatyCustomer;
        }
        foreach (Image img in customers_player2)
        {
            img.sprite = empatyCustomer;
        }

        // ตั้งค่า sprite ของช่องตามจำนวน health เป็น fullCustomer
        for (int i = 0; i < health_player1; i++)
        {
            customers_player1[i].sprite = fullCustomer[characterIndex_Player1];
        }

        for (int j = 0; j < health_player2; j++)
        {
            customers_player2[j].sprite = fullCustomer[characterIndex_Player2];
        }
    }

    public void Victory()
    {
        if (health_player1 <= 0)
        {
            Winer.sprite = ImagesWinner[characterIndex_Player2];
            VictoryPanel.SetActive(true); // แสดง VictoryPanel
        }
        else if (health_player2 <= 0)
        {
            Winer.sprite = ImagesWinner[characterIndex_Player1];
            VictoryPanel.SetActive(true); // แสดง VictoryPanel
        }
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
