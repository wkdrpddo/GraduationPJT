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
    class TextSocket
    {
        private IPAddress ip;                   // IP 주소
        private int port;                       // Port 번호
        private IPEndPoint endPoint;            // 연결 매체

        private Socket socket;                  // 소켓 선언
        public Thread th;                       // 스레드 선언

        private Server serverInfo;              // 폼의 컨트롤 사용을 위한 서버 변수

        public List<TextHandler> clientList;    // 서버에 접속한 클라이언트들을 저장할 List 선언

        // 메시지 전송 소켓연결
        public TextSocket(Server formInfo)
        {
            this.serverInfo = formInfo;

            // 클라이언트 정보를 담을 list
            clientList = new List<TextHandler>();

            // 채팅 전용 종단점 생성
            ip = IPAddress.Any;
            port = 23;
            endPoint = new IPEndPoint(ip, port);
            
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(endPoint);


            // 연결 대기
            socket.Listen(10);
            serverInfo.TextOP.Text = "waiting";

            // 연결이 되면 스레드 생성해서 시작
            th = new Thread(new ThreadStart(run));
            th.IsBackground = true;
            th.Start();
        }

        // run 함수 (스레드에 사용)
        private void run()
        {
            // 스레드가 동작하는 동안 반복
            while (th.IsAlive)
            {
                // 새로운 연결 생성
                Socket clientSocket = socket.Accept();
                serverInfo.TextOP.Text = "acceptClient";

                // 새로운 연결에 대한 핸들러를 클라이언트 리스트에 추가
                TextHandler ch = new TextHandler(clientSocket, serverInfo, clientList);
                clientList.Add(ch);
            }
        }

        // 종료함수 exit
        public void exit()
        {
            // 클라이언트 리스트에 포함된 모든 스레드와 소켓을 종료하고 닫는다.
            foreach (TextHandler ch in clientList)
            {
                ch.th.Abort();
                ch.clientSocket.Close();
            }
            
            socket.Close();
        }
    }
}
