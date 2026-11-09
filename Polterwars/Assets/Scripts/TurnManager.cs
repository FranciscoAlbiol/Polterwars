
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    // Tipos de turno disponibles
    public enum TurnType
    {
        Player,
        AI
    }

    public static TurnManager Instance { get; private set; }

    [Header("Turn Settings")]
    [SerializeField] private TurnType currentTurn = TurnType.Player;
    [SerializeField] private int currentRound = 1;

    [Header("Action Settings")]
    [SerializeField] private int playerActionsPerTurn = 3;
    private int remainingPlayerActions;

    // Propiedades de acceso
    public TurnType CurrentTurn => currentTurn;
    public int CurrentRound => currentRound;
    public int RemainingPlayerActions => remainingPlayerActions;

    // Avisa cuando cambia el turno
    public event Action<TurnType, int> OnTurnChanged;

    private void Update(){
        // Solo permitir finalizar el turno de la IA con Space
        if (IsAITurn() && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("La IA ha terminado su turno.");

            EndTurn();
        }
    }

    private void Awake()
    {
        // Evita que existan varios TurnManagers
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        StartGame();
    }

    // Iniciar partida
    public void StartGame()
    {
        currentRound = 1;
        currentTurn = TurnType.Player;

        StartTurn();
    }

    // Iniciar turno actual
    private void StartTurn(){
        // Si comienza el turno del jugador, recuperar sus acciones
        if (currentTurn == TurnType.Player)
        {
            remainingPlayerActions = playerActionsPerTurn;
        }

        Debug.Log("================================");
        Debug.Log("RONDA: " + currentRound);
        Debug.Log("TURNO: " + currentTurn);

        if (currentTurn == TurnType.Player)
        {
            Debug.Log("Acciones disponibles: "
                + remainingPlayerActions);
        }

        Debug.Log("================================");

        OnTurnChanged?.Invoke(currentTurn, currentRound);
    }

    // Finalizar turno actual
    public void EndTurn()
    {
        if (currentTurn == TurnType.Player)
        {
            currentTurn = TurnType.AI;
        }
        else
        {
            currentTurn = TurnType.Player;
            currentRound++;
        }

        StartTurn();
    }

    public void PerformPlayerAction(){

        // Comprobar que es el turno del jugador
        if (!IsPlayerTurn())
        {
            Debug.Log("No es el turno del jugador.");
            return;
        }

        // Comprobar que quedan acciones
        if (remainingPlayerActions <= 0)
        {
            Debug.Log("No quedan acciones disponibles.");
            return;
        }

        // Consumir una acción
        remainingPlayerActions--;

        Debug.Log("Acción realizada. Acciones restantes: "
            + remainingPlayerActions);

        // Si no quedan acciones, finalizar turno
        if (remainingPlayerActions == 0){
            Debug.Log("El jugador ha agotado sus acciones.");
            EndTurn();
        }
    }

    // Comprobar de quién es el turno
    public bool IsPlayerTurn()
    {
        return currentTurn == TurnType.Player;
    }

    public bool IsAITurn()
    {
        return currentTurn == TurnType.AI;
    }
}
