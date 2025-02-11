using UnityEngine;

/// <summary>
/// ������������ ���� ������.
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] private GameFlow gameFlow; // ������ �� �������� ����� ����
    [SerializeField] private LayerMask groundLayer; // ���� ��� �������� ����� �� �����
    [SerializeField] private LayerMask moneyLayer;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // ��������� ������� ����� ������ ����
        {
            HandleTap();
        }
    }

    /// <summary>
    /// ������������ ��� ������.
    /// </summary>
    private void HandleTap()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, groundLayer))
        {
            Vector3 tapPosition = hit.point;
            gameFlow.PlaceStation(new Vector3(tapPosition.x, -2, tapPosition.z)); // ������������� ������� �� ����� ����
        }

        if(Physics.Raycast(ray,out RaycastHit money, Mathf.Infinity, moneyLayer))
        {
            Destroy(money.transform.gameObject);
            Debug.Log("������ �������");
        }
    }
}
