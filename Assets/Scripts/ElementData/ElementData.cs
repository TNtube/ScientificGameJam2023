using UnityEngine;
using UnityEngine.EventSystems;

public class ElementData : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
{
    // Base a revoir 
    [SerializeField] private string name;
    [SerializeField] private int ressource;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Here");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Nous sommessur un batiment ! ");
    }
}
