using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour
{
    #region Paramètre de génération
    [Header("Paramètre de génération")]
    //L'object sur lequel seront générés les salles et là ou elle seront contunues
    [SerializeField] private Transform roomGenerator, roomsContainer;

    //nombre de salles que je veux générer 
    [Range(1, 300)] [SerializeField] private int roomCounterLimite;

    //écart entre les salles (dépend de la taille des salles)
    [SerializeField] private float moveIncrementHorizontal = 0;
    [SerializeField] private float moveIncrementVertical = 0;

    #endregion

    #region Listes des Salles
    [Header("Les listes de salles disponibles")]
    //liste des salles qui peuvent apparaitre 
    [SerializeField] private GameObject staringRoom;
    [SerializeField] private GameObject[] roomsLeftRight;
    [SerializeField] private GameObject[] roomsUpDown;
    [SerializeField] private GameObject[] roomsUpLeft;
    [SerializeField] private GameObject[] roomsUpRight;
    [SerializeField] private GameObject[] roomsDownLeft;
    [SerializeField] private GameObject[] roomsDownRight;
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

    void Start()
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
        //commence vers la droite
        nextDirection = Direction.Right;
    }

    private void GenerateNextRoom()
    {
        //Direction qu'il suit
        actualDirection = nextDirection;

        //Bouge dans cette direction
        Move(actualDirection);

        //Provient de la direction opposé
        comeFrom = actualDirection;
        InverseDirection(comeFrom);


        //regarde où il va ensuite
        DirectionRoll(nextDirection);
        if (nextDirection == comeFrom)
        {
            DirectionRoll(nextDirection);
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
    }

    #region Tool

    private void DirectionRoll(Direction dir)
    {
        diceRoll = Random.Range(1, 5);

        //Gauche 40%
        if (diceRoll == 1 || diceRoll == 2)
        {
            dir = Direction.Left;
        }
        //Gauche 40%
        else if (diceRoll == 3 || diceRoll == 4)
        {
            dir = Direction.Right;
        }
        //Gauche 20%
        else if (diceRoll == 5)
        {
            dir = Direction.Down;
        }

    }

    private void InverseDirection(Direction dir)
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

    }

    private void Move(Direction dir)
    {
        // Gauche
        if (dir == Direction.Left)
        {
            // bouge vers la gauche
            Vector3 moveLeft = new Vector2(-moveIncrementHorizontal, 0);
            roomGenerator.position += moveLeft;
        }
        //Droite
        else if (dir == Direction.Right)
        {
            // bouge vers la droite
            Vector3 moveLRight = new Vector2(moveIncrementHorizontal, 0);
            roomGenerator.position += moveLRight;
        }
        //Bas
        else if (dir == Direction.Down)
        {
            // bouge vers la gauche
            Vector3 moveLDown = new Vector2(0, -moveIncrementVertical);
            roomGenerator.position += moveLDown;
        }

    }

    private void RoomSpawning()
    {

        if(comeFrom == Direction.Left)
        {
            if (nextDirection == Direction.Right)
            {
                Instantiate(roomsLeftRight[0], roomsContainer,false);
            }
            else if (nextDirection == Direction.Down)
            {

            }

        }
        else if (comeFrom == Direction.Right)
        {
            if (nextDirection == Direction.Left)
            {

            }
            else if (nextDirection == Direction.Down)
            {

            }

        }
        else if (comeFrom == Direction.Up)
        {
            if (nextDirection == Direction.Left)
            {

            }
            else if (nextDirection == Direction.Right)
            {

            }
            else if (nextDirection == Direction.Down)
            {

            }
        }

    }

    #endregion

}
