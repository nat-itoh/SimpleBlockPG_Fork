using System;
using System.Xml.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using nitou.BlockPG.Interface;

namespace nitou.BlockPG.Serialization {

    public static class BPG_BlockSerializer {

        /// ----------------------------------------------------------------------------
        #region Public Method (Convert : Blocks => XML)

        /// <summary>
        /// Convert <see cref="I_BPG_Block">block</see> to XML element.
        /// </summary>
        public static XElement BlockToXML(I_BPG_Block block) {
            if (block == null)
                throw new System.ArgumentNullException(nameof(block));

            var sBlock = BlockToSerializableBlock(block);
            return SerializableBlock.ToXElement(sBlock);
        }

        /// <summary>
        /// Convert <see cref="I_BE2_Block">block</see> to serializable block.
        /// </summary>
        public static SerializableBlock BlockToSerializableBlock(I_BPG_Block block) {

            // serializable block
            var sBlock = new SerializableBlock(
                id: block.GetGameObjectID(),
                name: block.RectTransform.name,
                localPosition: block.RectTransform.localPosition);

            // Custom Data
            //sBlock.blockIns = block.GetAdditonalData();

            // Section conversion
            foreach (var section in block.Layout.Sections) {

                var sSection = new SerializableBlockSection();

                // input
                //foreach (var input in section.Header.Inputs) {

                //    if (input.Transform.TryGetComponent<I_BPG_Block>(out _)) {
                //        Debug.LogWarning("�u���b�N�̓��̓X�|�b�g�ɑ��u���b�N���ݒ肳��Ă��܂��B");
                //    }

                //    sSection.inputs.Add(new SerializableInput(input.InputValues.stringValue));
                //}

                // blocks
                if (section.Body != null) {
                    foreach (var childBlock in section.Body.ChildBlocks) {
                        sSection.childBlocks.Add(BlockToSerializableBlock(childBlock));
                    }
                }

                // Register to parent block
                sBlock.sections.Add(sSection);
            }
            return sBlock;
        }
        #endregion


        /// ----------------------------------------------------------------------------
        #region Public Method (Convert : XML => Blocks)


        public static I_BPG_Block SerializableBlockToBlock(SerializableBlock sBlock, I_BPG_ProgrammingEnv programmingEnv) {
            if (sBlock == null)
                return null;
            if (programmingEnv == null)
                throw new ArgumentNullException(nameof(programmingEnv));

            I_BPG_Block block = null;

            var prefabName = sBlock.name;
            var prefab = BPG_BlockUtils.LoadBlockPrefab(prefabName);
            if (prefab != null) {

                block = BPG_BlockUtils.CreateBlock(prefab, programmingEnv);
                block.RectTransform.localPosition = sBlock.localPosition;

                //if (sBlock.blockIns != null) {
                //    block.SetAdditonalData(sBlock.blockIns); // ���J�X�^���u���b�N�̌ŗL�f�[�^��ݒ肷��
                //}

                // �z���̃Z�N�V�����𐶐�����
                AddSerializableBlockSectionsAsync(block, sBlock, programmingEnv).Forget();
            }

            return block;
        }

        /// <summary>
        /// �Z�N�V�����͈ȉ��̃u���b�N�𐶐�����D
        /// ���q�u���b�N�͐e�Z�N�V�����̃C���X�^���X������ɏ������Ȃ���Null�G���[����������D
        /// </summary>
        private static async UniTaskVoid AddSerializableBlockSectionsAsync(I_BPG_Block block, SerializableBlock sBlock, I_BPG_ProgrammingEnv programmingEnv) {
            // �t���[���̏I����҂�
            await UniTask.Yield();

            var sections = block.Layout.Sections;
            for (int s = 0; s < sections.Count; s++) {

                var body = sections[s].Body;
                if (body != null) {
                    // Add children
                    foreach (var sChaildBlock in sBlock.sections[s].childBlocks) {
                        var childBlock = SerializableBlockToBlock(sChaildBlock, programmingEnv);
                        body.Append(childBlock);
                    }
                }

                sections[s].Header.UpdateItems();
                sections[s].Header.UpdateInputs();
            }

        }

        #endregion
    }
}
