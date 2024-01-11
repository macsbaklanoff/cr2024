﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cr2024
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            label2.Text = "Для того, чтобы работать на эмуляторе машины Тьюринга необходимо \rзнать ряд базовых правил.\r" + 
                "1. Для появления ленты необходимо нажать кнопку, находящуюся под \rполем ввода условия задачи - Создать ленту.\r" + 
                "2. Прежде чем заполнять ленту, символы, из которых будет \rсостоять запись в ленте должны находиться в алфавите.\r" +
                "3. Чтобы вставить символ в ленту, необходимо дважды \rкликнуть по ячейке ленты \r" + 
                "4. Основной и самый главный инструмент в эмуляторе - таблица состояний. \rОна обновляется автоматически при обновлении алфавита.\r" +
                "5. Для заполнения таблицы состояний используются базовые записи.\r" +
                "5.1. Символ, на который нужно заменить символ из алфавита \r+ движение ленты влево (<) или вправо (>), \rили отсутствие движения(.)  + \rQ и номер состояния для перехода.\r" +
                "Наприер: 1>Q2 - что означает заменить символ на единицу, \rсдвинуться вправо на 1 шаг и перейти в состояние Q2.\r" +
                "Следует обращать особое внимание на правильное заполнение таблицы. \r" +
                "В обязательном порядке в команде необходимо указать один \r из трех необходимых символов: <, >, . \r" +
                "Не пытаться перейти в отсутствующее состояние, \rуделять вводу команд особое внимание.";
        }
    }
}
