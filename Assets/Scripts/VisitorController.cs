using System;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

/// <summary>
/// Управляет поведением посетителя.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class VisitorController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Аниматор посетителя
    [SerializeField] private GameObject moneyPrefab; // Предфабрикат денег

    private NavMeshAgent agent;
    private bool isSpawned = false;

    private Vector3 destination;

    public event Action Delivered;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Устанавливает точку назначения для посетителя.
    /// </summary>
    /// <param name="destination">Точка назначения.</param>
    public void SetDestination(Vector3 destination)
    {
        Debug.Log(destination + " " + transform.position);
        this.destination = destination;
        agent.SetDestination(destination);
    }

    private void Update()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            // Посетитель достиг точки назначения
            DeliverMoney();
        }
    }

    /// <summary>
    /// Оставляет деньги на станции.
    /// </summary>
    private void DeliverMoney()
    {
        Invoke("SpawnMoney", 2f); // Через 2 секунды создаем объект денег
    }

    /// <summary>
    /// Создает объект денег на станции.
    /// </summary>
    private void SpawnMoney()
    {
        if(!isSpawned) 
        {
            Instantiate(moneyPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            isSpawned = true;
            Delivered?.Invoke();
        }

        Destroy(gameObject); // Удаляем посетителя после завершения действия
    }
}
