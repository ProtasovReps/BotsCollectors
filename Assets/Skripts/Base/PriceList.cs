using UnityEngine;

public class PriceList : MonoBehaviour
{
    [SerializeField] private int _newUnitPrice = 3;
    [SerializeField] private int _newBasePrice = 5;

    public int NewUnitPrice => _newUnitPrice;
    public int NewBasePrice => _newBasePrice;
}
