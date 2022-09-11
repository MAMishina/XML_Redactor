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
                MessageBox.Show("Заполните все поля.", "Ошибка.");
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
                    DataSet ds = new DataSet(); // создаем пока что пустой кэш данных
                    DataTable dt = new DataTable(); // создаем пока что пустую таблицу данных
                    dt.TableName = "Employee"; // название таблицы
                    dt.Columns.Add("Name"); // название колонок
                    dt.Columns.Add("Age");
                    dt.Columns.Add("Programmer");
                    ds.Tables.Add(dt); //в ds создается таблица, с названием и колонками, созданными выше

                    foreach (DataGridViewRow r in dataGridView1.Rows) // пока в dataGridView1 есть строки
                    {
                        DataRow row = ds.Tables["Employee"].NewRow(); // создаем новую строку в таблице, занесенной в ds
                        row["Name"] = r.Cells[0].Value;  //в столбец этой строки заносим данные из первого столбца dataGridView1
                        row["Age"] = r.Cells[1].Value; // то же самое со вторыми столбцами
                        row["Programmer"] = r.Cells[2].Value; //то же самое с третьими столбцами
                        ds.Tables["Employee"].Rows.Add(row); //добавление всей этой строки в таблицу ds.
                    }
                    ds.WriteXml(sfd.FileName);
                    
                    MessageBox.Show("XML файл успешно сохранен.", "Выполнено.");
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


        private void Load()//тест метода для Артема
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


        private void Save()//тест метода для Артема
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
                if (dataGridView1.Rows.Count > 0) //если в таблице больше нуля строк
                {
                    MessageBox.Show("Очистите поле перед загрузкой нового файла.", "Ошибка.");
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Text file  (*.xml)|*.xml|All files(*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.Cancel)
                        return;

                    DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                    ds.ReadXml(ofd.FileName); // записываем в него XML-данные из файла

                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // добавляем новую сроку в dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // заносим в первый столбец созданной строки данные из первого столбца таблицы ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Age"]; // то же самое со вторым столбцом
                        dataGridView1.Rows[n].Cells[2].Value = item["Programmer"]; // то же самое с третьим столбцом
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
                MessageBox.Show("Выберите строку для редактирования.", "Ошибка.");
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
                MessageBox.Show("Таблица пустая.", "Ошибка.");
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index); //удаление
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.", "Ошибка.");
            }
        }
    }
}