using UnityEngine;
public class TruckButton : MonoBehaviour
{
   [SerializeField] private TruckMouvement truck;

   public void Selection()
   {
      truck.Selected();
   }
}
