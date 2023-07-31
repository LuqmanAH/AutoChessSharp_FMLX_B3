﻿namespace AutoChessSharp.Core;

// might not be a good idea to create IBoard here SOLVE
//? can the price implemented as a field here?
//? which better? implement positioning method here or in the GR?

public class Piece : IPiece
{
    private ArcheTypeEnum _archeType;
    private RarityEnum _rarity;

    private Position? _position;
    private int _price;

    private string? _pieceName;
    private int _healthPoint;
    private int _attack;

    public Piece(int archeType, int rarity)
    {
        this._archeType = (ArcheTypeEnum)archeType;

        switch (archeType)
        {
            case (int)ArcheTypeEnum.Warrior:
                _attack = 12 * rarity;
                _healthPoint = 15 * rarity;
                _price = 1 * rarity;
                _rarity = (RarityEnum) rarity;
                break;

            case (int)ArcheTypeEnum.Mage:
                _attack = 18 * rarity;
                _healthPoint = 5 * rarity;
                _price = 2 * rarity;
                _rarity = (RarityEnum) rarity;
                break;

            case (int)ArcheTypeEnum.Hunter:
                _attack = 15 * rarity;
                _healthPoint = 6 * rarity;
                _price = 1 * rarity;
                _rarity = (RarityEnum) rarity;
                break;

            case (int)ArcheTypeEnum.Assassin:
                _attack = 16 * rarity;
                _healthPoint = 10 * rarity;
                _price = 3 * rarity;
                _rarity = (RarityEnum) rarity;
                break;

        }

    }

    public Position? GetPosition()
    {
        return _position;
    }

    //* position setting applied in GR, piece position should null until in game
    // public bool SetPosition(Position _position)
    // {
    //     if (_position.GetX() < 0 || _position.GetY() < 0 || _position.GetX() > board.GetBoardSize() || _position.GetY() > board.GetBoardSize())
    //     {
    //         return false;
    //     }
    //     this._position = _position;
    //     return true;
    // }
    
    public string? GetName()
    {
        return _pieceName;
    }

    public bool SetName(string _pieceName)
    {
        if (this._pieceName == _pieceName)
        {
            return false;
        }
        this._pieceName = _pieceName;
        return true;
    }

    public int GetHealthPoint()
    {
        return _healthPoint;
    }

    public bool SetHealthPoint(int _healthPoint)
    {
        if (_healthPoint < 0)
        {
            return false;
        }
        this._healthPoint = _healthPoint;
        return true;
    }

    public int GetAttack()
    {
        return _attack;
    }

    public bool SetAttack(int _attack)
    {
        if (_attack < 0)
        {
            return false;
        }
        this._attack = _attack;
        return true;
    }

    public bool IsHealthZero()
    {
        if (_healthPoint != 0)
        {
            return false;
        }
        return true;

    }

    public int GetPrice()
    {
        return _price;
    }

    //* below methods might not be used
    public ArcheTypeEnum GetArcheType()
    {
        return _archeType;
    }


    public RarityEnum GetRarityEnum()
    {
        return _rarity;
    }
}
