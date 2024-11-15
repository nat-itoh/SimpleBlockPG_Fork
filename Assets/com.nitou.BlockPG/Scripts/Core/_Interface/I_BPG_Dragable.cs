using UnityEngine;
using UnityEngine.EventSystems;

namespace nitou.BlockPG.Interface
{
    public interface I_BPG_Dragable {

        public RectTransform RectTransform { get; }

        public Vector2 RayPoint { get; }

        public I_BPG_Block Block {get;}
    }
}
