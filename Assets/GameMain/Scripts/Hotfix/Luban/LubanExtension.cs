using System.Collections;
using System.Collections.Generic;
using Game.Hotfix.Cfg;
using UnityEngine;

namespace Game.Hotfix
{
    public static class LubanExtension
    {
        public static Tables GetTables(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables;
            }

            return null;
        }

        public static TbEntity GetEntityTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbEntity;
            }

            return null;
        }
        
        public static TbMusic GetMusicTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbMusic;
            }

            return null;
        }
        
        public static TbScene GetSceneTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbScene;
            }

            return null;
        }
        
        public static TbSound GetSoundTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbSound;
            }

            return null;
        }
        
        public static TbUIForm GetUIFormTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbUIForm;
            }

            return null;
        }
        
        public static TbUISound GetUISoundTable(this LubanTableComponent lubanComponent)
        {
            if (lubanComponent.TryGetTables(out Tables tables))
            {
                return tables.TbUISound;
            }

            return null;
        }
    }
}


