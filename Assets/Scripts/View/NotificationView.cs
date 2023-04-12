using UnityEngine.UI;
using UnityEngine;
using Zenject;

public interface IViewNotification
{
    public delegate void CallBackHandler();
    public event CallBackHandler CallBack;
    public void Show(string message);
}

public interface IPresentarNotification : IPresentar
{
    public void Send(string message);
}

public class NotificationPresentar : IPresentarNotification
{
    private ISceneExplorer _explorer;
    private IModelGallery _model;
    private IViewNotification _view;

    public NotificationPresentar(IViewNotification view, IModelGallery model, ISceneExplorer explorer)
    {
        _view = view;
        _model = model;
        _explorer = explorer;
    }

    public void Execute()
    {
        _view.CallBack += OnCallBackView;
    }

    public void Send(string message)
    {
        _view.Show(message);
    }

    private void OnCallBackView()
    {
        if (_model.Unlock != _model.Sprites.Count)
            _explorer.Reload();
        else
            _explorer.Next();
    }
}

public class NotificationView : MonoBehaviour, IViewNotification
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private Button _button;
    [SerializeField] private Canvas _canvas;

    public event IViewNotification.CallBackHandler CallBack;

    private void Start()
    {
        _button.onClick.AddListener(() => CallBack?.Invoke());
    }

    public void Show(string message)
    {
        _text.SetText(message);
        _canvas.gameObject.SetActive(true);
    }
}
