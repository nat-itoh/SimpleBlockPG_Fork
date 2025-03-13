using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nitou.BlockPG.View {

    [DisallowMultipleComponent]
    public abstract class Block : ComponentBase {

        public Block Parent { get; internal set; }

        public IEnumerable<Block> Children { get; }
    }



    public static partial class BlockExtensions {

    }
}
