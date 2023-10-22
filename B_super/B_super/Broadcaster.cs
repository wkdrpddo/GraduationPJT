using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
using StringTools;
using System.Collections;
 

namespace Broadcaster
{
    public partial class Form1 : Form
    {
        private const int SEND_ID = 100;        // 필명 전송 상수
        private const int MSG = 200;            // 메시지 전송 상수
        private const int DISCONNECT = 300;     // 연결 종료 상수

        private const int FILE_NAME = 1000;     // 전송할 파일 이름 상수
        private const int FILE_SIZE = 2000;     // 전송할 파일 크기 상수
        private const int FILE_DATA = 3000;     // 전송할 파일 데이터 상수

        // 프로그램이 사용할 IP 변수
        private IPAddress ip;

        // 이미지 전송부분 Port, EndPoint, socket
        private int port;
        private IPEndPoint endPoint;
        Socket imageSocket;

        // 메시지 전송부분 Port, EndPoint, socket
        private int port2;
        private IPEndPoint endPoint2;
        Socket textSocket;

        // 파일 전송부분 Port, EndPoint, socket
        private int port3;
        private IPEndPoint endPoint3;
        Socket fileSocket;
        
        Thread th = null;           // 이미지 소켓 연결 및 수신 스레드
        Thread th2 = null;          // 메시지 소켓 연결 및 수신 스레드
        Thread th3 = null;          // 파일 소켓 연결 및 수신 스레드

        // 화면 캡쳐 스레드
        Thread th_Capture = null;

        private string id = "Broadcast";    // 송출자의 id는 Broadcast로 고정
        private string str;                 // 주고받을 메시지 데이터를 저장할 변수
        private string list;                // 접속중인 사람들의 id를 저장할 변수
        private string cnt;                 // 현재 접속 인원 수

        private byte[] receiveStr;          // 메시지 수신 변수
        private byte[] receiveData;         // 이미지 수신 변수
        private byte[] sendData;            // 전송할 이미지 변수
        private byte[] receiveFile;         // 파일 수신 변수
        private byte[] sendFile;            // 파일 송신 변수

        delegate void TextSetCallback();    // 채팅 입력을 처리할 Delegate
        delegate void ListSetCallback();    // 참여자 리스트 업데이트를 처리할 Delegate

        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();    // Timer 선언

        public Form1()
        {
            InitializeComponent();
        }

        // Broadcaster의 Load시 동작하는 이벤트
        private void Form1_Load(object sender, EventArgs e)
        { 
            timer.Tick += new EventHandler(timer_Tick);     // Tick 이벤트 할당
            timer.Enabled = false;                          // 타이머 스위치 Off
        }
        
        // 타이머의 Tick 이벤트 발생 시 캡쳐 스레드를 시작한다.
        private void timer_Tick(object sender, EventArgs e)
        {
            th_Capture = new Thread(new ThreadStart(capture_Screen));   // 스레드에 이벤트 할당
            th_Capture.Start();                                         // 타이머 스위치 시작
        }

