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
using System.Collections;
using StringTok;


namespace Server
{
    class TextHandler
    {
        private const int SEND_ID = 100;        // 접속자 이름 전송 상수
        private const int SEND_MSG = 200;       // 메시지 전송 상수
        private const int DISCONNECT = 300;     // 연결 종료 상수
        
        private Server serverInfo;              // 서버 변수
        
        public Socket clientSocket;             // 소켓 변수
        public Thread th;                       // 스레드 변수

        private List<TextHandler> clientList;   // 접속한 클라이언트를 저장할 List 선언

        private byte[] receiveData;             // 전송받은 텍스트 데이터를 저장할 byte 배열 선언

        private string str;                     // 전송받은 문자열 저장 변수
        private string id;                      // 접속한 클라이언트 이름 저장 변수
        private int cnt;                        // 접속 클라이언트 수를 저장할 변수

        // 활용할 문자열 및 byte 배열
        private string temp;
        byte[] buf;

        // TextHandler 생성자 정의
        public TextHandler(Socket clientSocket, Server serverInfo, List<TextHandler> clientList)
        {
            this.clientSocket = clientSocket;
            this.serverInfo = serverInfo;
            this.clientList = clientList;

            // 스레드를 생성하고 시작
            th = new Thread(new ThreadStart(run));
            th.Start();
        }

        // run 함수 (스레드에 사용)
        private void run()
        {
            // 받은 데이터를 저장할 배열의 정의
            receiveData = new byte[256];

            // 스레드가 살아있는 동안 동작
            while (th.IsAlive)
            {
                try
                {
                    // 받아온 데이터의 길이 저장 후, 문자열을 인코딩해서 저장
                    int length = clientSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                    str = Encoding.UTF8.GetString(receiveData, 0, length);
                    // StringTokenizer를 사용하여 문자열을 '||'를 기준으로 나누어 순서대로 받아온다.
                    StringTokenizer st = new StringTokenizer(str, "||");
                    int part = int.Parse(st.NextToken());
                    buf = null;

                    // 받아온 메시지의 상수를 구분하여 각각 동작
                    switch (part)
                    {
                        // 접속자 이름 상수
                        case SEND_ID:
                            // 접속을 알려주는 출력 메시지를 전송
                            id = st.NextToken().Trim();
                            str = "연결되었습니다." + Environment.NewLine + "[ " + id + " ]님이 입장하셨습니다.";
                            temp = SEND_MSG + "||" + str + "||";
                            buf = Encoding.UTF8.GetBytes(temp);
                            // SendMessage 함수 동작
                            SendMessage(buf);

                            // ClientListSet과 SendMessage 함수 각각 동작
                            ClientListSet();
                            SendMessage(buf);

                            Thread.Sleep(100);

                            // 받은 문자열 클라이언트로 재 전송
                            buf = Encoding.UTF8.GetBytes(str);
                            SendMessage(buf);
                            break;
                        // 메시지 전송 상수
                        case SEND_MSG:
                            // 메시지 전송 상수를 포함한 문자열을 다시 구성하여 SendMessage 함수 호출
                            temp = SEND_MSG + "||";
                            str = st.NextToken();
                            temp = temp + str + "||"; ;
                            
                            buf = Encoding.UTF8.GetBytes(temp);
                            SendMessage(buf);
                            break;
                        // 연결 종료 상수
                        case DISCONNECT:
                            // 접속 종료 메시지를 생성한다.
                            temp = st.NextToken().Trim();
                            str = "[ " + temp + " ]님이 접속을 종료 하였습니다.";
                            string str2 = SEND_MSG + "||[ " + temp + " ]님이 접속을 종료 하였습니다.";

                            // 모든 연결된 클라이언트에게 연결종료 메시지를 보낸다.
                            foreach (TextHandler ch in clientList)
                            {
                                if (ch.id == temp)
                                {
                                    // 접속 종료를 알리는 채팅 메시지
                                    buf = Encoding.UTF8.GetBytes(str2);
                                    SendMessage(buf);

                                    // 접속자 리스트에서 해당 접속자 제거
                                    clientList.Remove(ch);
                                    
                                    ClientListSet();
                                    SendMessage(buf);

                                    Thread.Sleep(100);
                                    
                                    // 해당 클라이언트의 스레드와 소켓 종료
                                    ch.th.Abort();
                                    ch.clientSocket.Close();
                                    break;
                                }
                            }
                            break;
                    }
                }
                catch (Exception e)
                {

                }
            }
        }

        // 접속자 리스트를 구성하는 함수
        private void ClientListSet()
        {
            // 접속자 이름 상수
            temp = SEND_ID + "||";

            cnt = 0;
            // 반복을 통해 temp 문자열에 모든 접속자의 이름을 추가
            foreach (TextHandler ch2 in clientList)
            {
                temp = temp + ch2.id + Environment.NewLine;
                cnt++;
            }
            temp = temp + "||";
            // 완성된 문자열을 byte타입으로 인코딩하여 저장
            buf = Encoding.UTF8.GetBytes(temp);
        }

        // 접속된 모든 클라이언트에게 메시지를 전송하는 함수
        private void SendMessage(byte[] buf)
        {
            foreach (TextHandler ch in clientList)
            {
                ch.clientSocket.Send(buf);
            }
        }
        
    }
}
