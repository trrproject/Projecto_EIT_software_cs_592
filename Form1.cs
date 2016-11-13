using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Threading;

namespace temporizador
{


    public partial class Form1 : Form
    {
        //Importação de uma biblioteca para a verificação das teclas
        [DllImport("user32.dll")]
        //Função de verificação das teclas
        static extern bool GetAsyncKeyState(System.Windows.Forms.Keys vKey);
        //Inicialização das variáveis
        private int slanguage = 1, trama_total = 0, trama_total_recebida = 0, trama_correcta = 0, trama_max = 5, aux = 0;
        double percentagem = 0;
        private string trama;
        private string[] theSerialPortNames = SerialPort.GetPortNames();

        string percento;


        //Tramas pré-definidas
        public byte[] Frente = new byte[] { 35, 70, 0, 51, 17 };
        public byte[] Tras = new byte[] { 35, 084, 0, 51, 221 };
        public byte[] Esquerda = new byte[] { 35, 83, 3, 80, 245 };
        public byte[] Direita = new byte[] { 35, 83, 7, 115, 169 };
        //public byte[] Centro = new byte[] { 035, 083, 005, 225 };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Procura das portas de série disponíveis
            for (int i = 0; i < theSerialPortNames.Length; i++)
                Sportslist.Items.Add(theSerialPortNames[i]);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Direcção de cima
            if (GetAsyncKeyState(Keys.Up) && (!GetAsyncKeyState(Keys.Left)) && (!GetAsyncKeyState(Keys.Right)) && (!GetAsyncKeyState(Keys.Down)))
            {

                textBox2.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Frente, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de cima e esquerda
            else if (GetAsyncKeyState(Keys.Up) && (GetAsyncKeyState(Keys.Left)) && (!GetAsyncKeyState(Keys.Right)) && (!GetAsyncKeyState(Keys.Down)))
            {
                textBox1.BackColor = Color.Black;
                textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Esquerda, 0, trama_max);
                trama_total += 1;
                Thread.Sleep(2);
                serialPort1.Write(Frente, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de cima e direita
            else if (GetAsyncKeyState(Keys.Up) && (!GetAsyncKeyState(Keys.Left)) && (GetAsyncKeyState(Keys.Right)) && (!GetAsyncKeyState(Keys.Down)))
            {
                textBox3.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Direita, 0, trama_max);
                trama_total += 1;
                Thread.Sleep(2);
                serialPort1.Write(Frente, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de baixo
            else if (!GetAsyncKeyState(Keys.Up) && (!GetAsyncKeyState(Keys.Left)) && (!GetAsyncKeyState(Keys.Right)) && (GetAsyncKeyState(Keys.Down)))
            {
                textBox8.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                serialPort1.Write(Tras, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de baixo e esquerda
            else if (!GetAsyncKeyState(Keys.Up) && (GetAsyncKeyState(Keys.Left)) && (!GetAsyncKeyState(Keys.Right)) && (GetAsyncKeyState(Keys.Down)))
            {
                textBox7.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Esquerda, 0, trama_max);
                trama_total += 1;
                Thread.Sleep(2);
                serialPort1.Write(Tras, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de baixo e direita
            else if (!GetAsyncKeyState(Keys.Up) && (!GetAsyncKeyState(Keys.Left)) && (GetAsyncKeyState(Keys.Right)) && (GetAsyncKeyState(Keys.Down)))
            {
                textBox9.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty;
                
                serialPort1.Write(Direita, 0, trama_max);
                trama_total += 1;
                Thread.Sleep(2);
                serialPort1.Write(Tras, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Direcção de esquerda
            else if (!GetAsyncKeyState(Keys.Up) && (GetAsyncKeyState(Keys.Left)) && (!GetAsyncKeyState(Keys.Right)) && (!GetAsyncKeyState(Keys.Down)))
            {
                textBox4.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Esquerda, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            ///////Diecções de Direita
            else if (!GetAsyncKeyState(Keys.Up) && (!GetAsyncKeyState(Keys.Left)) && (GetAsyncKeyState(Keys.Right)) && (!GetAsyncKeyState(Keys.Down)))
            {
                textBox6.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty; textBox5.BackColor = Color.Empty;
                textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
                
                serialPort1.Write(Direita, 0, trama_max);
                trama_total += 1;
                //this.Invoke(new EventHandler(Update_total));
            }
            //Nenhuma direcção associada
            else
            {
                textBox5.BackColor = Color.Black;
                textBox1.BackColor = Color.Empty; textBox2.BackColor = Color.Empty; textBox3.BackColor = Color.Empty; textBox4.BackColor = Color.Empty;
                textBox6.BackColor = Color.Empty; textBox7.BackColor = Color.Empty; textBox8.BackColor = Color.Empty; textBox9.BackColor = Color.Empty;
            }
        }
        //Contador para a velocidade (mais ou menos) 
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (GetAsyncKeyState(Keys.A))
                if (nspeed.Value < nspeed.Maximum)
                    nspeed.Value = nspeed.Value + 1;
            if (GetAsyncKeyState(Keys.Z))
                if (nspeed.Value > nspeed.Minimum)
                    nspeed.Value = nspeed.Value - 1;
        }

        //Botão para ligar a uma porta de série
        private void Connect_Click(object sender, EventArgs e)
        {
            if (theSerialPortNames.Length == 0)
            {
                MessageBox.Show("Não existem portas de série neste computador");
            }
            else
            {
                if ((Sportslist.Text == "Escolha uma porta") || (Sportslist.Text == ""))
                    MessageBox.Show("Tem de escolher uma porta");
                else
                {
                    serialPort1.PortName = Sportslist.Text;
                    try
                    {
                        serialPort1.Open();
                        Sportslist.Enabled = false;
                        timer1.Start();
                        timer2.Start();
                        Connect.Enabled = false;
                        disconnect.Enabled = true;
                    }
                    catch (Exception a)
                    {
                        MessageBox.Show("Não é possível estabelecer ligação à porta: " + serialPort1.PortName.ToString());
                    }
                }
            }
        }
        //Aceleração
        private void nspeed_Scroll(object sender, EventArgs e)
        {
            Frente[3] = (byte)(51 * nspeed.Value);
            aux = Frente[0] + Frente[1] + Frente[2] + Frente[3];
            Frente[4] = CRC8x8((byte)aux);

            Tras[3] = (byte)(51 * nspeed.Value);
            aux = Tras[0] + Tras[1] + Tras[2] + Tras[3];
            Tras[4] = CRC8x8((byte)aux);

        }
        //Recepção de dados
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (serialPort1.IsOpen)
                this.Invoke(new EventHandler(DoUpdate));
        }
        //Actualização da caixas de texto
        private void DoUpdate(object s, EventArgs e)
        {
            trama = serialPort1.ReadExisting();
            if (trama.Contains('S'))
            {
                trama_total_recebida += 1;
                textBox11.Text = trama_total_recebida.ToString();
            }
            if (trama.Contains('N'))
            {
                trama_correcta += 1;
                textBox10.Text = trama_correcta.ToString();
            }
            textBox12.Text = trama_total.ToString();
            label3.Text = Math.Round(100 - ((Division(trama_correcta, trama_total) * 100)), 3).ToString() + "%";
            progressBar1.Value = (int)((Division(trama_correcta, trama_total) * 100));
            //this.Invoke(new EventHandler(Update_total));
        }

        private void Update_total(object myobject, EventArgs e)
        {
            textBox12.Text = trama_total.ToString();
            label3.Text = Math.Round(Division(trama_total, trama_correcta), 3).ToString();
            progressBar1.Value = (int)Math.Round(Division(trama_total, trama_correcta), 3);
        }
        //Desliga-se
        private void disconnect_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
                timer1.Stop();
            if (timer2.Enabled)
                timer2.Stop();
            if (serialPort1.IsOpen)
                serialPort1.Close();
            Connect.Enabled = true;
            Sportslist.Enabled = true;
            disconnect.Enabled = false;
        }

        private byte CRC8(byte input, byte seed)
        {
            byte i;
            byte feedback;

            for (i = 0; i < 8; i++)
            {
                feedback = (byte)((seed ^ input) & 0x01);
                if (feedback == 0)
                    seed >>= 1;
                else
                {
                    seed ^= 0x18;
                    seed >>= 1;
                    seed |= 0x80;
                }
                input >>= 1;
            }

            return seed;
        }

        private byte CRC8x8(byte input)
        {
            byte i;
            byte check;

            check = 0;
            for (i = 0; i < 8; i++)
            {
                check = CRC8(input, check);
                input++;
            }

            return check;
        }

        public double Division(int a, int b) 
        {
            if ((b == 0) || (a == b)) 
                return (double)0;
            else
                return (double)a / b; 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            slanguage = 1;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            slanguage = 0;
            Form.ActiveForm.Refresh();

        }
    }
}