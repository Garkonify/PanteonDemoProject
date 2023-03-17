using UnityEngine;

public interface ISelectable
{
    void OnSelected();
    void OnDeselected();
    void RightClickOrderFree(GridNode node);
    void RightClickOrderOccupied(GridNode node);
}
