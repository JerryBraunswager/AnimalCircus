using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Управляет поведением работника.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] private Animator animator; // Аниматор работника
    private NavMeshAgent agent;

    private Vector3 destination;

    public event Action Complited;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// Устанавливает точку назначения для работника.
    /// </summary>
    /// <param name="destination">Точка назначения.</param>
    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        agent.SetDestination(destination);
    }

    private void Update()
    {
        PerformWork();
    }

    /// <summary>
    /// Выполняет работу на станции.
    /// </summary>
    private void PerformWork()
    {
        animator.SetBool("IsWorking", true); // Включаем анимацию работы
        Invoke("FinishWork", 3f); // Через 3 секунды завершаем работу
    }

    /// <summary>
    /// Завершает работу и ждет посетителя.
    /// </summary>
    private void FinishWork()
    {
        animator.SetBool("IsWorking", false); // Отключаем анимацию работы
        Debug.Log("Работа завершена! Ждем посетителя.");
        Complited?.Invoke();
    }
}
