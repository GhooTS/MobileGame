using UnityEngine;

[RequireComponent(typeof(CoreCell))]
public class CoreCellCollectionSubscriber : MonoBehaviour
{
    public CoreCellDynamicCollection activeCells;
    public CoreCellDynamicCollection destroyedCells;
    private CoreCell coreCell;

    private void OnEnable()
    {
        if(coreCell == null) coreCell = GetComponent<CoreCell>();
        UpdateSubscription();
    }


    private void OnDisable()
    {
        activeCells.Unsubscribe(coreCell);
        destroyedCells.Unsubscribe(coreCell);
    }

    public void UpdateSubscription()
    {
        if (coreCell.Destroyed)
        {
            activeCells.Unsubscribe(coreCell);
            destroyedCells.Subscribe(coreCell);
        }
        else
        {
            activeCells.Subscribe(coreCell);
            destroyedCells.Unsubscribe(coreCell);
        }
    }
}