        // 화면을 캡처하는 이벤트 함수
        private void capture_Screen()
        {
            // 화면의 크기를 얻어와서 크기 정보에 맞춘 Bitmap 파일을 생성
            // Graphics를 사용하여 화면에서 색 데이터를 가져온다.
            Size sz = new Size(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Bitmap bt = new Bitmap(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width, System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height);
            Graphics g = Graphics.FromImage(bt);
            g.CopyFromScreen(0, 0, 0, 0, sz);

            // 전송 속도가 빠른 MemoryStream 방식을 사용
            MemoryStream ms = new MemoryStream();
            ms.Position = 0;
            bt.Save(ms, ImageFormat.Jpeg);              // 화면 캡쳐
            sendData = ms.ToArray();                    // Byte[]로 변환

            try
            {
                imageSocket.Send(sendData);             // 이미지 전송
            }
            catch (Exception e)
            {

            }
        }

        // 이미지 전용 소켓 연결 함수
        private void img_Connect()
        {
            try
            {
                imageSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                imageSocket.Connect(endPoint);

                receiveData = new byte[300000];
                // 이미지 스레드가 살아있는 동안 반복
                while (th.IsAlive)
                {
                    try
                    {
                        // 서버에서 받은 데이터를 저장
                        imageSocket.Receive(receiveData, receiveData.Length, SocketFlags.None);

                        byte[] PicData = receiveData;
                        // 받은 데이터 출력
                        print_Screen(PicData);
                    }
                    catch (Exception e)
                    {
                        // 수신 실패 시에도 계속해서 수신을 시도
                        continue;
                    }
                }
            }
            catch (Exception e)
            {
            }
        }

        // 이미지를 저장하고 화면 컨트롤에 출력하는 함수
        private void print_Screen(byte[] PicData)
        {
            MemoryStream ms = new MemoryStream();
            ms.Write(PicData, 0, PicData.Length);
            Bitmap bitmap = new Bitmap(ms);
            bitmap.Save("x2.jpg", ImageFormat.Jpeg);
            Screen.ImageLocation = "x2.jpg";
        }

        // START/EXIT 버튼 클릭 시의 이벤트 함수
        private void START_Click(object sender, EventArgs e)
        {
            if(CntButton.Text == "START")
            {
                CntButton.Text = "EXIT";

                waitingPage.Visible = false;
                ON.Visible = true;
                OFF.Visible = false;
                SEND.Enabled = true;
                IPtext.Visible = false;

                messageBox.ReadOnly = false;
                messageBox.Focus();

                // 서버측 IP 주소
                ip = IPAddress.Parse(IPtext.Text);

                // 이미지 전용 Port번호 설정 및 소켓 스레드 시작
                port = 22;
                endPoint = new IPEndPoint(ip, port);
                th = new Thread(new ThreadStart(img_Connect));
                th.Start();

                // 메시지 전용 Port번호 설정 및 소켓, 스레드 시작
                port2 = 23;
                endPoint2 = new IPEndPoint(ip, port2);
                th2 = new Thread(new ThreadStart(msg_Connect));
                th2.Start();

                // 파일 전용 Port번호 설정 및 소켓, 스레드 시작
                port3 = 24;
                endPoint3 = new IPEndPoint(ip, port3);
                th3 = new Thread(new ThreadStart(file_Connect));
                th3.Start();

                // 타이머를 사용하여 Interval 간격으로 캡쳐 수행
                timer.Interval = 150;   // interval 설정 : 캡처하는 빈도 설정
                timer.Enabled = true;   // 타이머 스위치 On
            } else
            {
                progExit();
            }
        }

        // 프로그램의 X를 눌러 종료시의 이벤트 함수
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                progExit();
            }
            catch (Exception exc) { }
        }

        // 프로그램 종료 함수
        private void progExit()
        {
            // 연결종료 메시지를 서버에 보낸다.
            sendDisConnect();
            Thread.Sleep(500);

            // 소켓과 스레드, 프로그램을 종료한다.
            imageSocket.Close();
            textSocket.Close();
            fileSocket.Close();
            th.Abort();
            th2.Abort();
            th3.Abort();
            this.Close();
        }

        // 서버에 연결종료를 알리는 함수
        private void sendDisConnect()
        {
            string str = DISCONNECT + "||" + id + "||";
            byte[] strBuffer = Encoding.UTF8.GetBytes(str);
            textSocket.Send(strBuffer);
        }

