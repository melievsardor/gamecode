using UnityEngine;
using UnityEngine.EventSystems;

public sealed class RadialListViewport : MonoBehaviour, IDragHandler
{
    [SerializeField]
    private RadialList _rl;

    public void OnDrag(PointerEventData eventData)
    {
        float dirFactor = 1f;

        if (_rl.invert)
            dirFactor = -dirFactor;

        _rl.Rotation += eventData.delta.x * Time.deltaTime * _rl.sensitivity * dirFactor;
        _rl.UpdateListView();
    }

}
