using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// ��������� ���������� ���������.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class WorkerController : MonoBehaviour
{
    [SerializeField] private Animator animator; // �������� ���������
    private NavMeshAgent agent;

    private Vector3 destination;

    public event Action Complited;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// ������������� ����� ���������� ��� ���������.
    /// </summary>
    /// <param name="destination">����� ����������.</param>
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
    /// ��������� ������ �� �������.
    /// </summary>
    private void PerformWork()
    {
        animator.SetBool("IsWorking", true); // �������� �������� ������
        Invoke("FinishWork", 3f); // ����� 3 ������� ��������� ������
    }

    /// <summary>
    /// ��������� ������ � ���� ����������.
    /// </summary>
    private void FinishWork()
    {
        animator.SetBool("IsWorking", false); // ��������� �������� ������
        Debug.Log("������ ���������! ���� ����������.");
        Complited?.Invoke();
    }
}
