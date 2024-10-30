using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [Header("Player1")]
    public GameObject[] projectile_player1;
    public Transform launchPointPlayer1; 
    private int characterIndex_Player1; 
    private bool canLaunchPlayer1; // ตรวจสอบว่าสามารถโยนได้หรือไม่

    [Header("Player2")]
    public GameObject[] projectile_player2;
    public Transform launchPointPlayer2; 
    private int characterIndex_Player2; 
    private bool canLaunchPlayer2; 

    public float maxLaunchSpeed = 20f; 
    public float chargeRate = 5f; 

    private float currentLaunchSpeed; 

    void Update()
    {
        characterIndex_Player1 = PlayerPrefs.GetInt("SelectedCharacter_Player1", 0);
        characterIndex_Player2 = PlayerPrefs.GetInt("SelectedCharacter_Player2", 0);

        if (Input.GetMouseButton(0)) 
        {
            currentLaunchSpeed += chargeRate * Time.deltaTime; 
            currentLaunchSpeed = Mathf.Clamp(currentLaunchSpeed, 0, maxLaunchSpeed); 
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (canLaunchPlayer1)
            {
                LaunchProjectile(projectile_player1[characterIndex_Player1], launchPointPlayer1);
                canLaunchPlayer1 = false;
            }
            else if (canLaunchPlayer2)
            {
                LaunchProjectile(projectile_player2[characterIndex_Player2], launchPointPlayer2);
                canLaunchPlayer2 = false;
            }

            currentLaunchSpeed = 0;
        }
    }

    private void LaunchProjectile(GameObject projectilePrefab, Transform launchPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, launchPoint.rotation);
        projectile.GetComponent<Rigidbody>().velocity = currentLaunchSpeed * launchPoint.up;
    }

    public void EnablePlayer1Launch(System.Action onLaunchCallback)
    {
        canLaunchPlayer1 = true;
    }

    public void EnablePlayer2Launch(System.Action onLaunchCallback)
    {
        canLaunchPlayer2 = true;
    }
}
