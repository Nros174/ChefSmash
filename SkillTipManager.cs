using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SkillTipManager : MonoBehaviour
{
    public static SkillTipManager _instance;
    public TMP_Text skillNameText;     
    public TMP_Text descriptionText;     

    private void Awake(){
        if(_instance != null && _instance != this){
            Destroy(this.gameObject);
        }
        else{
            _instance = this;
        }
    }

    void Start()
    {
        Cursor.visible = true;
        gameObject.SetActive(false);
    }

    void Update()
    {
        transform.position = Input.mousePosition;
    }

    public void ShowSkillTip(string skillName, string description){
        gameObject.SetActive(true);
        skillNameText.text = skillName;
        descriptionText.text = description;
    }

    public void HideSkillTip(){
        gameObject.SetActive(false);
        skillNameText.text = string.Empty;
        descriptionText.text = string.Empty;
    }
}
