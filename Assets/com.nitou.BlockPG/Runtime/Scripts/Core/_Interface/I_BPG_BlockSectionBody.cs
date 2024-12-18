using System.Collections.Generic;
using UnityEngine;

namespace nitou.BlockPG.Interface{

    public interface I_BPG_BlockSectionBody : ILayoutable {
        
        /// <summary>
        /// �����Z�N�V�����D
        /// </summary>
        I_BPG_BlockSection BlockSection { get; }
        
        /// <summary>
        /// �ڑ�����Ă���q�u���b�N�̃��X�g�D
        /// </summary>
        IReadOnlyList<I_BPG_Block> ChildBlocks { get; }
        
        /// <summary>
        /// �u���b�N�ڑ��̉۔���p�R���|�[�l���g�D
        /// </summary>
        I_BPG_Spot Spot { get;}

        /// <summary>
        /// Updates ChildBlocksCount and ChildBlocksArray with the current child blocks.
        /// </summary>
        void UpdateChildBlocks();
    }


    /// <summary>
    /// <see cref="I_BPG_BlockSectionBody"/>�^�̔ėp�I�Ȋg�����\�b�h�W�D
    /// </summary>
    public static class BPG_BlockSectionBody_Extensions {

        /// <summary>
        /// �Z�N�V�����ɑΏۃu���b�N��ڑ�����D
        /// </summary>
        public static void Append(this I_BPG_BlockSectionBody self, I_BPG_Block block, int siblingIndex = 0) {
            block.RectTransform.SetParent(self.Spot.RectTransform);
            block.RectTransform.SetSiblingIndex(siblingIndex);
            
            // �q�u���b�N���̍X�V
            block.SetParentSection(self.BlockSection);

            // �Z�N�V�������̍X�V
            self.BlockSection.UpdateLayout();

            Debug.Log($"Connect to {self.BlockSection.Block.RectTransform.name} [{block.RectTransform.GetSiblingIndex()}]");
        }

        /// <summary>
        /// �Z�N�V�����ɑΏۃu���b�N��ڑ�����D
        /// </summary>
        public static void AppendFirst(this I_BPG_BlockSectionBody self, I_BPG_Block block) {
            int siblingIndex = 0;
            self.Append(block, siblingIndex);
        }

        /// <summary>
        /// �Z�N�V�����ɑΏۃu���b�N��ڑ�����D
        /// </summary>
        public static void AppendLast(this I_BPG_BlockSectionBody self, I_BPG_Block block) {
            int siblingIndex = self.RectTransform.childCount;
            self.Append(block, siblingIndex);
        }
    }

}