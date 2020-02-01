using UnityEngine;

public class Itemslot : MonoBehaviour
{
    [SerializeField]
    private GameObject template;
    private ItemDragHandler item;
    


    private void Start()
    {
        
        createNewInstance();
        setPosToZero();
        
    }

    private bool InstanceInDropArea()
    {
        var dropArea = transform.parent as RectTransform;
        return dropArea.rect.Contains(item.transform.position);
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
    private void Update()
    {
        if (!item.IsMoved)
        {
            return;
        }
        if (InstanceInDropArea() )
        {
            createNewInstance();
            setPosToZero();
        }
        else 
        {
            item.ResetPosition();
        }
        
    }

}