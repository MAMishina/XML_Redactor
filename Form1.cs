using System.Data;
namespace XML_Redactor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("��������� ��� ����.", "������.");
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }

        }

        private void saveButton_Click(object sender, EventArgs e)

        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text file  (*.xml)|*.xml|All files(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = new DataSet(); // ������� ���� ��� ������ ��� ������
                    DataTable dt = new DataTable(); // ������� ���� ��� ������ ������� ������
                    dt.TableName = "Employee"; // �������� �������
                    dt.Columns.Add("Name"); // �������� �������
                    dt.Columns.Add("Age");
                    dt.Columns.Add("Programmer");
                    ds.Tables.Add(dt); //� ds ��������� �������, � ��������� � ���������, ���������� ����

                    foreach (DataGridViewRow r in dataGridView1.Rows) // ���� � dataGridView1 ���� ������
                    {
                        DataRow row = ds.Tables["Employee"].NewRow(); // ������� ����� ������ � �������, ���������� � ds
                        row["Name"] = r.Cells[0].Value;  //� ������� ���� ������ ������� ������ �� ������� ������� dataGridView1
                        row["Age"] = r.Cells[1].Value; // �� �� ����� �� ������� ���������
                        row["Programmer"] = r.Cells[2].Value; //�� �� ����� � �������� ���������
                        ds.Tables["Employee"].Rows.Add(row); //���������� ���� ���� ������ � ������� ds.
                    }
                    ds.WriteXml(sfd.FileName);
                    
                    MessageBox.Show("XML ���� ������� ��������.", "���������.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void Load()//���� ������ ��� ������
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Filter = "Text file  (*.txt)|*.txt|All files(*.*)|*.*";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    using (StreamReader sr = new StreamReader(ofd.FileName))
                    {
                        string lineFromFile = sr.ReadLine();
                        while (lineFromFile != null)
                        {

                            string[] args = lineFromFile.Split(";");

                            int n = dataGridView1.Rows.Add();
                            dataGridView1.Rows[n].Cells[0].Value = args[0];
                            dataGridView1.Rows[n].Cells[1].Value = args[1];
                            dataGridView1.Rows[n].Cells[2].Value = args[2];
                            lineFromFile = sr.ReadLine();
                            MessageBox.Show(n.ToString());
                            // dataGridView1.Rows[n].Cells[3].Value = args[3];
                        }
                    }
                    MessageBox.Show("ok");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        private void Save()//���� ������ ��� ������
        {


            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Text file  (*.txt)|*.txt|All files(*.*)|*.*";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string forWrite = "";
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.Default))
                    {

                        foreach (DataGridViewRow dgv in dataGridView1.Rows)
                        {
                            forWrite += dgv.Cells[0].Value.ToString() + ";" + dgv.Cells[1].Value.ToString() + ";" +
                                dgv.Cells[2].Value.ToString() + "\n";
                            //dataGridView1.Rows[n].Cells[3].Value = forWrite;
                        }
                        MessageBox.Show("File was saved");
                        sw.WriteLine(forWrite);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }








        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0) //���� � ������� ������ ���� �����
                {
                    MessageBox.Show("�������� ���� ����� ��������� ������ �����.", "������.");
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Text file  (*.xml)|*.xml|All files(*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.Cancel)
                        return;

                    DataSet ds = new DataSet(); // ������� ����� ������ ��� ������
                    ds.ReadXml(ofd.FileName); // ���������� � ���� XML-������ �� �����

                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // ��������� ����� ����� � dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // ������� � ������ ������� ��������� ������ ������ �� ������� ������� ������� ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Age"]; // �� �� ����� �� ������ ��������
                        dataGridView1.Rows[n].Cells[2].Value = item["Programmer"]; // �� �� ����� � ������� ��������
                    }
                }
                    
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            int n = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value);
            numericUpDown1.Value = n;
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();

        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }
            else
            {
                MessageBox.Show("�������� ������ ��� ��������������.", "������.");
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count > 0)
            {
                dataGridView1.Rows.Clear();               
            }
            else
            {
                MessageBox.Show("������� ������.", "������.");
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //��������
            }
            else
            {
                MessageBox.Show("�������� ������ ��� ��������.", "������.");
            }
        }
    }
}