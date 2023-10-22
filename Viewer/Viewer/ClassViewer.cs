using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace Viewer
{
    public partial class Viewer : Form
    {
        private string id = "gest";             // 익명성 보장을 위해 모든 참여자를 gest로 고정
        // 각각 전송을 판단하기위한 상수
        private const int SEND_ID = 100;        // 참여자 이름 전송 상수
        private const int MSG = 200;            // 메세지 전송 상수
        private const int DISCONNECT = 300;     // 연결종료 전송 상수

        // ip와 용도별 소켓연결 정보 정의 (각각 채팅, 이미지, 파일의 전송을 위한 소켓 정보)
        private IPAddress ip;                   // 서버가 동작중인 IP의 주소를 받을 변수

        // 채팅 통신을 위한 정보
        private int chatPort;                   // 포트번호 변수
        private IPEndPoint chatEndPoint;        // 연결을 위한 endPoint
        Socket chatSocket;                      // 채팅용 소켓 변수

        // 화면(이미지) 통신을 위한 정보
        private int imagePort;                  // 이미지 통신 포트번호 변수
        private IPEndPoint imageEndPoint;       // 연결을 위한 endPoint
        Socket imageSocket;                     // 이미지 통신용 소켓 변수

        // 파일 통신을 위한 정보
        private int filePort;                   // 파일 통신 포트번호 변수
        private IPEndPoint fileEndPoint;        // 연결을 위한 endPoint
        Socket fileSocket;                      // 파일 통신용 소켓 변수

        // 각 연결에 사용되는 스레드
        Thread th1 = null;                      // 채팅용 스레드
        Thread th2 = null;                      // 이미지용 스레드
        Thread th3 = null;                      // 파일용 스레드
        
        private string str;                     // 전송할 메세지를 담을 변수
        private string list;                    // 참여자 목록 정보를 담을 변수

        private int captureCount = 0;           // 캡처이미지의 저장이름에 사용되는 순번 변수

        private string saveFileName;            // 전송받은 파일의 이름 정보를 받을 변수
        private int saveFileSize;               // 전송받은 파일의 크기 정보를 받을 변수
        private int fileNameSize;               // 전송받은 파일의 이름 크기 정보를 받을 변수

        private byte[] receiveStr;              // 서버에서 전송받은 메세지 데이터를 담을 배열
        private byte[] receiveData;             // 서버에서 전송받은 이미지 데이터를 담을 배열

        // 각각 요청마다 동작할 callback 함수를 delegate로 정의한다.
        delegate void TextSetCallback();         // 채팅 입력을 처리할 delegate
        delegate void ListSetCallback();         // 참여자 리스트 업데이트를 처리할 Delegate


        public Viewer()
        {
            InitializeComponent();

            // 뷰어 시작과 함께 캡쳐사진의 저장경로를 검색한다.
            searchImage();
        }

        // 캡쳐 이미지의 번호 라벨링을 위해 이미지를 검색한다.
        // 캡처기능을 이용한 적이 없다면, 캡쳐이미지 저장 폴더가 없으므로 에러 메시지가 표시된다.
        private void searchImage()
        {
            // 파일 이름들을 담을 string 배열 객체
            string[] files = { };
            try
            {
                // 경로를 검색하여 파일 이름을 받아온다.
                files = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\captureScreen", "*.png", SearchOption.TopDirectoryOnly);
            }
            catch (IOException e)
            {
                // 경로를 찾을 수 없다는 메시지, 확인을 눌러주면 정상적으로 실행된다.
                MessageBox.Show(e.Message);
            }

            // 폴더 하위의 이미지를 검색하여 다음에 저장될 이미지의 번호를 라벨링 한다.
            string s = "";
            // 파일 이름들을 담아둔 files배열에서 'capture'라는 단어를 포함한 것만을 찾아서 s에 옮겨담는다.
            for (int i = 0; i < files.Length; i++)
            {
                if (files[i].Contains("capture"))
                    s = files[i];
            }
            // 파일이 존재한다면 다음 작업을 진행한다.
            if (s != "")
            {
                files = s.Split('\\');                  // 문자열 s를 '\\'를 기준으로 분리하여 files 배열에 담는다.
                for (int j = 0; j < files.Length; j++)
                {
                    // 'capture'문자열을 포함한 경우에만 문자열 s에 옮겨담는다.
                    if (files[j].Contains("capture"))
                        s = files[j];
                }

                // 문자열 s에서 숫자만을 뽑아낸다. (단, 최대 2자리 숫자까지만 도출 가능)
                string t = s.Substring(7, 2);

                // 캡처한 이미지 숫자를 count하는 변수에 이미 캡처된 수 +1 하여 저장한다.
                captureCount = int.Parse(t) + 1;
            }
        }

        // 화면공유를 위한 이미지 소켓 연결
        private void Connect_Image()
        {
            try
            {
                // 이미지 소켓을 생성하고 endpoint로 연결한다.
                imageSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                imageSocket.Connect(imageEndPoint);

                // 전송받은 이미지 데이터를 저장할 byte배열 정의 (크기는 임의로 1000000으로 설정)
                receiveData = new byte[1000000];
                // 스레드가 살아있는지 확인하고 살아있는 동안 반복적으로 소켓으로 들어온 이미지를 받아온다.
                // 스레드 종료나 오류 발생을 제외하고는 계속해서 동작한다.
                // while 반복문을 사용한 해당 작업의 반복을 통해 화면이 동영상처럼 보인다.
                while (th2.IsAlive)
                {
                    try
                    {
                        // 이미지 전용 소켓으로 들어온 데이터를 받아 receiveData변수에 저장한다.
                        imageSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);
                        
                        byte[] ImageData = receiveData;

                        // printScreen 동작
                        printScreen(ImageData);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
            }
            catch (Exception e)
            {

            }
        }

        // 이미지 소켓을 통해 받아온 이미지를 화면에 출력
        private void printScreen(byte[] data)
        {
            // 메모리 스트림을 통해 받아온 데이터를 Write한다.
            MemoryStream ms = new MemoryStream();
            ms.Write(data, 0, data.Length);
            // 기록된 데이터를 Bitmap파일로 변경한 뒤, jpeg타입으로 저장한다.
            Bitmap bitmap = new Bitmap(ms);
            bitmap.Save("view.jpg", ImageFormat.Jpeg);

            // 저장한 이미지를 Screen에 뿌려준다.
            ScreenView.ImageLocation = "view.jpg";
        }

        // 채팅을 위한 채팅 소켓 연결
        private void Connect_Chat()
        {
            try
            {
                // 채팅용 소켓을 생성하고 endPoint로 연결한다.
                chatSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                chatSocket.Connect(chatEndPoint);

                // sendID 함수를 동작한다.
                sendID();

                // 전송된 메시지를 받아올 byte배열 정의
                receiveStr = new byte[256];

                // 채팅용 스레드가 살아있는 동안 반복적으로 정보를 받아 확인한다.
                // 해당 소켓으로 전송되는 데이터가 없다면 대기한다.
                while (th1.IsAlive)
                {
                    try
                    {
                        // 전송받은 데이터를 receiveStr에 저장하고 해당 길이를 length에 저장
                        // 그 후, 문자열 변수 str에 receiveStr에 저장된 데이터를 인코딩하여 저장
                        int length = chatSocket.Receive(receiveStr, receiveStr.Length, SocketFlags.None);
                        str = Encoding.UTF8.GetString(receiveStr, 0, length);
                        // StringTokenizer를 사용하여 '||'를 기준으로 문자열을 분리하여 순서대로 가져온다.
                        StringTokenizer st = new StringTokenizer(str, "||");
                        int text = int.Parse(st.NextToken());

                        // 받아온 데이터를 분석하여 채팅 메시지와 접속 알림에 따라 동작한다.
                        switch (text)
                        {
                            // text의 데이터를 처음에 정의해둔 상수에 따라 구분하여 동작한다.
                            // 메세지 전송 상수 MSG의 경우 동작
                            case MSG:
                                str = st.NextToken();
                                ChatSet();
                                break;
                            // 참여자 이름 전송상수 SEND_ID의 경우 동작
                            case SEND_ID:
                                list = "=== 현재 참여자 ===";
                                list += st.NextToken();
                                ListSet();
                                break;
                        }
                    }
                    catch (Exception e) { continue; }
                }
            }
            catch (Exception e) { }
        }

        // 참여자의 접속 시 ID를 전송한다.
        private void sendID()
        {
            // 참여자 이름 상수를 가진 메시지를 byte형으로 변경하여 전송한다.
            string str = SEND_ID + "||" + id + "||";
            byte[] strBuffer = Encoding.UTF8.GetBytes(str);
            chatSocket.Send(strBuffer);                         // 참여자 id 전송
        }

        // 채팅이 수신되었을 때 채팅을 내용을 채팅화면에 출력해준다.
        private void ChatSet()
        {
            // 호출요청을 감지한다. (InvokeRequired)
            if (this.ChatField.InvokeRequired)
            {
                // callback을 해당 함수 ChatSet과 연결하고 동작한다.
                TextSetCallback call = new TextSetCallback(ChatSet);
                this.Invoke(call, new object[] { });
            }
            else
            {
                // 채팅화면에 메세지들을 추가하여 출력
                this.ChatField.AppendText(str + Environment.NewLine);
            }
        }

        // 서버에 새로운 접속이 감지되었을때 접속자 정보를 수신하여 세팅해준다.
        private void ListSet()
        {
            // 호출요청 감지
            if (ViewerList.InvokeRequired)
            {
                // callback을 해당 함수 ListSet과 연결하고 동작한다.
                ListSetCallback call = new ListSetCallback(ListSet);
                Invoke(call, new object[] { });
            }
            else
            {
                // 참여자 목록 화면에 리스트를 추가한다.
                ViewerList.Text = "";

                ViewerList.Text = list;
            }
        }

        // 파일 수신을 위한 소켓 연결
        private void Connect_File()
        {
            try
            {
                // 파일 수신용 소켓을 생성하고 endPoint에 연결
                fileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fileSocket.Connect(fileEndPoint);

                // 스레드가 살아있는 동안 반복해서 다음 동작을 한다.
                while (th3.IsAlive)
                {
                    // 전송된 파일 크기를 저장
                    byte[] buffer = new byte[4];
                    fileSocket.Receive(buffer);
                    saveFileSize = BitConverter.ToInt32(buffer, 0);

                    // 전송된 파일이름의 크기를 저장
                    buffer = new byte[4];
                    fileSocket.Receive(buffer);
                    fileNameSize = BitConverter.ToInt32(buffer, 0);

                    // 전송된 파일 이름을 저장
                    buffer = new byte[fileNameSize];
                    fileSocket.Receive(buffer);
                    saveFileName = Encoding.UTF8.GetString(buffer);
                    
                    buffer = new byte[1024];                // 수신된 파일의 데이터를 받을 byte배열
                    int fullLength = 0;                     // 현재까지 받은 데이터의 크기를 저장할 변수
                    string saveDir = Directory.GetCurrentDirectory() + "\\" + saveFileName;             // 파일 저장 경로 및 파일명 정보
                    
                    // 파일 스트림과 BinaryWriter를 사용하여 데이터를 저장할 준비를 함
                    FileStream fs = new FileStream(saveDir, FileMode.Create, FileAccess.Write);
                    BinaryWriter bw = new BinaryWriter(fs);

                    // 전체 파일크기와 현재까지 받은 파일의 크기를 비교하며 반복해서 데이터를 받아서 기록/저장한다.
                    while (fullLength < saveFileSize)
                    {
                        int receiveLength = fileSocket.Receive(buffer);
                        bw.Write(buffer, 0, receiveLength);
                        fullLength += receiveLength;
                    }
                }
            }
            catch (Exception e) { }
        }

        // 연결/종료 버튼을 눌렀을 때의 동작
        private void CntButton_Click(object sender, EventArgs e)
        {
            // 버튼의 텍스트 정보에 따라 동작구분
            // 연결 시 뷰어의 컨트롤 변화 및 각 통신을 위한 기본 연결과 스레드를 시작
            if (CntButton.Text == "연 결")
            {
                WatingPage.Visible = false;
                ScreenView.BackgroundImage = null;
                CntButton.Text = "연결 종료";
                ON.Visible = true;
                OFF.Visible = false;
                TypeField.ReadOnly = false;
                TypeField.Focus();
                SendButton.Enabled = true;
                IPtext.Visible = false;

                // 연결 시 textBox에서 ip 문자열을 받아와서 사용
                ip = IPAddress.Parse(IPtext.Text);

                // 각각 다른 포트를 사용하여 연결하고 스레드를 동작, 소켓을 연결한다.
                chatPort = 23;
                chatEndPoint = new IPEndPoint(ip, chatPort);
                th1 = new Thread(new ThreadStart(Connect_Chat));
                th1.Start();

                imagePort = 22;
                imageEndPoint = new IPEndPoint(ip, imagePort);
                th2 = new Thread(new ThreadStart(Connect_Image));
                th2.Start();

                filePort = 24;
                fileEndPoint = new IPEndPoint(ip, filePort);
                th3 = new Thread(new ThreadStart(Connect_File));
                th3.Start();
            }
            else
            {
                // 연결종료인 경우 뷰어의 컨트롤 변화 및 각 통신을 위한 접속을 종료
                WatingPage.Visible = true;
                CntButton.Text = "연 결";
                ON.Visible = false;
                OFF.Visible = true;
                TypeField.ReadOnly = true;
                SendButton.Enabled = false;

                // 접속 종료를 위한 함수
                progExit();
            }
        }

        // 프로그램 종료 함수
        private void progExit()
        {
            // 연결종료 메세지를 전송하는 disConnect 함수
            disConnect();

            // Sleep으로 지연시간을 줌
            Thread.Sleep(500);

            // 각 소켓을 닫고 스레드와 프로그램을 종료한다.
            chatSocket.Close();
            imageSocket.Close();
            fileSocket.Close();
            th1.Abort();
            th2.Abort();
            th3.Abort();
            this.Close();
        }

        // 서버에 연결종료 메시지를 보내어주는 함수
        private void disConnect()
        {
            // 연결종료 상수와 함께 연결종료 메시지를 전송한다.
            string str = DISCONNECT + "||" + id + "||";
            byte[] strBuf = Encoding.UTF8.GetBytes(str);
            chatSocket.Send(strBuf);
        }

        // 채팅을 치기위한 키보드에 반응하는 속성
        private void TypeField_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 줄띄움 명령(엔터)의 입력 시, 채팅의 내용과 메시지전송 상수를 함께 전송한다.
            if (e.KeyChar == '\r')
            {
                e.KeyChar = ' ';
                string str = MSG + "||" + id + ">>" + TypeField.Text + "||";
                byte[] strBuffer = Encoding.UTF8.GetBytes(str);
                chatSocket.Send(strBuffer);
                TypeField.Text = "";
            }
        }

        // 메세지 전송 버튼을 눌렀을때의 동작 속성
        private void SendButton_Click(object sender, EventArgs e)
        {
            // 메시지 전송 상수와 메시지를 함께 전송
            string str = MSG + "||" + TypeField.Text + "||";
            byte[] strBuffer = Encoding.UTF8.GetBytes(str);
            chatSocket.Send(strBuffer);
            TypeField.Text = null;
        }

        // 프로그램의 폼의 X를 눌러 종료할 시 동작하는 속성
        private void Viewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                progExit();
            }
            catch (Exception except) { }
        }

        // 캡쳐 버튼의 클릭 시 동작하는 속성
        private void CapButton_Click(object sender, EventArgs e)
        {
            Image img = ScreenView.Image;
            Image changeImg = new Bitmap(img, 2880, 1620);
            
            // 현재 폴더의 하위에 캡쳐 이미지를 저장할 폴더를 설정한다.
            // 폴더가 없다면 새로 생성되고 있다면 기존의 폴더가 선택된다.
            string saveFolder = Directory.GetCurrentDirectory() + "\\captureScreen";
            if (!Directory.Exists(saveFolder))
            {
                Directory.CreateDirectory(saveFolder);
            }
            string fileName = "";

            // 캡쳐한 이미지에 번호를 매겨 저장한다.
            if (captureCount < 10)
            {
                fileName = "\\capture0" + Convert.ToString(captureCount++) + ".png";
            }
            else
            {
                fileName = "\\capture" + Convert.ToString(captureCount++) + ".png";
            }

            changeImg.Save(saveFolder + fileName, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
