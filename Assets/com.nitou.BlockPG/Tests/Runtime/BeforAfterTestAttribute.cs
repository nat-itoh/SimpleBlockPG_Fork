using System.Collections;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using UnityEngine;
using UnityEngine.TestTools;

namespace RuntimeTests
{
    public class BeforeAfterTestAttribute : NUnitAttribute, 
        IOuterUnityTestAction {

        // �e�X�g�O����
        public IEnumerator BeforeTest(ITest test) {
            Debug.Log("Before Test");
            yield break;
        }

        // �e�X�g�㏈��
        public IEnumerator AfterTest(ITest test) {
            Debug.Log("After Test");
            yield break;
        }
    }
}
