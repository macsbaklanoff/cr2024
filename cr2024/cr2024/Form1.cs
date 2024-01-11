using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cr2024
{
    public partial class Form1 : Form
    {
        Graphics gr;
        int width = 0;
        int height = 0;
        int cells = 50;
        int lentaleft = -22;//левое число ленты
        int lentaright = 21;//правое число ленты
        int cell;
        int columnscount = 1;
        int col = 1;
        bool run = false;
        int centercaretk = 0;
        Dictionary <int, string> dict = new Dictionary<int, string>(); //словарь для ленты
        Dictionary<string, int> dicttable = new Dictionary<string, int>();//словарь для таблицы (поиск команды для символа)
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            gr = pictureBox1.CreateGraphics(); //создание ленты
            width = pictureBox1.Width;
            height = pictureBox1.Height;
            draw();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            centercaretk -= 1;
            lentaleft -= 1;
            lentaright -= 1;
            draw();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            centercaretk += 1;
            lentaleft += 1;
            lentaright += 1;
            draw();
        }
        private void draw() //функция рисования
        {
            gr.Clear(BackColor);
            gr.DrawLine(new Pen(Color.DarkBlue), width / 2 + 12, 5, width / 2 + 12 - 10, 0); //стрелка центральная
            gr.DrawLine(new Pen(Color.DarkBlue), width / 2 + 12, 5, width / 2 + 12 + 10, 0); //стрелка центральная
            gr.DrawLine(new Pen(Color.DarkBlue), 0, 12, width, 12);//горизонтальная верхняя
            gr.DrawLine(new Pen(Color.DarkBlue), 0, 40, width, 40);//горизонтальная нижняя
            int x1 = 0;
            int x2 = 0;
            for (int i = 0; i < cells; i++) //рисование ячеек
            {
                gr.DrawLine(new Pen(Color.DarkBlue), x1, 12, x2, 40);
                x1 += 25;
                x2 += 25;
            }
            x1 = 7;
            for (int i = lentaleft; i <= lentaright; i++) //рисование чисел над лентой и в ленте
            {
                gr.DrawString(i.ToString(), new Font("Arial", 8), new SolidBrush(Color.DarkBlue), x1, 0);
                if (dict.ContainsKey(i)) gr.DrawString(dict[i], new Font("Arial", 8), new SolidBrush(Color.DarkBlue), x1, 20);
                x1 += 25;
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            changeTable(); //функция изменения таблицы
        }
        public void changedcell(string s)
        {
            dict[cell] = s;
            draw(); //функция рисования
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            MouseEventArgs me = e as MouseEventArgs; //координаты double click
            cell = (int)(me.X / 25) + lentaleft;
            Form2 form2 = new Form2(this);
            form2.Show();
            
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            createcolumn();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (columnscount > 1)
            {
                dataGridView1.Columns.RemoveAt(columnscount);
                columnscount--;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //changeTable();
        }
        private void changeTable() {
            int rowsCount = dataGridView1.Rows.Count - 1, i;
            for (i = 0; i < rowsCount; i++)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[0]);
            }
            for (i = 0; i < textBox1.Text.Length; i++)
            {
                dicttable[textBox1.Text[i].ToString()] = i;
                dataGridView1.Rows.Add(textBox1.Text[i].ToString());
            }
            dicttable[""] = i;
            dataGridView1.Rows.Add("");
        }
        private void createcolumn()
        {
            DataGridViewTextBoxColumn newColumn = new DataGridViewTextBoxColumn();
            columnscount++;
            newColumn.HeaderText = "Q" + columnscount.ToString(); // Заголовок
            newColumn.Name = "newColumn"; // Название столбца
            newColumn.ValueType = typeof(string); // Тип данных в столбце
            dataGridView1.Columns.Add(newColumn); // Добавляем столбец к DataGridView
        }
        private void button7_Click(object sender, EventArgs e)
        {
            dict.Clear();
            draw();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            run = !run;
            col = 1;

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (run) {
                solve();
            }
        }
        private void button9_Click(object sender, EventArgs e)
        {
            solve();
        }
        private void solve() //решение (движение ленты)
        {
            string s = ""; //поиск символа в алфавите 
            if (dict.ContainsKey(centercaretk))
                s = dict[centercaretk];
            if (!dicttable.ContainsKey(s))
                s = "";
            int i = dicttable[s];
            if (dataGridView1[col, i].Value == null)
            {
                run = false;
                MessageBox.Show("Отсутствует команда");
                return;
            }
            string comand = dataGridView1[col, i].Value.ToString();
            if (int.TryParse(comand[comand.Length - 1].ToString(), out var number1)) //если хотим перейти в несуществующее состояние
            {
                if (number1 > columnscount)
                {
                    run = false;
                    MessageBox.Show("Несуществующее состояние");
                    return;
                }
            }
            if (comand.Length < 4)
            {
                run = false;
                MessageBox.Show("Неверная команда");
                return;
            }
            dict[centercaretk] = comand[0].ToString();
            if (comand[1] == '>')
            {
                centercaretk++;
                lentaleft++;
                lentaright++;
            }
            else if (comand[1] == '<')
            {
                centercaretk--;
                lentaleft--;
                lentaright--;
            }
            int k = 1;
            string ch = "";
            while (comand[comand.Length - k] != 'Q')
            {
                ch += comand[comand.Length - k];
                if (!int.TryParse(ch, out var number2))
                {
                    run = false;
                    MessageBox.Show("Неверная команда");
                    return;
                }
                k++;
            }
            ch.Reverse();
            col = int.Parse(ch);
            if (col == 0)
            {
                run = false;
                draw();
                MessageBox.Show("Программа завершена");
                return;
            }
            draw();
        }
        private void button10_Click(object sender, EventArgs e) //сохранение таблицы
        {
            SaveFileDialog save = new SaveFileDialog();
            if (save.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = save.FileName;
            // сохраняем текст в файл
            List <string> list = new List<string>();
            list.Add(textBox1.Text + " " + columnscount.ToString());
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                for (int j = 1; j < dataGridView1.Columns.Count;j++)
                {
                    if (dataGridView1[j, i].Value != null) list.Add(i.ToString() + " " + j.ToString() + " " + dataGridView1[j, i].Value.ToString());
                }
            }
            File.WriteAllText(filename, string.Join("\n", list)); //запись в файл
            MessageBox.Show("Файл сохранен");
        }

        private void button11_Click(object sender, EventArgs e) //загрузка таблицы
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = open.FileName;// получаем выбранный файл
            string[] lines= File.ReadAllLines(filename);// читаем файл в массив строк
            textBox1.Text = lines[0].Split()[0]; //заполняем алфавит
            changeTable(); //чтобы появилось нужное кол-во строк для исключения ошибок индексации при заполнении таблицы из файла
            for (int i = 0; i < int.Parse(lines[0].Split()[1]); i++) createcolumn(); //что бы появилось нужное кол-во столбцов для исключения ошибок индексации при заполнении таблицы из файла
            for (int i = 1; i < lines.Length; i++)
            {

                string[] line = lines[i].Split();
                if (line.Length == 4)
                {
                    dataGridView1[int.Parse(line[1]), int.Parse(line[0])].Value = " " + line[3];
                }
                else dataGridView1[int.Parse(line[1]), int.Parse(line[0])].Value = line[2]; 
            }
            MessageBox.Show("Файл открыт");
        }
        private void button12_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(); //инструкция
            form3.Show();
        }
    }
}
