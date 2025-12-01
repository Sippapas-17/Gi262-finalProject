using Solution;
using UnityEngine;

public class NPCSkill : Identity
{
    public GameObject skillUi;
    public bool canTalk = true;

    public override bool Interact(OOPPlayer player)
    {
        if (canTalk)
        {
            Debug.Log("NPCSkill: Opening Skill Tree.");
            skillUi.SetActive(true);
            return true;
        }
        else
        {
            Debug.Log("I not need to talk to you");
            return false;
        }
    }

    public override bool Hit()
    {
        return false;
    }
}