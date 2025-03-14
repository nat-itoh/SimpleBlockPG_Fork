using UnityEngine;

namespace nitou.BlockPG.Blocks.Section {
    using nitou.BlockPG.Interface;

    /// <summary>
    /// <see cref="BPG_BlockSectionHeader"/>�����ɔz�u����郌�C�A�E�g�v�f�D
    /// </summary>
    public sealed class BPG_BlockSectionHeader_Item : BPG_ComponentBase, 
        I_BPG_BlockSectionHeaderItem {

        /// <summary>
        /// �T�C�Y���D
        /// </summary>
        public Vector2 Size => RectTransform.sizeDelta;
    }
}
