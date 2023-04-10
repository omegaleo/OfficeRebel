using UnityEngine;

/// <summary>
/// Class that will hold the values for what color we're searching and what color to replace with
/// </summary>
public class EmployeeColorShift
{
    public Color LookingForHair => GameManager.Instance.NormalHairColor;
    public Color LookingForClothes => GameManager.Instance.NormalClothesColor;
    public Color LookingForClothesShadow => GameManager.Instance.NormalClothesShadowColor;
    public Color ReplaceWithHair;
    public Color ReplaceWithClothes;
    public Color ReplaceWithClothesShadow;

    public EmployeeColorShift()
    {
        ReplaceWithHair = GameManager.Instance.HairColors.Random();
        ReplaceWithClothes = GameManager.Instance.ClothesColors.Random();
        ReplaceWithClothesShadow = GameManager.Instance.ClothesShadowsColors.Random();
    }
}