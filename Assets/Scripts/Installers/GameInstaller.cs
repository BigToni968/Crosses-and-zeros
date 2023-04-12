using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private CharacterView _characterView;
    [SerializeField] private MapView _mapView;
    [SerializeField] private TMPro.TextMeshProUGUI _title;
    [SerializeField] private SceneExplorer _explorer;
    [SerializeField] private NotificationView _notificationView;

    public override void InstallBindings()
    {
        IModelGallery model = ProjectContext.Instance.Container.Resolve<IModelGallery>();
        _title.SetText($"{_title.text} {model.Unlock + 1}");

        IPresentarNotification notification = new NotificationPresentar(_notificationView, model, _explorer);

        Container.BindInstance<IController>(new CharacterController(model, _characterView)).WithId("CharacterController").AsTransient();
        Container.BindInstance<IPresenterMap>(new MapPresenter(new MapModel(), _mapView, model, notification)).AsSingle();
    }
}