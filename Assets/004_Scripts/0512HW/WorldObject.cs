using UnityEngine;

public class WorldObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string _name;
    public void Descrption()
    {
        Debug.Log($"[ E ] {_name} 상호작용");
    }

}
