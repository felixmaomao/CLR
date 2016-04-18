namespace JDD.Task.Log
{
    partial class LogInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.JDD_Task_Process = new System.ServiceProcess.ServiceProcessInstaller();
            this.JDD_Task_Log_Install = new System.ServiceProcess.ServiceInstaller();
            // 
            // JDD_Task_Process
            // 
            this.JDD_Task_Process.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.JDD_Task_Process.Password = null;
            this.JDD_Task_Process.Username = null;
            // 
            // JDD_Task_Log_Install
            // 
            this.JDD_Task_Log_Install.Description = "苏州奖多多科技有限公司";
            this.JDD_Task_Log_Install.ServiceName = "JDD.Task.Log";
            this.JDD_Task_Log_Install.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // LogInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.JDD_Task_Process,
            this.JDD_Task_Log_Install});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller JDD_Task_Process;
        private System.ServiceProcess.ServiceInstaller JDD_Task_Log_Install;

    }
}