using GameFramework.DataTable;
using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityEngine;
using UnityGameFramework.Runtime;

namespace Game.Hotfix
{
    public class ProcedureHotfixLaunch : ProcedureBase
    {
        private bool m_IsInitSuccessful = false;
        
        //建议一些热更初始化相关的操作丢在这里
        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            
            //todo anythings
            m_IsInitSuccessful = true;
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            m_IsInitSuccessful = false;
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (!m_IsInitSuccessful)
                return;
            
            ChangeState<ProcedurePreload>(procedureOwner);
        }

        public override bool UseNativeDialog { get; }
    }
}
