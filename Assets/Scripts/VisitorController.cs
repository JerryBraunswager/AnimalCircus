using System;
using UnityEngine;
using UnityEngine.AI;
using static Unity.VisualScripting.Antlr3.Runtime.Tree.TreeWizard;

/// <summary>
/// ��������� ���������� ����������.
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class VisitorController : MonoBehaviour
{
    [SerializeField] private Animator animator; // �������� ����������
    [SerializeField] private GameObject moneyPrefab; // ������������ �����

    private NavMeshAgent agent;
    private bool isSpawned = false;

    private Vector3 destination;

    public event Action Delivered;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    /// <summary>
    /// ������������� ����� ���������� ��� ����������.
    /// </summary>
    /// <param name="destination">����� ����������.</param>
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
            // ���������� ������ ����� ����������
            DeliverMoney();
        }
    }

    /// <summary>
    /// ��������� ������ �� �������.
    /// </summary>
    private void DeliverMoney()
    {
        Invoke("SpawnMoney", 2f); // ����� 2 ������� ������� ������ �����
    }

    /// <summary>
    /// ������� ������ ����� �� �������.
    /// </summary>
    private void SpawnMoney()
    {
        if(!isSpawned) 
        {
            Instantiate(moneyPrefab, transform.position + Vector3.up * 0.5f, Quaternion.identity);
            isSpawned = true;
            Delivered?.Invoke();
        }

        Destroy(gameObject); // ������� ���������� ����� ���������� ��������
    }
}
