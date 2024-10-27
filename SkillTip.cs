using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTip : MonoBehaviour
{
    public string skillName;
    public string description;

    private void OnMouseEnter(){
        SkillTipManager._instance.ShowSkillTip(skillName, description);
    }

    private void OnMouseExit(){
        SkillTipManager._instance.HideSkillTip();
    }
}