        // 메시지 전용 소켓 연결 함수
        private void msg_Connect()
         {
             try
             {
                textSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                textSocket.Connect(endPoint2);
                
                // ID 전송
                sendID();

                receiveStr = new byte[256];
                // 메시지 전용 스레드가 살아있는 동안 반복
                while (th2.IsAlive)
                {
                    try
                    {
                        // 전송받은 데이터의 길이를 받아 데이터를 저장
                        // StringTokenizer를 사용하여 '||'를 기준으로 문자열 분리 후 순서대로 받아옴
                        int length = textSocket.Receive(receiveStr, receiveStr.Length, SocketFlags.None);
                        str = Encoding.UTF8.GetString(receiveStr, 0, length);
                        StringTokenizer st = new StringTokenizer(str, "||");
                        int part = int.Parse(st.nextToken());

                        // 메시지 머리에 붙여서 전송된 분류상수를 통해 각각 동작
                        switch (part)
                        {
                            // id전송 상수 : 참여자 목록 업데이트
                            case SEND_ID:
                                list = "";
                                list = st.nextToken();
                                ListSet();
                                break;
                            // 메시지 전송 상수 : 메시지를 받아온 뒤, 채팅창 업데이트
                            case MSG:
                                str = st.nextToken();
                                TextSet();
                                break;
                        }
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
        
        // 사용자 이름(id)를 전송하기 위한 함수 (고정값 : broadcast)
        private void sendID()
        {
            string str = SEND_ID + "||" + id + "||";
            byte[] strBuffer = Encoding.UTF8.GetBytes(str);
            textSocket.Send(strBuffer);
        }

        // 채팅창을 업데이트 하기 위한 함수
        private void TextSet()
        {
            // delegate를 사용한 Callback함수를 사용하여 동작
            if (this.ChatField.InvokeRequired)
            {
                TextSetCallback d = new TextSetCallback(TextSet);
                this.Invoke(d, new object[] { });
            }
            else
            {
                // 채팅창에 메시지들을 추가
                this.ChatField.AppendText(str + Environment.NewLine);
            }
        }

        // 참여자 목록 업데이트를 위한 함수
        private void ListSet()
        {
            // delegate를 사용한 Callback함수를 사용하여 동작
            if (ViewerList.InvokeRequired)
            {
                ListSetCallback d = new ListSetCallback(ListSet);
                Invoke(d, new object[] { });
            }
            else
            {
                // 참여자 목록에 list문자열 입력
                ViewerList.Text = "";
                ViewerList.Text = list;
            }
        }

        // 줄바꿈 문자열을 키보드에서 입력받은 경우 동작
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                e.KeyChar = ' ';
                string str = MSG + "||" + id + ">>" + messageBox.Text + "||";
                byte[] strBuffer = Encoding.UTF8.GetBytes(str);
                textSocket.Send(strBuffer);
                messageBox.Text = "";
            }
        }

        // 전송 버튼을 클릭한 경우 동작하는 이벤트 함수
        private void send_Click(object sender, EventArgs e)
        {
            string str = MSG + "||" + id + ">> " + messageBox.Text + "||";
            byte[] strBuffer = Encoding.UTF8.GetBytes(str);
            textSocket.Send(strBuffer);
            messageBox.Text = "";
        }

        // 파일 전송을 위한 소켓 연결
        private void file_Connect()
        {
            try
            {
                // 소켓 연결
                fileSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                fileSocket.Connect(endPoint3);

                // 서버가 재전송해주는 파일의 데이터를 임시로 받아줄 배열
                receiveFile = new byte[5000000];
                // 스레드가 살아있는 동안 동작
                while (th3.IsAlive)
                {
                    try
                    {
                        // 해당 소켓을 통해 전송받은 데이터를 저장 (송신자는 파일을 저장할 필요가 없으므로 데이터를 받는것으로 종료)
                        fileSocket.Receive(receiveFile, receiveFile.Length, SocketFlags.None);
                    } catch(Exception e)
                    {
                        continue;
                    }
                }
            }
            catch (Exception e) { }
        }

        // 파일 이름을 저장할 변수
        string filename = "";

        // 파일전송 버튼 클릭 시 동작
        private void FileButton_Click(object sender, EventArgs e)
        {
            try
            {
                // OpenFileDialog를 사용하여 전송할 파일을 선택, 경로를 받아온다.
                string filePath;
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "모든파일|*.*|한글파일(*.hwp)|*.hwp|PDF 파일(*.pdf)|*.pdf|텍스트 파일(*.txt)|*.txt";
                if (DialogResult.OK == open.ShowDialog())
                {
                    filePath = open.FileName;
                } else return;

                // 파일 경로에서 파일이름과 확장자를 받아온다.
                string[] f = filePath.Split('\\');
                filename = f[f.Count() - 1];
                
                // 파일스트림을 사용하여 읽어온 파일정보를 사용한다.
                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                // 파일 크기를 전송
                int fileSize = (int)fs.Length;
                byte[] fileBuffer = BitConverter.GetBytes(fileSize);
                fileSocket.Send(fileBuffer);
                
                // 파일이름의 크기를 전송
                int fileNameSize = Encoding.UTF8.GetByteCount(filename);
                fileBuffer = BitConverter.GetBytes(fileNameSize);
                fileSocket.Send(fileBuffer);

                // 파일 이름을 전송
                fileBuffer = Encoding.UTF8.GetBytes(filename);
                fileSocket.Send(fileBuffer);

                // 파일 전체크기를 분할하여 전송한다.
                // count는 분할된 조각의 갯수
                int count = fileSize / 1024 + 1;

                // BinaryReader를 사용하여 전송할 파일을 이진값으로 읽어서 순차적으로 전송
                BinaryReader br = new BinaryReader(fs);
                for(int i = 0; i < count; i++)
                {
                    fileBuffer = br.ReadBytes(1024);
                    fileSocket.Send(fileBuffer);
                }
                br.Close();
                fs.Close();
            } catch(Exception)
            {
                return;
            }
        }
    }
}
