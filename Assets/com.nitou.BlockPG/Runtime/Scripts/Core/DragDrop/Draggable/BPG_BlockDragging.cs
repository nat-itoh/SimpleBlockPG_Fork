using UnityEngine;
using UnityEngine.EventSystems;

namespace nitou.BlockPG.DragDrop{
    using nitou.BlockPG.Interface;
    using nitou.BlockPG.Events;

    public class BPG_BlockDragging : BPG_BlockDraggingBase {

        /// ----------------------------------------------------------------------------
        // Interface Method

        /// <summary>
        /// �h���b�O�J�n�����D
        /// </summary>
        public override void OnBegineDrag(PointerEventData eventData) {
            // Append to dagging layer
             _system.AssignToDraggingPanel(this);
        }

        /// <summary>
        /// �h���b�O�����D
        /// </summary>
        public override void OnDrag(PointerEventData eventData) { }

        /// <summary>
        /// �h���b�O�I�������D
        /// </summary>
        public override void OnEndDrag(PointerEventData eventData) {

            if (DropToRaycastedFreeSpot(eventData)) { 
                AdjustTransformPositionAndRotation();
            }
        }

    }
}
