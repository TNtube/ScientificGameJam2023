using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] TruckMouvement[] trucks;
    public GameObject truck;
    public void SetTruck(GameObject _obj)
    {
        truck = _obj;
    }

    public void updateTruck()
    {
        foreach (TruckMouvement tm in trucks)
        {
            tm.isSelected = false;
        }

        truck = null;
    }
}
