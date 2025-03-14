using UnityEngine;

namespace nitou.BlockPG.DragDrop{
    using Location = BlockLocation;

    /// <summary>
	/// �u���b�N�z�u�ꏊ�̕���.
	/// </summary>
	public enum BlockLocation {

        /// <summary>�h���b�O�\�[�X�̈�</summary>
        Outside,

        /// <summary>ProgrammingEnv�̃t���[�̈�</summary>
        ProgEnv,

        /// <summary>ProgrammingEnv��BlockStackr�̈�</summary>
        Stack,

        /// <summary>���m�F��</summary>
        InputSpot,
	}

    /// <summary>
    /// �h���b�O�����̌���
    /// </summary>
	public enum DraggingResult {

		// �u���b�N�𐶐�����
		CreateBlock,

		// �u���b�N��j������
		DestroyBlock,

		// �u���b�N���ړ����ꂽ
		Move,

		// �u���b�N���ړ����� (���ڑ��֌W�ɕω��Ȃ�)
		FreeMove,
	}


	/// <summary>
	/// <see cref="I_BE2_Block"/>�̃h���b�O����Ɋւ���ėp���\�b�h�W
	/// </summary>
	public static class DraggingUtil {

		/// <summary>
		/// �h���b�O����ɂ�錋�ʂ𔻒肷��
		/// </summary>
		public static DraggingResult CheckResult(Location from, Location to) {
			return (from, to) switch {
				// �u���b�N�̐��� (��Outside����u���b�N�̃h���b�O���J�n���ꂽ�ꍇ)
				(Location.Outside, Location.ProgEnv) => DraggingResult.CreateBlock,
				(Location.Outside, Location.Stack) => DraggingResult.CreateBlock,
				(Location.Outside, Location.InputSpot) => DraggingResult.CreateBlock,

				// �u���b�N�̍폜 (��Outside�Ƀu���b�N���h���b�v����ꍇ)
				(Location.ProgEnv, Location.Outside) => DraggingResult.DestroyBlock,
				(Location.Stack, Location.Outside) => DraggingResult.DestroyBlock,
				(Location.InputSpot, Location.Outside) => DraggingResult.DestroyBlock,

				// �u���b�N�̈ړ� (���ڑ��֌W�̕ω��Ȃ�)
				(Location.ProgEnv, Location.ProgEnv) => DraggingResult.FreeMove,

				// �u���b�N�̈ړ�
				_ => DraggingResult.Move
			};
		}

	}

}
