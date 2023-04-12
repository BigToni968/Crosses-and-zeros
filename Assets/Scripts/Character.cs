using UnityEngine;
using Zenject;

public interface IController
{
    public void Execute();
}

public class CharacterController : IController
{
    private IModelGallery _model;
    private IViewCharacter _view;
    public CharacterController(IModelGallery model, IViewCharacter view)
    {
        _model = model;
        _view = view;
    }

    public void Execute()
    {
        _view.Show(_model.Sprites[_model.Unlock]);
    }
}


public class Character : MonoBehaviour
{
    private IController _controller;

    [Inject]
    public void Construct([Inject(Id = "CharacterController")] IController controller)
    {
        _controller = controller;
    }

    private void Start()
    {
        _controller.Execute();
    }
}