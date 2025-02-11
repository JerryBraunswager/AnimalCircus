using UnityEngine;

/// <summary>
/// Управляет основным потоком игры.
/// </summary>
public class GameFlow : MonoBehaviour
{
    [SerializeField] private GameObject stationPrefab; // Предфабрикат станции
    [SerializeField] private GameObject workerPrefab;  // Предфабрикат работника
    [SerializeField] private GameObject visitorPrefab; // Предфабрикат посетителя

    private GameObject currentStation; // Текущая станция
    private WorkerController currentWorker; // Текущий работник

    /// <summary>
    /// Создает новую станцию на указанной позиции.
    /// </summary>
    /// <param name="position">Позиция для установки станции.</param>
    public void PlaceStation(Vector3 position)
    {
        if (currentStation != null)
        {
            Debug.LogWarning("Станция уже установлена!");
            return;
        }

        currentStation = Instantiate(stationPrefab, position, Quaternion.identity);
        SpawnWorker();
    }

    /// <summary>
    /// Создает нового работника и отправляет его к станции.
    /// </summary>
    private void SpawnWorker()
    {
        if (currentStation == null)
        {
            Debug.LogError("Станция не установлена!");
            return;
        }

        GameObject worker = Instantiate(workerPrefab, new Vector3(currentStation.transform.position.x + 3, -2, currentStation.transform.position.z + 3) , Quaternion.identity);
        currentWorker = worker.GetComponent<WorkerController>();
        currentWorker.Complited += SpawnVisitor;
        currentWorker.SetDestination(currentStation.transform.position); // Отправляем работника к станции
    }

    /// <summary>
    /// Создает нового посетителя и отправляет его к станции.
    /// </summary>
    private void SpawnVisitor()
    {
        if (currentStation == null || currentWorker == null)
        {
            Debug.LogError("Станция или работник недоступны!");
            return;
        }

        Vector3 position = new Vector3(-7, -2, -2);
        GameObject visitor = Instantiate(visitorPrefab, position, Quaternion.identity);
        VisitorController visitorController = visitor.GetComponent<VisitorController>();
        visitorController.SetDestination(currentStation.transform.position); // Отправляем посетителя к станции
        visitorController.Delivered += VisitorController_Delivered;
        currentWorker.Complited -= SpawnVisitor;
    }

    private void VisitorController_Delivered()
    {
        currentWorker.Complited += SpawnVisitor;
    }
}
