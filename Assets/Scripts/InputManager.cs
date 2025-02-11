using UnityEngine;

/// <summary>
/// Обрабатывает ввод игрока.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow; // Ссылка на основной поток игры
    [SerializeField] private LayerMask groundLayer; // Слой для проверки клика по земле
    [SerializeField] private LayerMask moneyLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Проверяем нажатие левой кнопки мыши
        {
            HandleTap();
        }
    }

    /// <summary>
    /// Обрабатывает тап игрока.
    /// </summary>
    private void HandleTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 tapPosition = hit.point;
            gameFlow.PlaceStation(new Vector3(tapPosition.x, -2, tapPosition.z)); // Устанавливаем станцию на месте тапа
        }

        if(Physics.Raycast(ray,out RaycastHit money, Mathf.Infinity, moneyLayer))
        {
            Destroy(money.transform.gameObject);
            Debug.Log("Деньги собраны");
        }
    }
}
