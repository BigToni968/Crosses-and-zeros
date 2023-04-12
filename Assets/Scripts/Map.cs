using UnityEngine;
using Zenject;

public enum FieldType
{
    Empty,
    Player,
    Enemy
}

public interface IModelMap
{
    public int Column { get; }
    public int Row { get; }

    public FieldType[,] Create(int size);
    public bool Update(FieldType field, int position);
    public bool SearchIntersections(FieldType field);
    public bool SearchEmptyField();
}

public class MapModel : IModelMap
{
    public int Column { get; private set; }
    public int Row { get; private set; }

    private FieldType[,] _map;

    public FieldType[,] Create(int size)
    {
        Column = Row = size;
        _map = new FieldType[Column, Row];

        return _map;
    }

    public bool SearchIntersections(FieldType field)
    {
        int countIntersections = 0;

        //Horizontal
        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Row; j++)
                if (_map[i, j] == field)
                    countIntersections++;

            if (countIntersections != Row) countIntersections = 0;
            else return true;
        }

        countIntersections = 0;

        //Vertical
        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Row; j++)
                if (_map[j, i] == field)
                    countIntersections++;

            if (countIntersections != Column) countIntersections = 0;
            else return true;
        }

        countIntersections = 0;

        //Сurve
        for (int i = 0; i < Column; i++)
            if (_map[i, i] == field)
                countIntersections++;

        if (countIntersections != Column) countIntersections = 0;
        else return true;

        countIntersections = 0;

        for (int i = 0; i < Column; i++)
            if (_map[i, (Column - 1) - i] == field) countIntersections++;

        if (countIntersections != Column) countIntersections = 0;
        else return true;

        return false;
    }

    public bool Update(FieldType field, int position)
    {
        int indexField = 0;

        for (int i = 0; i < Column; i++)
        {
            for (int j = 0; j < Row; j++)
            {
                if (indexField == position)
                    if (_map[i, j] == FieldType.Empty)
                    {
                        _map[i, j] = field;

                        return true;
                    }

                indexField++;
            }
        }

        return false;
    }

    public bool SearchEmptyField()
    {
        int amountField = 0;

        for (int i = 0; i < Column; i++)
            for (int j = 0; j < Row; j++)
                if (_map[i, j] == FieldType.Empty)
                    amountField++;

        return amountField > 0;
    }
}

public interface IPresentar
{
    public void Execute();
}

public interface IPresenterMap : IPresentar
{
    public void ChoiseField(string name);
}

public class MapPresenter : IPresenterMap
{
    private IModelMap _model;
    private IViewMap _view;
    private IModelGallery _modelGallery;
    private IPresentarNotification _notification;

    public MapPresenter(IModelMap model,
        IViewMap view,
        IModelGallery modelGallery,
        IPresentarNotification notification)
    {
        _model = model;
        _view = view;
        _view.ChoiseEvent += ChoiseField;
        _modelGallery = modelGallery;
        _notification = notification;
    }

    public void Execute()
    {
        FieldType[,] map = _model.Create(_modelGallery.Unlock + 1 < _modelGallery.Sprites.Count / 2 ? 3 : 5);
        _view.show(map, _model.Column);
        _notification.Execute();
    }

    public void ChoiseField(string name)
    {
        int position = System.Int32.Parse(name.Split('_')[1]);

        if (_model.Update(FieldType.Player, position))
        {
            _view.UpdateField(FieldType.Player, position);
            Game();
        }
    }

    private void Game()
    {
        if (_model.SearchIntersections(FieldType.Player))
        {
            _modelGallery.Unlock += _modelGallery.Unlock < _modelGallery.Sprites.Count ? 1 : 0;
            _notification.Send("Вы победили. Нажмите на кнопку ниже чтобы перейти на следующий этап");
            return;
        }


        StepBot();

        if (_model.SearchIntersections(FieldType.Enemy))
        {
            _notification.Send("Очень жаль, но вы проиграли. Нажмите на кнопку ниже чтобы повторить снова.");
            return;
        }

    }

    private void StepBot()
    {
        while (_model.SearchEmptyField())
        {
            int position = Random.Range(0, _model.Column * _model.Row);

            if (_model.Update(FieldType.Enemy, position))
            {
                _view.UpdateField(FieldType.Enemy, position);
                return;
            }
        }

        _notification.Send("Вы зашли в тупик. Но это засчитается как победа! Вы молодец!");
        _modelGallery.Unlock += _modelGallery.Unlock < _modelGallery.Sprites.Count ? 1 : 0;
    }
}

public class Map : MonoBehaviour
{
    private IPresenterMap _presenter;

    [Inject]
    public void Contsruct(IPresenterMap presenter)
    {
        _presenter = presenter;
    }

    private void Start()
    {
        _presenter.Execute();
    }
}