using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace nitou.BlockPG.Interface{

    public interface I_BPG_BlockSection : ILayoutable {
        
        I_BPG_Block Block { get; }

        I_BPG_BlockSectionHeader Header { get; }

        I_BPG_BlockSectionBody Body { get; }
    }


    /// <summary>
    /// Basic extension methods for type of <see cref="I_BPG_BlockSection"/>.
    /// </summary>
    public static class BPG_BlockSection_Extensions {

        // ----------------------------------------------------------------------------
        #region Info

        /// <summary>
        /// 
        /// </summary>
        public static int GetSectionIndex(this I_BPG_BlockSection self) {
            return self.RectTransform.GetSiblingIndex();
        }

        #endregion


        /// ----------------------------------------------------------------------------
        #region Getter

        /// <summary>
        /// Body�����̃u���b�N���擾����
        /// </summary>
        public static IEnumerable<I_BPG_Block> GetBodyBlocks(this I_BPG_BlockSection self) {
            return self.Body.ChildBlocks.Where(b => b != null);
        }

        /// <summary>
        /// �q�K�w�ȉ���<see cref="I_BPG_Block"/>���ċA�I�Ɏ擾����
        /// </summary>
        public static List<I_BPG_Block> GetAllChaildBlocks(this I_BPG_BlockSection self) {
            if (self.Body == null) return new List<I_BPG_Block>();

            // �q�K�w�ȉ�
            return self.GetBodyBlocks()
                .SelectMany(block => block.GetAllChaildBlocks())
                .ToList();
        }

        /// <summary>
        /// �q�K�w�ȉ���<see cref="I_BPG_Block"/>�̐����擾����
        /// </summary>
        public static int GetAllChaildBlocksCount(this I_BPG_BlockSection self) {
            if (self == null || self.Body == null) return 0;

            // Block�̑���
            return self.GetBodyBlocks()
                .Select(block => block.GetAllChaildBlocksCount())
                .Sum();
        }


        /// <summary>
        /// Obtains a reference to the parent section to which it belongs.
        /// </summary>
        //public static I_BPG_Block GetParentBlock(this I_BPG_BlockSection self) {
        //    return (self.ParentSection != null) ? self.ParentSection.Block : null;
        //}

        #endregion
    }
}
