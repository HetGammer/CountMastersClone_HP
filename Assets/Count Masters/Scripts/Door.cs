using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Door : MonoBehaviour
{
    [Header(" Bonuses ")]
    [SerializeField] private bool randomBonuses;
    [SerializeField] private Bonus rightBonus;
    [SerializeField] private Bonus leftBonus;

    [Header(" Components ")]
    [SerializeField] private Collider[] doorsColliders;
    [SerializeField] private TextMeshPro rightDoorText;
    [SerializeField] private TextMeshPro leftDoorText;

    // Start is called before the first frame update
    void Start()
    {
        if(randomBonuses)
            SetRandomBonuses();

        ConfigureBonusTexts();
    }

    private void SetRandomBonuses()
    {
        rightBonus = BonusUtils.GetRandomBonus();
        leftBonus = BonusUtils.GetRandomBonus();
    }

    private void ConfigureBonusTexts()
    {
        rightDoorText.text = BonusUtils.GetBonusString(rightBonus);
        leftDoorText.text = BonusUtils.GetBonusString(leftBonus);
    }

    public int GetRunnersAmountToAdd(Collider collidedDoor, int currentRunnersAmount)
    {
        DisableDoors();

        Bonus bonus;

        if (collidedDoor.transform.position.x > 0)
            bonus = rightBonus;
        else
            bonus = leftBonus;

        return BonusUtils.GetRunnersAmountToAdd(currentRunnersAmount, bonus);
    }

    private void DisableDoors()
    {
        foreach (Collider c in doorsColliders)
            c.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
