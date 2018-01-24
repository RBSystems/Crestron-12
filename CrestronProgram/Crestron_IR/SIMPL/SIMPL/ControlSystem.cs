using System;
using Crestron.SimplSharp;                          				// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;
using Crestron.SimplSharpPro.CrestronThread;                       				// For Basic SIMPL#Pro classes
using ILiveSmart.light;
using ILiveSmart;
using Socket;
using Crestron.SimplSharpPro.Keypads;
namespace IliveSmart
{

    public class ControlSystem : CrestronControlSystem
    {
        //SmartExecute smartExec = null;
        //LightExecute lightExec = null;
        //  ILiveSmartAPI logic = null;
        #region ������Ϣ
        /// <summary>
        /// ������Ϣ����
        /// </summary>
        private CrestronQueue<String> RxQueue = new CrestronQueue<string>();
        /// <summary>
        /// �����¼�
        /// </summary>
        //private Thread tcpListenHandler;
        #endregion

        #region ��ʼ���豸

        #endregion
        /// <summary>
        /// Constructor of the Control System Class. Make sure the constructor always exists.
        /// If it doesn't exit, the code will not run on your 3-Series processor.
        /// </summary>
        public ControlSystem()
            : base()
        {
            //��ϵͳʹ�õ�����߳���
            Thread.MaxNumberOfUserThreads = 30;

            //�����˼��ϵͳ�¼������磺������������ɵȵ�
            CrestronEnvironment.SystemEventHandler += new SystemEventHandler(ControlSystem_ControllerSystemEventHandler);
            //��������¼������磺ֹͣ����ͣ���ָ��ȡ�
            CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(ControlSystem_ControllerProgramEventHandler);
            //�����˼�������¼���
            CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(ControlSystem_ControllerEthernetEventHandler);
    

        }


        /// <summary>
        /// Overridden function... Invoked before any traffic starts flowing back and forth between the devices and the 
        /// user program. 
        /// This is used to start all the user threads and create all events / mutexes etc.
        /// This function should exit ... If this function does not exit then the program will not start
        /// </summary>
     
        private SmartAPI logicl;



        public override void InitializeSystem()     //�߼������ʼ��
        {
            this.DefaultStringEncoding = eStringEncoding.eEncodingUTF16;
            try
            {
   
                logicl = new SmartAPI(this);
               




            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }
        #region �����¼�
        /// <summary>
        /// This event is triggered whenever an Ethernet event happens. 
        /// </summary>
        /// <param name="ethernetEventArgs">Holds all the data needed to properly parse</param>
        void ControlSystem_ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }

        #endregion

        #region �����¼�
        /// <summary>
        /// This event is triggered whenever a program event happens (such as stop, pause, resume, etc.)
        /// </summary>
        /// <param name="programEventType">These event arguments hold all the data to properly parse the event</param>
        void ControlSystem_ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            //ILiveDebug.Instance.WriteLine(programStatusEventType.ToString());
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    // Crestron.SimplSharp.CrestronLogger.CrestronLogger.Initialize(10,Crestron.SimplSharp.CrestronLogger.LoggerModeEnum.CONSOLE);
                    // Crestron.SimplSharp.CrestronLogger.CrestronLogger.Clear(true);
                    //The program has been stopped.
                    //Close all threads. 
                    //Shutdown all Client/Servers in the system.
                    //General cleanup.
                    //Unsubscribe to all System Monitor events

                    //RxQueue.Enqueue(null); // The RxThread will terminate when it receives a null
                    break;
            }

        }

        #endregion

        #region ϵͳ�¼�
        /// <summary>
        /// This handler is triggered for system events
        /// </summary>
        /// <param name="systemEventType">The event argument needed to parse.</param>
        void ControlSystem_ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            // ILiveDebug.WriteLine(systemEventType.ToString());
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }

        }
        #endregion

    }

}

