using UnityEngine;

namespace Nitou.BlockPG.View {

    public class BlockSectionHeader_Item : ComponentBase ,
        IBlockSectionHeaderItem {

        /// <summary>
        /// �T�C�Y���D
        /// </summary>
        public Vector2 Size => RectTransform.sizeDelta;
    }

}
