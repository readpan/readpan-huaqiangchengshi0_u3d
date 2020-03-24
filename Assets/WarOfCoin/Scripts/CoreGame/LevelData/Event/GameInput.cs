using UnityEngine;

namespace WarOfCoin.Scripts.CoreGame.LevelData.Event {

    public struct GameInput {

        /// <summary>
        /// 输入类型
        /// </summary>
        public InputType InputType;
        
        /// <summary>
        /// 输入区域
        /// </summary>
        public Vector2 InputArea;

    }

    public enum InputType {

        None,
        SwapUp,
        SwapDown

    }

}
