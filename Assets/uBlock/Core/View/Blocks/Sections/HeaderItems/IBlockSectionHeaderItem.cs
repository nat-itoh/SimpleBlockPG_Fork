using UnityEngine;

namespace Nitou.BlockPG.View {

    public interface IBlockSectionHeaderItem {

        /// <summary>
        /// 
        /// </summary>
        RectTransform RectTransform { get; }

        /// <summary>
        /// �T�C�Y���D
        /// </summary>
        Vector2 Size { get; }
    }
}
