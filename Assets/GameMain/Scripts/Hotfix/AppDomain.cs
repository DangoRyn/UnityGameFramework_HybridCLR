using GameFramework;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;

namespace Game.Hotfix
{
    public class AppDomain
    {
        public static void Start()
        {
            Debug.Log("App Domain Start!");
            GameEntry.Fsm.DestroyFsm<IProcedureManager>();
            
            IProcedureManager procedureManager = GameFrameworkEntry.GetModule<IProcedureManager>();
            IFsmManager fsmManager = GameFrameworkEntry.GetModule<IFsmManager>();
            procedureManager.Initialize(fsmManager, new ProcedureBase[]
            {
                new ProcedureHotfixLaunch(),
                new ProcedurePreload(),
                new ProcedureMain(),
                new ProcedureMenu(),
                new ProcedureChangeScene(),
            });
            
            procedureManager.StartProcedure<ProcedureHotfixLaunch>();
        }
    }
}
