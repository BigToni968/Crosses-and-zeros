using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;

[CreateAssetMenu(menuName = "Source")]
public class Source : ScriptableObject
{
    [field: SerializeField]
    public GalleryModel Gallery { get; private set; }

    [System.Serializable]
    public class GalleryModel : IModelGallery, ICloneable
    {
        [field: SerializeField]
        public List<Sprite> Sprites { get; private set; }

        [field: SerializeField]
        public int Unlock { get; set; } = -1;

        public GalleryModel(List<Sprite> sprites, int unlock)
        {
            Sprites = sprites;
            Unlock = unlock;
        }

        public object Clone() => new GalleryModel(Sprites, Unlock);
    }
}