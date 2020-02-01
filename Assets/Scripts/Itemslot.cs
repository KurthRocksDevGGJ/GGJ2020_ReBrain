using UnityEngine;

namespace Photon.Pun.Demo.PunBasics
{
    public class Itemslot : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private GameObject template;
        private ItemDragHandler item;
        
        private void Start()
        {        
            createNewInstance();
            setPosToZero();
        }
        
        private void setPosToZero()
        {
            item.ResetPosition();
        }

        private void createNewInstance()
        {
            var instance = Instantiate(template, transform, false);
            item = instance.GetComponent<ItemDragHandler>();
        }
    }
}