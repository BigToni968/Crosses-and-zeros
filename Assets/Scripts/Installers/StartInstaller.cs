using UnityEngine;
using Zenject;

public class StartInstaller : MonoInstaller
{
    [SerializeField] private Source _source;
    [SerializeField] private SceneExplorer _explorer;

    public override void InstallBindings()
    {
        ProjectContext.Instance.Container.BindInstance<IModelGallery>(_source.Gallery.Clone() as IModelGallery).AsSingle();
        _explorer.Next();
    }
}