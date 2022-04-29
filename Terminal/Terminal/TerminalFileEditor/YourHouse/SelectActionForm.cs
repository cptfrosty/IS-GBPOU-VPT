﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;

namespace YourHouse
{
    public partial class SelectActionForm : KryptonForm
    {
        private string nameBtn;
        public SelectActionForm(string nameBtn)
        {
            InitializeComponent();
            this.nameBtn = nameBtn;
        }

        private void SelectActionForm_Load(object sender, EventArgs e)
        {
            nameFile.Text = nameBtn + ".xml";
        }

        private void Button_Add_Click(object sender, EventArgs e)
        {
            if(nameBtn == "Corps")
            {
                AddCorpsForm addCorpsForm = new AddCorpsForm(nameBtn);
                addCorpsForm.ShowDialog();
            }
        }

        private void Button_Remove_Click(object sender, EventArgs e)
        {

        }

        private void Button_Change_Click(object sender, EventArgs e)
        {

        }

        private void Button_Open_Click(object sender, EventArgs e)
        {

        }
    }
}
