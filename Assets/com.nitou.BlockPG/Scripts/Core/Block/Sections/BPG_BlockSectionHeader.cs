using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace nitou.BlockPG.Block.Section {
    using nitou.BlockPG.Interface;
    using System.Collections.Generic;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Image))]
    //[RequireComponent(typeof(Shadow))]
    public sealed class BPG_BlockSectionHeader : BPG_ComponentBase, I_BPG_BlockSectionHeader {

        // refecences (self)
        private Image _image;
        
        // references (parent)
        private I_BPG_BlockSection _section;
        private I_BPG_BlockLayout _blockLayout;
        
        // references (children)
        private readonly List<I_BPG_BlockSectionHeaderItem> _items = new();

        [SerializeField] float _minHeight = 105f;
        [SerializeField] float _minWidht = 105f;
        [SerializeField] float _paddingRight = 0f;
        [SerializeField] float _spacing = 15f;


        /// <summary>
        /// 
        /// </summary>
        public Vector2 Size {
            get => RectTransform.sizeDelta;
            set => RectTransform.sizeDelta = value;
        }

        /// <summary>
        /// Header items exist in target section header.
        /// </summary>
        public IList<I_BPG_BlockSectionHeaderItem> Items => _items;


        /// ----------------------------------------------------------------------------

        private void Awake() {
            GatherComponents();

            if (_image != null) {
                _image.type = Image.Type.Sliced;
                _image.pixelsPerUnitMultiplier = 2;
            }
            
            UpdateItems();
            UpdateInputs();
        }


        /// ----------------------------------------------------------------------------
        // Public Method

        /// <summary>
        /// Updates the layout of an individual block header. Used to correctly resize the body after adding operation blocks
        /// </summary>
        [ContextMenu("Update Layout")]
        public void UpdateLayout() {
#if UNITY_EDITOR
            if (!EditorApplication.isPlaying) {
                UpdateItems();
            }
#endif

            ApplyColor();
            UpdateSelfSize();
        }

        /// <summary>
        /// Updates the ItemsArray with all the current I_BE2_BlockSectionHeaderItem (labels and inputs) in the header
        /// </summary>
        public void UpdateItems() {
            _items.Clear();

            foreach (Transform chiled in transform) {
                if(chiled.TryGetComponent<I_BPG_BlockSectionHeaderItem>(out var item)
                    && item.RectTransform.gameObject.activeSelf) {
                        _items.Add(item);
                }                
            }
        }

        /// <summary>
        /// Updates the InputsArray with all the current I_BE2_BlockSectionHeaderInput (inputs only) in the header 
        /// </summary>
        public void UpdateInputs() {

        }


        /// ----------------------------------------------------------------------------
        // Private Method

        private void GatherComponents() {

            _image = GetComponent<Image>();

            // parents
            _section = transform.parent.GetComponent<I_BPG_BlockSection>();
            _blockLayout = transform.parent.parent.GetComponent<I_BPG_BlockLayout>();
        }

        private void ApplyColor() {
            if (_image.sprite != null && _blockLayout != null) {
                _image.color = _blockLayout.Color;
            }
        }

        private void UpdateSelfSize() {
            bool isFirstSection = _section.RectTransform.GetSiblingIndex() == 0;

            float width = 0, height = 0;
            if (isFirstSection) {
                // width
                float w = _items.Sum(item => item.Size.x + _spacing) + _spacing + _paddingRight;
                width = Mathf.Max(_minWidht, w);

                // height
                float h = _items.Any() ? _items.Max(item => item.Size.y) : 0;
                height = Mathf.Max(_minHeight, (_minHeight - 40) + h);

            } else {
                width = _blockLayout.Sections[0].Header.Size.x;
                height = RectTransform.sizeDelta.y;
            }

            // apply
            RectTransform.sizeDelta = new Vector2(width, height);
        }


        /// ----------------------------------------------------------------------------
#if UNITY_EDITOR
        private void OnValidate() {
            GatherComponents();
        }
#endif
    }
}


public static class TransformExtensions {

    /// <summary>
    /// �q�v�f�̂� IEnumerable<Transform> �Ƃ��ĕԂ��܂��B
    /// </summary>
    public static IEnumerable<Transform> GetChildren(this Transform transform) {
        return transform.Cast<Transform>();
    }

}