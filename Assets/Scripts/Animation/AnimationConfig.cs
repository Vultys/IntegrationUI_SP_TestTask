using UnityEngine;

[CreateAssetMenu(fileName = "AnimationConfig", menuName = "IntegrationUI/AnimationConfig")]
public class AnimationConfig : ScriptableObject
{
    public float hideNonSelectedBoostersDuration = 0.3f;
    public float animateToCenterDuration = 0.5f;
    public float animateToSlotDuration = 0.6f;
    public float fadeOutDialogDuration = 0.5f;
    public float sidePanelAnimationDuration = 0.5f;
    public float boosterItemCenterScale = 1.3f;
    public float boosterItemSlotScale = 0.55f;
}
