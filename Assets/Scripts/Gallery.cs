using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

public interface IModelGallery
{
    public List<Sprite> Sprites { get; }
    public int Unlock { get; set; }
}


public class GalleryController : IController
{
    public IModelGallery Model { get; private set; }
    private IViewGallery _view;

    private List<RawImage> _listImageLink;

    public GalleryController(IModelGallery model, IViewGallery view)
    {
        Model = model;
        _view = view;
        _listImageLink = new List<RawImage>();

        foreach (var sprite in Model.Sprites)
        {
            RawImage image = GameObject.Instantiate(_view.Prefab, _view.Content);
            image.texture = sprite.texture;
            _listImageLink.Add(image);
        }

    }

    public void Execute()
    {
        _view.Show(_listImageLink, Model.Unlock);
    }
}

public class Gallery : MonoBehaviour
{
    private IController _controller;

    [Inject]
    public void Construct(IController controller) => _controller = controller;

    private void OnEnable() => _controller.Execute();
}