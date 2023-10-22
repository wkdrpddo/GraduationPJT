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
using StringTok;


namespace Server
{
    class FileHandler
    {
        private Server serverInfo;              // 서버 변수

        public Socket clientSocket;             // 소켓 선언
        public Thread th;                       // 스레드 선언

        private List<FileHandler> clientList;   // 접속된 클라이언트를 저장할 리스트 변수

        private byte[] receiveData;             // 전송받은 데이터를 저장할 byte배열
        private int fileNamesize;               // 파일이름의 크기를 받을 변수
        private int filesize;                   // 파일의 크기를 받을 변수

        public FileHandler(Server form)
        {
        }
        
        // 생성자 함수
        public FileHandler(Socket clientSocket, Server serverInfo, List<FileHandler> clientList)
        {
            this.clientSocket = clientSocket;
            this.serverInfo = serverInfo;
            this.clientList = clientList;

            // 스레드를 정의하고 시작
            th = new Thread(new ThreadStart(run));
            th.Start();
        }

        // run함수
        private void run()
        {
            // 스레드가 살아있는 동안 동작
            while (th.IsAlive)
            {
                try
                {
                    // 파일 크기를 받아서 접속된 모든 클라이언트에게 전송
                    receiveData = new byte[4];
                    clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                    multiCast(receiveData);
                    filesize = BitConverter.ToInt32(receiveData, 0);

                    // 파일 이름의 크기를 받아서 접속된 모든 클라이언트에게 전송
                    receiveData = new byte[4];
                    clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                    multiCast(receiveData);
                    fileNamesize = BitConverter.ToInt32(receiveData, 0);
                    
                    // 분할되어 전송되는 파일의 정보를 받는다.
                    receiveData = new byte[fileNamesize];
                    clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                    multiCast(receiveData);

                    receiveData = new byte[1024];
                    int fullLength = 0;
                    // 분할된 파일이 모두 받아질 때 까지 반복적으로 데이터를 받아 접속된 모든 클라이언트에게 재전송
                    while(fullLength < filesize)
                    {
                        int recevieLength = clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                        multiCast(receiveData);
                        fullLength += recevieLength;
                    }
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        // 접속된 모든 클라이언트에게 데이터 전송
        private void multiCast(byte[] buf)
        {
            foreach (FileHandler ch in clientList)
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
