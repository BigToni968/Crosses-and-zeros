using UnityEngine.UI;
using UnityEngine;

public interface IViewCharacter
{
    public void Show(Sprite sprite);
}

public class CharacterView : MonoBehaviour, IViewCharacter
{
    [SerializeField] private RawImage _portrait;

    public void Show(Sprite sprite)
    {
        _portrait.texture = sprite.texture;
    }
}