//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework.Event;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public abstract class GameBase
    {
        public abstract GameMode GameMode
        {
            get;
        }
        public virtual void Initialize()
        {
            

        }

        public virtual void Shutdown()
        {
            
        }

        public virtual void Update(float elapseSeconds, float realElapseSeconds)
        {
            
        }
    }
}
