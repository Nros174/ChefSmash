using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill
{
    public string skillName;
    public string description;
}

[System.Serializable]
public class Character
{
    public string characterName;
    public Skill[] skills; // Array for character skills
}
