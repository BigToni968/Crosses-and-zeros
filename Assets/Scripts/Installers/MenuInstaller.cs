using UnityEngine;
using Zenject;

public class MenuInstaller : MonoInstaller
{
    [SerializeField]
    private GalleryView _view;

    public override void InstallBindings()
    {
        IModelGallery model = ProjectContext.Instance.Container.Resolve<IModelGallery>();
        Container.BindInstance<IController>(new GalleryController(model, _view)).AsSingle();
    }
}