using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
#if UNITY_EDITOR
using UnityEditor;
#endif

// [REF]
//  LIGHT11: Unity Test Runner�iTest Framework�j�Ńe�X�g�̑O�㏈�����������@�܂Ƃ� https://light11.hatenadiary.com/entry/2020/06/14/190752

namespace RuntimeTests {

    /// <summary>
    /// �e�X�g�p�̃��\�[�X�����E�폜��S���N���X�D
    /// </summary>
    public class PrePostBuildProcess : IPrebuildSetup, IPostBuildCleanup {

        private static readonly string Path = "Assets/";


        void IPrebuildSetup.Setup() {
#if UNITY_EDITOR

#endif
        }
        
        void IPostBuildCleanup.Cleanup() {
#if UNITY_EDITOR

#endif
        }
    
    }

}