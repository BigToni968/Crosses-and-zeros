using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public interface IViewGallery
{
    public RawImage Prefab { get; }
    public Transform Content { get; }

    public void Show(List<RawImage> images, int unlock);
}

public class GalleryView : MonoBehaviour, IViewGallery
{
    [field: SerializeField]
    public RawImage Prefab { get; private set; }

    [field: SerializeField]
    public Transform Content { get; private set; }

    public void Show(List<RawImage> images, int unlock)
    {
        for (int i = unlock; i > 0; i--)
            images[i - 1].gameObject.SetActive(true);
    }
}
