using UnityEngine;

namespace nitou.BlockPG.DragDrop{
    using nitou.BlockPG.Interface;
    using nitou.BlockPG.Block;
    using UnityEngine.EventSystems;

    public class BPG_BlockDraggingWithConnection : BPG_BlockDraggingBase {

        private I_BPG_Spot _currentSpot;


        /// ----------------------------------------------------------------------------
        // Interface Method

        public override void OnBegineDrag(PointerEventData eventData) {
            // append to dagging layer
            _system.AssignToDraggingPanel(this);
        }

        public override void OnDrag(PointerEventData eventData) {
            // detect candidate spot
            DetectConectableBlockSpot();
        }

        public override void OnEndDrag(PointerEventData eventData) {

            // �ڑ��Ώۂ̃u���b�N�����݂���ꍇ�A
            if (_currentSpot != null) {
                HandleDropToCurrentSpot();
            }
            // �ڑ��Ώۂ̃u���b�N�����݂��Ȃ��ꍇ�A
            else {
                DropToRaycastedFreeSpot(eventData);
            }

            // 
            AdjustTransformPositionAndRotation();
            _currentSpot = null;
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ���݂̃|�C���^�[���W�ł�<see cref="I_BE2_Spot">�X�|�b�g</see>�����o����
        /// ��<see cref="BE2_DragDropManager"/>�ɂ���ăt���[���I�����Ɏ��s�����
        /// </summary>
        private void DetectConectableBlockSpot() {
            
            // 
            var spot = _system.FindClosestSpotForBlock(this, _system.DetectionDistance);

            //// �ڑ��۔���̃f���R�[�h
            //if (spot != null && spot.Block != null && this.Block.IsConditionBlock()) {
            //    bool canConnect = true;

            //    // Condition�u���b�N����q�ɂ��邱�Ƃ͏o���Ȃ�
            //    if (spot.Block.IsConditionBlock() && spot is not BE2_SpotOuterArea) {
            //        canConnect = false;
            //    }

            //    // 
            //    else if (spot.Block.ParentSection != null && spot.Block.ParentSection.Block.IsConditionBlock()) {
            //        canConnect = false;
            //    }

            //    // �ڑ��s�̏ꍇ,
            //    if (!canConnect) {
            //        _dragDropManager.GhostBlock.Hide();
            //        _dragDropManager.SetCurrentSpot(null);
            //        return;
            //    }
            //}

            // BlockBody
            if (spot is BPG_SpotBlockBody && spot.Block != this.Block) {
                _system.GhostBlock.Show(
                    parent: spot.RectTransform,
                    localScale: Vector3.one,
                    siblingIndex: 0);

                // cache spot
                _currentSpot = spot;
            }
            // OuterArea (�u���b�N����)
            else if (spot is BPG_SpotOuterArea) {
                _system.GhostBlock.Show(
                    parent: spot.Block.RectTransform.parent,
                    localScale: Vector3.one,
                    siblingIndex: spot.Block.RectTransform.GetSiblingIndex() + 1);  // ���Ώۃu���b�N�̈���ɔz�u����

                // 
                spot.Block.ParentSection.UpdateLayout();

                // cache spot
                _currentSpot = spot;
            }
            // ���̑�
            else {
                _system.GhostBlock.Hide();
                _currentSpot = null;
            }

            // Debug.Log($"current spot is null : {_dragDropManager.CurrentSpot is null}");
        }

        /// <summary>
        /// Spot�ɔz�u����
        /// </summary>
        private void HandleDropToCurrentSpot() {
            var spot = _currentSpot;

            // block body
            if (spot is BPG_SpotBlockBody spotBlockBody) {
                this.Block.ConnectTo(spotBlockBody);
            } 
            // block outer area
            else if(spot is BPG_SpotOuterArea spotOuterArea) {
                this.Block.InsertNextTo(spotOuterArea);
            }

            _currentSpot = null;
        }
    }
}
