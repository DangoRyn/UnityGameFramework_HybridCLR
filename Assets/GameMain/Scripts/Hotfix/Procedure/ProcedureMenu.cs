using GameFramework.Event;
using GameFramework.Fsm;
using GameFramework.Procedure;
using UnityGameFramework.Runtime;
using UnityEngine;

namespace Game.Hotfix
{
    public class ProcedureMenu : ProcedureBase
    {
        private bool m_StartGame = false;
        private MenuForm m_MenuForm = null;
        private GameMode m_GameMode;

        public void StartGame(GameMode gameMode)
        {
            m_GameMode = gameMode;
            m_StartGame = true;
        }

        protected override void OnEnter(IFsm<IProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            GameEntry.Event.Subscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            m_StartGame = false;
            
            GameEntry.Entity.ShowEntity(typeof(TestEntity),"Default",100, new TestEntityData(GameEntry.Entity.GenerateSerialId(),10000)
            {
                Position = Vector3.zero,
                Rotation = Quaternion.identity
            });
            
            GameEntry.UI.OpenUIForm(UIFormId.MenuForm, this);
        }

        protected override void OnLeave(IFsm<IProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            GameEntry.Event.Unsubscribe(OpenUIFormSuccessEventArgs.EventId, OnOpenUIFormSuccess);

            if (m_MenuForm != null && m_MenuForm.Visible)
            {
                m_MenuForm.Close(isShutdown);
                m_MenuForm = null;
            }
        }

        protected override void OnUpdate(IFsm<IProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            if (m_StartGame)
            {
                procedureOwner.SetData<VarInt32>("NextSceneId", GameEntry.Config.GetInt("Scene.Main"));
                procedureOwner.SetData<VarByte>("GameMode", (byte)m_GameMode);
                ChangeState<ProcedureChangeScene>(procedureOwner);
            }
        }

        private void OnOpenUIFormSuccess(object sender, GameEventArgs e)
        {
            OpenUIFormSuccessEventArgs ne = (OpenUIFormSuccessEventArgs)e;
            if (ne.UserData != this)
            {
                return;
            }

            m_MenuForm = (MenuForm)ne.UIForm.Logic;
        }

        public override bool UseNativeDialog { get; }
    }
}
