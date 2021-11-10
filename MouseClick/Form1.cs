using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities;
namespace MouseClick
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        Bitmap screenCapture, myPic;
        Graphics g;
        Thread thread1;
        int cordal1x;
        int cordal1y;
        int cordal2x;
        int cordal2y;
        int cordal3x;
        int cordal3y;

        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        globalKeyboardHook sol = new globalKeyboardHook();
        globalKeyboardHook sag = new globalKeyboardHook();
        globalKeyboardHook yuk = new globalKeyboardHook();
        globalKeyboardHook asagi = new globalKeyboardHook();
        globalKeyboardHook tik = new globalKeyboardHook();

        globalKeyboardHook cord1 = new globalKeyboardHook();
        globalKeyboardHook cord2 = new globalKeyboardHook();
        globalKeyboardHook cord3 = new globalKeyboardHook();

        globalKeyboardHook start = new globalKeyboardHook();
        globalKeyboardHook stop = new globalKeyboardHook();
        public Form1()
        {
            InitializeComponent();
            klavyetus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public void klavyetus()
        {
            cord1.HookedKeys.Add(Keys.NumPad1);
            cord2.HookedKeys.Add(Keys.NumPad2);
            cord3.HookedKeys.Add(Keys.NumPad3);
            start.HookedKeys.Add(Keys.NumPad4);
            stop.HookedKeys.Add(Keys.NumPad5);

            sol.HookedKeys.Add(Keys.Left);
            sag.HookedKeys.Add(Keys.Right);
            yuk.HookedKeys.Add(Keys.Up);
            asagi.HookedKeys.Add(Keys.Down);
            tik.HookedKeys.Add(Keys.NumPad0);

            sol.KeyDown += new KeyEventHandler(solyap);
            sag.KeyDown += new KeyEventHandler(sagyap);
            yuk.KeyDown += new KeyEventHandler(yukyap);
            asagi.KeyDown += new KeyEventHandler(asagiyap);
            tik.KeyDown += new KeyEventHandler(tikyap);
            tik.KeyUp += new KeyEventHandler(tikyap1);

            cord1.KeyDown += new KeyEventHandler(cordal1);
            cord2.KeyDown += new KeyEventHandler(cordal2);
            cord3.KeyDown += new KeyEventHandler(cordal3);
            start.KeyDown += new KeyEventHandler(basla);
            stop.KeyDown += new KeyEventHandler(dur);


        }

        void dur(object sender, KeyEventArgs e)
        {
            timer1.Stop();

            e.Handled = false;
        }
        void basla(object sender, KeyEventArgs e)
        {
            timer1.Start();

            e.Handled = false;
        }

        void cordal1(object sender, KeyEventArgs e)
        {
            cordal1x = Cursor.Position.X;
            cordal1y = Cursor.Position.Y;
            label1.Text = cordal1x.ToString() +" , "+ cordal1y.ToString();
            
            e.Handled = false;
        }


        void cordal2(object sender, KeyEventArgs e)
        {
            cordal2x = Cursor.Position.X;
            cordal2y = Cursor.Position.Y;
            label2.Text = cordal2x.ToString() + " , " + cordal2y.ToString();
            e.Handled = false;
        }


        void cordal3(object sender, KeyEventArgs e)
        {
            cordal3x = Cursor.Position.X;
            cordal3y = Cursor.Position.Y;
            label3.Text = cordal3x.ToString() + " , " + cordal3y.ToString();
            e.Handled = false;
        }

       

        void solyap(object sender, KeyEventArgs e)
        {
            Cursor.Position = new Point(Cursor.Position.X -15, Cursor.Position.Y);
            e.Handled = false;
        }
        void sagyap(object sender, KeyEventArgs e)
        {
            Cursor.Position = new Point(Cursor.Position.X + 15, Cursor.Position.Y);
            e.Handled = false;
        }
        void yukyap(object sender, KeyEventArgs e)
        {
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y - 15);
            e.Handled = false;
        }
        void asagiyap(object sender, KeyEventArgs e)
        {
            Cursor.Position = new Point(Cursor.Position.X, Cursor.Position.Y + 15);
            e.Handled = false;
        }
        void tikyap(object sender, KeyEventArgs e)
        {
            int xx = System.Windows.Forms.Cursor.Position.X;
            int yy = System.Windows.Forms.Cursor.Position.Y;
            Cursor.Position = new System.Drawing.Point(xx, yy);
            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
            e.Handled = false;
        }
        void tikyap1(object sender, KeyEventArgs e)
        {
            int xx = System.Windows.Forms.Cursor.Position.X;
            int yy = System.Windows.Forms.Cursor.Position.Y;
            Cursor.Position = new System.Drawing.Point(xx, yy);
            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
            e.Handled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int x = rnd.Next(cordal1x, cordal2x);
            int y = rnd.Next(cordal1y, cordal2y);
            Cursor.Position = new System.Drawing.Point(x, y);
            mouse_event((int)(MouseEventFlags.RIGHTDOWN), 0, 0, 0, 0);
            mouse_event((int)(MouseEventFlags.RIGHTUP), 0, 0, 0, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            System.Windows.Forms.Form.CheckForIllegalCrossThreadCalls = false;
            thread1 = new Thread(new ThreadStart(test));
            openFileDialog1.FileName = "";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                myPic = new Bitmap(openFileDialog1.OpenFile());
                thread1.Start();

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("NUM1 1. Kordinat\nNUM2 2. Kordinat\nNUM3 3. Kordinat");
        }


        private bool IsInCapture(Bitmap searchFor, Bitmap searchIn)
        {
            for (int x = 0; x < searchIn.Width; x++)
            {
                for (int y = 0; y < searchIn.Height; y++)
                {
                    bool invalid = false;
                    int k = x, l = y;
                    for (int a = 0; a < searchFor.Width; a++)
                    {
                        l = y;
                        for (int b = 0; b < searchFor.Height; b++)
                        {
                            if (searchFor.GetPixel(a, b) != searchIn.GetPixel(k, l))
                            {
                                invalid = true;
                                break;
                            }
                            else
                                l++;
                        }
                        if (invalid)
                            break;
                        else
                            k++;
                    }
                    if (!invalid)
                        return true;
                }
            }
            return false;
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            
        }

        public void test()
        {
            while (true)
            {
                //Thread.Sleep(50);

                screenCapture = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);

                g = Graphics.FromImage(screenCapture);

                g.CopyFromScreen(Screen.PrimaryScreen.Bounds.X,
                                 Screen.PrimaryScreen.Bounds.Y,
                                 0, 0,
                                 screenCapture.Size,
                                 CopyPixelOperation.SourceCopy);


                bool isInCapture = IsInCapture(myPic, screenCapture);
                if (isInCapture == true)
                {
                    button2.Text = "V";
                }
                else
                {
                    button2.Text = "X";
                }


            }
        }
    }
}
    