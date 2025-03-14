using System.Collections.Generic;
using UnityEngine;

namespace nitou.BlockPG.Interface{

    /// <summary>
    /// 
    /// </summary>
    public interface I_BPG_BlockLayout : ILayoutable{

        /// <summary>
        /// �q�Z�N�V�����D
        /// </summary>
        IReadOnlyList<I_BPG_BlockSection> Sections { get; }

        /// <summary>
        /// �u���b�N�J���[�D.
        /// </summary>
        Color Color { get; set; }
    }
}
