namespace Evaluacija_WinForm
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog1.FileName;
                string error;
                List<Student> students = new List<Student>();
                students = XMLStudent.xmlReadStudents(filename, out error);
                try
                {
                    textBox1.Text = students.ToString();
                }
                catch (Exception ex)
                {
                    textBox1.Text = "Nazalost nisu ucitani studenti";
                }
            }
        }
    }
}