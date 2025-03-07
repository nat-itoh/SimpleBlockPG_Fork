using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using nitou.BlockPG.Blocks;
using nitou.BlockPG.Interface;

namespace RuntimeTests {

    public class Test {

        private readonly static string ResourcePath = "BlockPG/Block [Condition]";


        /// ----------------------------------------------------------------------------

        [UnityTest]
        public IEnumerator �u���b�N�̐���() {

            // 
            var prefab = Resources.Load<BPG_BlockBase>(ResourcePath);
            var block = GameObject.Instantiate<BPG_BlockBase>(prefab);

            // �e�X�g�R�[�h
            var rootBlock = block.GetRootBlock();
            Debug.Log($"Root : {rootBlock?.RectTransform.name}");

            var parentBlock = block.GetParentBlock();
            Debug.Log($"Parent : {parentBlock?.RectTransform.name}");

            var previousBlock = block.GetPreviousBlock();
            Debug.Log($"Previous : {previousBlock?.RectTransform.name}");

            var nextBlock = block.GetNextBlock();
            Debug.Log($"Next : {nextBlock?.RectTransform.name}");



            // Start()�O
            Assert.That(block != null);
            Assert.That(block == rootBlock);
            //Assert.That(hoge.count == 0);

            yield return null;

            // Start()��
            Assert.That(block != null);
            Assert.That(block == rootBlock);
        }

    }

}