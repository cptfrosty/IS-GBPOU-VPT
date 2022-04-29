using System;
using ComponentFactory.Krypton.Toolkit;


namespace YourHouse
{
    public partial class MainForm : KryptonForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            
        }

        private void Button_Corps_Click(object sender, EventArgs e)
        {
            string nameBtn = "Corps";
            SelectActionForm selectActionForm = new SelectActionForm(nameBtn);
            selectActionForm.ShowDialog();
        }

        private void Button_Museum_Click(object sender, EventArgs e)
        {
            string nameBtn = "Museum";
            SelectActionForm selectActionForm = new SelectActionForm(nameBtn);
            selectActionForm.ShowDialog();
        }

        private void Button_Specialities_Click(object sender, EventArgs e)
        {
            string nameBtn = "Specialties";
            SelectActionForm selectActionForm = new SelectActionForm(nameBtn);
            selectActionForm.ShowDialog();
        }
       
        private void Button_Workshops_Click(object sender, EventArgs e)
        {
            string nameBtn = "Workshops";
            SelectActionForm selectActionForm = new SelectActionForm(nameBtn);
            selectActionForm.ShowDialog();
        }

    }
}
