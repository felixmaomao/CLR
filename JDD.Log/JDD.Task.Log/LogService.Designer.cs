namespace JDD.Task.Log
{
    partial class LogService
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
            this.JDD_Task_Log_Install = new System.ServiceProcess.ServiceController();
            // 
            // JDD_Task_Log_Install
            // 
            this.JDD_Task_Log_Install.MachineName = "苏州奖多多科技有限公司";
            this.JDD_Task_Log_Install.ServiceName = "JDD.Task.Log";
            // 
            // LogService
            // 
            this.ServiceName = "LogService";

        }

        #endregion

        private System.ServiceProcess.ServiceController JDD_Task_Log_Install;

    }
}
