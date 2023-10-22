using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;
using System.IO;
using System.IO.Compression;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;


namespace Server
{
    class ScreenHandler
    {
        public Thread th;                           // 스레드 선언
        public Socket clientSocket;                 // 소켓 선언

        private Server serverInfo;                  // 서버 변수

        private List<ScreenHandler> clientList;     // 접속한 클라이언트를 저장하는 리스트 변수

        private byte[] receiveData;                 // 전송받은 데이터를 담을 배열
        private byte[] PicData;                     // 사진 데이터를 담을 배열

        public ScreenHandler(Server form)
        {
        }

        public ScreenHandler(Socket clientSocket, Server serverInfo, List<ScreenHandler> clientList)
        {
            this.clientSocket = clientSocket;
            this.serverInfo = serverInfo;
            this.clientList = clientList;

            // 스레드 생성 및 시작
            th = new Thread(new ThreadStart(run));
            th.Start();
        }

        // run 함수
        private void run()
        {
            // 전송받은 데이터 저장 배열의 정의 (임의로 5000000 크기)
            receiveData = new byte[500000];
            // 스레드가 살아있는 동안 동작
            while (th.IsAlive)
            {
                try
                {
                    // 해당 소켓으로 전송된 데이터를 receiveData에 저장 후 다시 재전송
                    clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                    
                    PicData = receiveData;
                    multiCast(PicData);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        // 접속된 모든 클라이언트에게 전송하는 함수
        private void multiCast(byte[] buf)
        {
            foreach (ScreenHandler ch in clientList)
            {
                try
                {
                    ch.clientSocket.Send(buf);
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }
}
