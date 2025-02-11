using UnityEngine;

/// <summary>
/// ��������� �������� ������� ����.
/// </summary>
public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject stationPrefab; // ������������ �������
    [SerializeField] private GameObject workerPrefab;  // ������������ ���������
    [SerializeField] private GameObject visitorPrefab; // ������������ ����������

    private GameObject currentStation; // ������� �������
    private WorkerController currentWorker; // ������� ��������

    /// <summary>
    /// ������� ����� ������� �� ��������� �������.
    /// </summary>
    /// <param name="position">������� ��� ��������� �������.</param>
    public void PlaceStation(Vector3 position)
    {
        if (currentStation != null)
        {
            Debug.LogWarning("������� ��� �����������!");
            return;
        }

        currentStation = Instantiate(stationPrefab, position, Quaternion.identity);
        SpawnWorker();
    }

    /// <summary>
    /// ������� ������ ��������� � ���������� ��� � �������.
    /// </summary>
    private void SpawnWorker()
    {
        if (currentStation == null)
        {
            Debug.LogError("������� �� �����������!");
            return;
        }

        GameObject worker = Instantiate(workerPrefab, new Vector3(currentStation.transform.position.x + 3, -2, currentStation.transform.position.z + 3) , Quaternion.identity);
        currentWorker = worker.GetComponent<WorkerController>();
        currentWorker.Complited += SpawnVisitor;
        currentWorker.SetDestination(currentStation.transform.position); // ���������� ��������� � �������
    }

    /// <summary>
    /// ������� ������ ���������� � ���������� ��� � �������.
    /// </summary>
    private void SpawnVisitor()
    {
        if (currentStation == null || currentWorker == null)
        {
            Debug.LogError("������� ��� �������� ����������!");
            return;
        }

        Vector3 position = new Vector3(-7, -2, -2);
        GameObject visitor = Instantiate(visitorPrefab, position, Quaternion.identity);
        VisitorController visitorController = visitor.GetComponent<VisitorController>();
        visitorController.SetDestination(currentStation.transform.position); // ���������� ���������� � �������
        visitorController.Delivered += VisitorController_Delivered;
        currentWorker.Complited -= SpawnVisitor;
    }

    private void VisitorController_Delivered()
    {
        currentWorker.Complited += SpawnVisitor;
    }
}
