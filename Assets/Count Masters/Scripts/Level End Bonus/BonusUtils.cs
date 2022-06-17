using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BonusUtils
{
    public enum BonusType { Add, Multiply }

    static int[] addValues = { 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60, 65, 70, 80, 90, 100 };
    static int[] multiplyValues = { 2, 3, 4, 5, 6, 7, 8 };

    public static Bonus GetRandomBonus()
    {
        BonusType randomBonusType = GetRandomBonusType();
        int value = 0;

        switch(randomBonusType)
        {
            case BonusType.Add:
                value = addValues[Random.Range(0, addValues.Length)];
                break;

            case BonusType.Multiply:
                value = multiplyValues[Random.Range(0, multiplyValues.Length)];
                break;
        }

        return new Bonus(randomBonusType, value);
    }

    public static int GetRunnersAmountToAdd(int currentRunnersAmount, Bonus bonus)
    {
        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                return bonus.GetValue();

            case BonusType.Multiply:
                return (currentRunnersAmount * bonus.GetValue());
        }

        return 0;
    }

    public static string GetBonusString(Bonus bonus)
    {
        string bonusString = null;

        switch(bonus.GetBonusType())
        {
            case BonusType.Add:
                bonusString += "+";
                break;

            case BonusType.Multiply:
                bonusString += "x";
                break;
        }

        bonusString += bonus.GetValue();

        return bonusString;
    }

    private static BonusType GetRandomBonusType()
    {
        BonusType[] bonusTypes = (BonusType[])System.Enum.GetValues(typeof(BonusType));
        return bonusTypes[Random.Range(0, bonusTypes.Length)];
    }
}

[System.Serializable]
public struct Bonus
{
    [SerializeField] private BonusUtils.BonusType bonusType;
    [SerializeField] private int value;

    public Bonus(BonusUtils.BonusType bonusType, int value)
    {
        this.bonusType = bonusType;
        this.value = value;
    }

    public BonusUtils.BonusType GetBonusType()
    {
        return bonusType;
    }

    public int GetValue()
    {
        return value;
    }
}
