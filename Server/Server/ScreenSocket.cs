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
    class ScreenSocket
    {
        private IPAddress ip;                   // IP 주소
        private int port;                       // Port 번호
        private IPEndPoint endPoint;            // 연결 매체

        private Socket socket;                  // 소켓 선언
        public Thread th;                       // 스레드 선언

        private Server serverInfo;              // 폼의 컨트롤 사용을 위한 서버변수
        public List<ScreenHandler> clientList;  // 접속된 클라이언트들을 저장할 리스트 변수


        // 화면 전송 소켓연결
        public ScreenSocket(Server formInfo)
        {
            this.serverInfo = formInfo;

            // 클라이언트 정보를 담을 list
            clientList = new List<ScreenHandler>();

            // 화면 전용 종단점 생성
            ip = IPAddress.Any;
            port = 22;
            endPoint = new IPEndPoint(ip, port);
            
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);

            // 연결 대기
            socket.Listen(100);
            serverInfo.ScreenOP.Text = "waiting";

            // 연결 시 스레드 정의 및 시작
            th = new Thread(new ThreadStart(run));
            th.IsBackground = true;
            th.Start();
        }

        // run 함수
        private void run()
        {
            // 스레드가 살아있는 동안 동작
            while (th.IsAlive)
            {
                // 새로운 연결 생성
                Socket clientSocket = socket.Accept();
                serverInfo.ScreenOP.Text = "acceptClient";

                // 새로운 연결에 대한 핸들러 생성 후 리스트에 등록
                ScreenHandler ch = new ScreenHandler(clientSocket, serverInfo, clientList);
                clientList.Add(ch);
            }
        }

        // 종료함수 exit
        public void exit()
        {
            // 리스트에 포함된 모든 클라이언트의 스레드와 소켓 종료
            foreach (ScreenHandler ch in clientList)
            {
                ch.th.Abort();
                ch.clientSocket.Close();
            }

            socket.Close();
        }
    }
}
