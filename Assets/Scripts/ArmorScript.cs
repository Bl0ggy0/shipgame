using UnityEngine;

public class ArmorScript : MonoBehaviour
{
    // SPRITES | 0=helmet | 1=chestplate | 2=leggings | 3=boots
    public Sprite[] clothSprites;
    public Sprite[] ironSprites;

    [Space]

    // STATS
    public float TotalDef;

    private float HelmetDef;
    private float ChestplateDef;
    private float LeggingsDef;
    private float BootsDef;

    private void UpdateStats()
    {
        TotalDef = HelmetDef + ChestplateDef + LeggingsDef + BootsDef;
    }

    public void UnequipHelmet()
    {

    }
}
