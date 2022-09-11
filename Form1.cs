using System.Data;
namespace XML_Redactor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// êíîïêà "äîáàâèòü"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")   
            {
                MessageBox.Show("Çàïîëíèòå âñå ïîëÿ.", "Îøèáêà."); 
            }
            else
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = textBox1.Text;
                dataGridView1.Rows[n].Cells[1].Value = numericUpDown1.Value;
                dataGridView1.Rows[n].Cells[2].Value = comboBox1.Text;
            }
            ///fghfhjdfjgdjgdjgfdjgd

        }

        /// <summary>
        /// êíîïêà "ñîõðàíèòü"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveButton_Click(object sender, EventArgs e)

        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog(); // ñîçäàåì äèàëîã ñîõðàíåíèÿ ôàéëà
                sfd.Filter = "Text file  (*.xml)|*.xml|All files(*.*)|*.*"; // óñòàíàâëèâàåì ôèëüòð äîïóñòèìûõ çíà÷åíèé
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    DataSet ds = new DataSet(); // ñîçäàåì ïîêà ÷òî ïóñòîé êýø äàííûõ
                    DataTable dt = new DataTable(); // ñîçäàåì ïîêà ÷òî ïóñòóþ òàáëèöó äàííûõ
                    dt.TableName = "Employee"; // íàçâàíèå òàáëèöû
                    dt.Columns.Add("Name"); // íàçâàíèå êîëîíîê
                    dt.Columns.Add("Age");
                    dt.Columns.Add("Programmer");
                    ds.Tables.Add(dt); //â ds ñîçäàåòñÿ òàáëèöà, ñ íàçâàíèåì è êîëîíêàìè, ñîçäàííûìè âûøå

                    foreach (DataGridViewRow r in dataGridView1.Rows) // ïîêà â dataGridView1 åñòü ñòðîêè
                    {
                        DataRow row = ds.Tables["Employee"].NewRow(); // ñîçäàåì íîâóþ ñòðîêó â òàáëèöå, çàíåñåííîé â ds
                        row["Name"] = r.Cells[0].Value;  //â ñòîëáåö ýòîé ñòðîêè çàíîñèì äàííûå èç ïåðâîãî ñòîëáöà dataGridView1
                        row["Age"] = r.Cells[1].Value; // òî æå ñàìîå ñî âòîðûìè ñòîëáöàìè
                        row["Programmer"] = r.Cells[2].Value; //òî æå ñàìîå ñ òðåòüèìè ñòîëáöàìè
                        ds.Tables["Employee"].Rows.Add(row); //äîáàâëåíèå âñåé ýòîé ñòðîêè â òàáëèöó ds.
                    }
                    ds.WriteXml(sfd.FileName);
                    
                    MessageBox.Show("XML ôàéë óñïåøíî ñîõðàíåí.", "Âûïîëíåíî.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// êíîïêà "çàãðóçèòü"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0) //åñëè â òàáëèöå áîëüøå íóëÿ ñòðîê
                {
                    MessageBox.Show("Î÷èñòèòå ïîëå ïåðåä çàãðóçêîé íîâîãî ôàéëà.", "Îøèáêà.");
                }
                else
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.Filter = "Text file  (*.xml)|*.xml|All files(*.*)|*.*";
                    if (ofd.ShowDialog() == DialogResult.Cancel)
                        return;

                    DataSet ds = new DataSet(); // ñîçäàåì íîâûé ïóñòîé êýø äàííûõ
                    ds.ReadXml(ofd.FileName); // çàïèñûâàåì â íåãî XML-äàííûå èç ôàéëà

                    foreach (DataRow item in ds.Tables["Employee"].Rows)
                    {
                        int n = dataGridView1.Rows.Add(); // äîáàâëÿåì íîâóþ ñðîêó â dataGridView1
                        dataGridView1.Rows[n].Cells[0].Value = item["Name"]; // çàíîñèì â ïåðâûé ñòîëáåö ñîçäàííîé ñòðîêè äàííûå èç ïåðâîãî ñòîëáöà òàáëèöû ds.
                        dataGridView1.Rows[n].Cells[1].Value = item["Age"]; // òî æå ñàìîå ñî âòîðûì ñòîëáöîì
                        dataGridView1.Rows[n].Cells[2].Value = item["Programmer"]; // òî æå ñàìîå ñ òðåòüèì ñòîëáöîì
                    }
                }
                    
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }

        /// <summary>
        /// äåéñòâèå ïðè âûáîðå ñòðîêè â dataGridView1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            numericUpDown1.Value = (decimal)dataGridView1.SelectedRows[0].Cells[1].Value;
            comboBox1.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
        }

        /// <summary>
        /// êíîïêà "ðåäàêòèðîâàòü"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                MessageBox.Show("Âûáåðèòå ñòðîêó äëÿ ðåäàêòèðîâàíèÿ.", "Îøèáêà.");
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
                MessageBox.Show("Òàáëèöà ïóñòàÿ.", "Îøèáêà.");
            }
        }

        private void delButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int n = dataGridView1.SelectedRows[0].Index;
                dataGridView1.Rows.RemoveAt(n); //óäàëåíèå
            }
            else
            {
                MessageBox.Show("Âûáåðèòå ñòðîêó äëÿ óäàëåíèÿ.", "Îøèáêà.");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
