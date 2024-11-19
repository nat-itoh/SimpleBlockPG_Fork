using System.Linq;
using UnityEngine;

// [�Q�l]
//  qiita: �V���O���g�����g���Ă݂悤�@https://qiita.com/Teach/items/c146c7939db7acbd7eee
//  kan�̃�����: MonoBehaviour���p�������V���O���g�� https://kan-kikuchi.hatenablog.com/entry/SingletonMonoBehaviour

namespace nitou.Utils {

    /// <summary>
    /// MonoBehaviour���p�������V���O���g��
    /// ��DontDestroyOnLoad�͌Ă΂Ȃ��i�h���N���X���ōs���j
    /// </summary>
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour 
        where T : MonoBehaviour {

        private static T _instance;
        public static T Instance {
            get {
                if (_instance == null) {
#if UNITY_2022_1_OR_NEWER
                    _instance = FindAnyObjectByType<T>(); 
#else
                    _instance = FindObjectOfType<T>(includeInactive: true); 
#endif

                    // �V�[�����ɑ��݂��Ȃ��ꍇ�̓G���[
                    if (_instance == null) {
                        Debug.LogError(typeof(T) + " ���A�^�b�`���Ă���GameObject�͂���܂���");
                    }
                }
                return _instance;
            }
        }


        /// ----------------------------------------------------------------------------
        // MonoBehaviour Method

        protected virtual void Awake() {
            // ���ɃC���X�^���X�����݂���Ȃ�j��
            if (!CheckInstance())
                Destroy(this.gameObject);
        }


        /// ----------------------------------------------------------------------------
        // Private Method

        /// <summary>
        /// ���̃Q�[���I�u�W�F�N�g�ɃA�^�b�`����Ă��邩���ׂ�
        /// </summary>
        protected bool CheckInstance() {
            // ���݂��Ȃ��ior�������g�j�ꍇ
            if (_instance == null) {
                _instance = this as T;
                return true;
            } else if (Instance == this) {
                return true;
            }
            // ���ɑ��݂���ꍇ
            return false;
        }
    }
}