using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    #region Paramètre de génération
    [Header("Paramètre de génération")]
    //L'object sur lequel seront générés les salles et là ou elle seront contunues
    [SerializeField] private Transform roomGenerator = null, roomsContainer = null;

    //nombre de salles que je veux générer 
    [Range(1, 300)] [SerializeField] private int roomCounterLimite = 30;

    //écart entre les salles (dépend de la taille des salles)
    [SerializeField] private float moveIncrementHorizontal = 0;
    [SerializeField] private float moveIncrementVertical = 0;

    #endregion

    #region Listes des Salles
    [Header("Les listes de salles disponibles")]
    //liste des salles qui peuvent apparaitre 
    [SerializeField] private GameObject staringRoom = null;
    [SerializeField] private GameObject[] roomsLeftToRight = null;
    [SerializeField] private GameObject[] roomsRightToLeft = null;
    [SerializeField] private GameObject[] roomsUpToDown = null;
    [SerializeField] private GameObject[] roomsUpToLeft = null;
    [SerializeField] private GameObject[] roomsUpToRight = null;
    [SerializeField] private GameObject[] roomsLeftToDown = null;
    [SerializeField] private GameObject[] roomsRightToDown = null;
    [SerializeField] private GameObject roomsLeftToEnd = null;
    [SerializeField] private GameObject roomsRightToEnd = null;
    [SerializeField] private GameObject roomsUpToEnd = null;
    #endregion

    #region Pour le fonctionnement en interne
    [Header("Inside")]
    //nombre de salles générées
    [SerializeField] private int roomCounter = 1;
    //Enum des directions possibles
    private enum Direction  {Down, Left, Right, Up};
    //direction du générateur pour ce déplacer
    private Direction comeFrom;
    private Direction actualDirection;
    private Direction nextDirection;
    //Roll direction
    [SerializeField] private int diceRoll = 0;

    #endregion

    private void Start()
    {
        Initialisation();

        //tant que toute les salles ne sont pas crées
        for (roomCounter = 0; roomCounter < roomCounterLimite; roomCounter++)
        {
            GenerateNextRoom();
        }

        TheEnd();
    }

    private void Initialisation()
    {
        Instantiate(roomsLeftToRight[Random.Range(0, roomsLeftToRight.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);

        //commence vers la droite
        nextDirection = Direction.Right;
        comeFrom = Direction.Left;
    }

    private void GenerateNextRoom()
    {
        //Direction qu'il suit
        actualDirection = nextDirection;

        //Bouge dans cette direction
        Move(actualDirection);

        //Provient de la direction opposé
        comeFrom = actualDirection;
        comeFrom = InverseDirection(comeFrom);


        //regarde où il va ensuite
        nextDirection = DirectionRoll(nextDirection);
        while (nextDirection == comeFrom)
        {
            nextDirection = DirectionRoll(nextDirection);
        }

        //Fait apparaitre la salle
        RoomSpawning();
    }

    private void TheEnd()
    {
        //Direction qu'il suit
        actualDirection = nextDirection;
        //Bouge dans cette direction
        Move(actualDirection);
        //Provient de la direction opposé
        comeFrom = actualDirection;
        InverseDirection(comeFrom);

        //instancier la dernière room selon là d'ou l'on vient
        if (comeFrom == Direction.Left)
        {
                Instantiate(roomsLeftToEnd, roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
        }
        else if (comeFrom == Direction.Right)
        {
                Instantiate(roomsRightToEnd, roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
        }
        else if (comeFrom == Direction.Up)
        {
                Instantiate(roomsUpToEnd, roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
        }

    }

    #region Tool

    private Direction DirectionRoll(Direction dir)
    {
        diceRoll = Random.Range(1, 7);

        //Gauche 33%
        if (diceRoll == 1 || diceRoll == 2)
        {
            dir = Direction.Left;
        }
        //Gauche 33%
        else if (diceRoll == 3 || diceRoll == 4)
        {
            dir = Direction.Right;
        }
        //Gauche 33%
        else if (diceRoll == 5 || diceRoll == 6)
        {
            dir = Direction.Down;
        }

        return dir;
    }

    private Direction InverseDirection(Direction dir)
    {
        if (dir == Direction.Left)
        {
            dir = Direction.Right;
        }
        else if (dir == Direction.Right)
        {
            dir = Direction.Left;
        }
        else if (dir == Direction.Down)
        {
            dir = Direction.Up;
        }
        else if (dir == Direction.Up)
        {
            dir = Direction.Down;
        }

        return dir;
    }

    private void Move(Direction dir)
    {
        // Gauche
        if (dir == Direction.Left)
        {
            // bouge vers la gauche
            Vector3 moveLeft = new Vector3(-moveIncrementHorizontal, 0,0);
            roomGenerator.position += moveLeft;
        }
        //Droite
        else if (dir == Direction.Right)
        {
            // bouge vers la droite
            Vector3 moveRight = new Vector3(moveIncrementHorizontal, 0,0);
            roomGenerator.position += moveRight;
        }
        //Bas
        else if (dir == Direction.Down)
        {
            // bouge vers la gauche
            Vector3 moveDown = new Vector3(0, -moveIncrementVertical,0);
            roomGenerator.position += moveDown;
        }

    }

    private void RoomSpawning()
    {

        if(comeFrom == Direction.Left)
        {
            if (nextDirection == Direction.Right)
            {
                Instantiate(roomsLeftToRight[Random.Range(0, roomsLeftToRight.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }
            else if (nextDirection == Direction.Down)
            {
                Instantiate(roomsLeftToDown[Random.Range(0, roomsLeftToDown.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }

        }
        else if (comeFrom == Direction.Right)
        {
            if (nextDirection == Direction.Left)
            {
                Instantiate(roomsRightToLeft[Random.Range(0, roomsRightToLeft.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }
            else if (nextDirection == Direction.Down)
            {
                Instantiate(roomsRightToDown[Random.Range(0, roomsRightToDown.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }

        }
        else if (comeFrom == Direction.Up)
        {
            if (nextDirection == Direction.Left)
            {
                Instantiate(roomsUpToLeft[Random.Range(0, roomsUpToLeft.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }
            else if (nextDirection == Direction.Right)
            {
                Instantiate(roomsUpToRight[Random.Range(0, roomsUpToRight.Length)], roomGenerator.position, new Quaternion(0, 0, 0, 0), roomsContainer);
            }
            else if (nextDirection == Direction.Down)
            {
                Instantiate(roomsUpToDown[Random.Range(0, roomsUpToDown.Length)], roomGenerator.position, new Quaternion (0,0,0,0), roomsContainer);

            }
        }

    }

    #endregion
}