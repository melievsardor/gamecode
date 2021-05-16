using UnityEngine;
using UnityEngine.EventSystems;

namespace AlphabetBook
{
    public class ItemDropHandlerBase : MonoBehaviour, IDropHandler
    {
        public Transform parentTransform;


        protected IItemDropHandler drop;

        private void Start()
        {
            drop = GetComponentInParent<IItemDropHandler>();

            if (parentTransform == null)
                parentTransform = transform;

        }



        protected virtual void OnDrop()
        {
            
        }

        public void OnDrop(PointerEventData eventData)
        {
            OnDrop();
        }
    }

}

