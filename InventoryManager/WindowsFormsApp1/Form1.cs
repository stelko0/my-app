using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Runtime.InteropServices;



namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        private SaveFileDialog sfd;
        private OpenFileDialog ofd;

        private String filePath = "";
        private String fileContent = "";
       
        public Form1()
        {
           
            InitializeComponent();

            sfd = new SaveFileDialog();
            ofd = new OpenFileDialog();

            // Забранява на потребителя да оразмерява прозореца на програмата
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            listViews();
        }

        private void listViews()
        {
            // Списък за всеки елемент
            listView1.View = View.Details;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("Име", 150);
            listView1.Columns.Add("Количество", 150);
            listView1.Columns.Add("Дата", 150);
            dateElement.Format = DateTimePickerFormat.Custom;
            dateElement.CustomFormat = "dd-MM-yyyy";


            //Списък за всяка мерна единица
            //listView2.View = View.Details;
            //listView2.FullRowSelect = true;
            //listView2.Columns.Add("Мерна единица", 150);
            //listView2.Columns.Add("Символ", 100);

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Добавяме прозореца на About Inventory Manager
            About aboutForm = new About();

            // Отваряме прозореца на About Inventory Manager
            aboutForm.ShowDialog();



        }
        // Добавяне на елемент
        private void newItem_Click(object sender, EventArgs e)
        {

            if (nameElement.Text == "" || amountElement.Text == "")
            {
                MessageBox.Show("Всички полета са задължителни!");
                return;
            }
            else
            {
                string date = DateTime.UtcNow.ToString("dd-MM-yyyy");
                String[] row = { nameElement.Text, amountElement.Text, date  };
                ListViewItem item = new ListViewItem(row);

                listView1.Items.Add(item);
                nameElement.Text = "";
                amountElement.Text = "";
            }
        }

        // Премахване на елемент
        private void removeItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listView1.Items.Cast<ListViewItem>().Where(T => T.Selected)
                .Select(T => T.Index).ToList().ForEach(T => listView1.Items.RemoveAt(T));
                nameElement.Text = "";
                amountElement.Text = "";
            }
            else
            {
                MessageBox.Show("Първо изберете елемент!");
            }
        }

        // Намаляне на количеството
        private void decreaseBtn_Click(object sender, EventArgs e)
        {
            if (nameElement.Text != "" || amountElement.Text != "")
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    // Превръщаме string към int(текст към цяло число)
                    int amount = Int32.Parse(amountElement.Text);

                    // Проверяваме дали количеството е по-голямо от 0
                    if (amount > 0)
                    {
                        // Ако е по-голямо можем да премахваме 1 количество
                        amount--;
                        // Добавяме новото количество в текстовото поле, след като сме премахнали
                        amountElement.Text = amount.ToString();
                    }
                    else
                    {
                        // Ако е 0 правим полето 0 за да не можем да отиден в отрицателно количество
                        amountElement.Text = "0";
                    }
                }
                else
                {
                    MessageBox.Show("Първо изберете елемент!");
                }
            }
        }

        // Увеличаване на количеството
        private void increaseBtn_Click(object sender, EventArgs e)
        {
            if (nameElement.Text != "" || amountElement.Text != "")
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    int amount = Int32.Parse(amountElement.Text);
                    // Добавяме 1 количество
                    amount++;
                    // Добавяме новото количество в текстовото поле, след като сме добавили
                    amountElement.Text = amount.ToString();
                }
                else
                {
                    MessageBox.Show("Първо изберете елемент!");
                }
            }
        }

        private void amountElement_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Бутона, който натискаме в полето за количество
            char ch = e.KeyChar;
            // Правим проверка дали това, което натискаме е различно от число или backspace 
            if (!Char.IsDigit(ch) && ch != 8)
            {
                e.Handled = true;
            }
        }

        // Бутон от горното меню за затваряне на програмата
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Сигурни ли сте, че искате да затворите програмата? Това ще изтрие всичко, ако не сте го запазили!","ВНИМАНИЕ!!", MessageBoxButtons.YesNo);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        // Избиране на елемент, при което се въвеждат автоматично данните в текстовите полета ако потребителя 
        // желае да редактира дадения елемент
        private void listView1_DoubleClick(object sender, EventArgs e)
        {

            String name = listView1.SelectedItems[0].SubItems[0].Text;
            String amount = listView1.SelectedItems[0].SubItems[1].Text;
            String date = listView1.SelectedItems[0].SubItems[2].Text;


            nameElement.Text = name;
            amountElement.Text = amount;
            dateElement.Text = date;
        }


        // Актулизиране на елемент
        private void updateBtn_Click(object sender, EventArgs e)
        {
            if (amountElement.Text != "")
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    ListViewItem item = listView1.SelectedItems[0];
                    item.SubItems[0].Text = nameElement.Text;
                    item.SubItems[1].Text = amountElement.Text;
                    item.SubItems[2].Text = dateElement.Text;



                    nameElement.Text = "";
                    amountElement.Text = "";
                    dateElement.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Първо изберете елемент!");

            }
        }

        
        // Премахване на мерна единица
       

        // Запазване
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

            if (File.Exists(filePath) == true)
            {

            string jsonData;

            var elementsList = new List<Elements>();

            foreach (ListViewItem itemRow in listView1.Items)
            {
                elementsList.Add(new Elements()
                {
                    Name = itemRow.SubItems[0].Text,
                    Amount = itemRow.SubItems[1].Text,
                    Date = itemRow.SubItems[2].Text,
                });
            }
       
                jsonData = JsonConvert.SerializeObject(elementsList, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                MessageBox.Show("Файлът е запазен!");
            }
            } catch
            {
                MessageBox.Show("Файлът не е запазен!");
            }
        }

        // Запазване като
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            // Задаваме локация къде да ни пусне (Този компютър / This PC)
            sfd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            sfd.Filter = "Json files (*.json) | *.json";
            sfd.FilterIndex = 2;
            sfd.Title = "Save elements";

            sfd.ShowDialog();

            if (sfd.FileName != "")
            {
                var jsonData = string.Empty;
                var elementsList = new List<Elements>();
                foreach (ListViewItem itemRow in listView1.Items)
                {
                    elementsList.Add(new Elements()
                    {
                        Name = itemRow.SubItems[0].Text,
                        Amount = itemRow.SubItems[1].Text,
                        Date = itemRow.SubItems[2].Text,
                    });


                    jsonData = JsonConvert.SerializeObject(elementsList, Formatting.Indented);
                    File.WriteAllText(sfd.FileName, jsonData);
                }
                    MessageBox.Show("Вашият файл е запазен!");

                }
            }
            catch
            {
                MessageBox.Show("Вашият файл не е запазен!");
            }
        }

        // Изчистване
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to exit off the application", "Are you sure?", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes) //Creates the yes function
            {
                listView1.Clear();
                listViews();
                filePath = "";

            }
            
        }

            
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ofd.InitialDirectory = "::{20D04FE0-3AEA-1069-A2D8-08002B30309D}";
            ofd.Filter = "Json files (*.json) | *.json";
            ofd.FilterIndex = 2;
            ofd.Title = "Open elements";
            ofd.ShowDialog();
            filePath = ofd.FileName;
            var fileStream = ofd.OpenFile();

            using (StreamReader reader = new StreamReader(fileStream))
            {
                fileContent = reader.ReadToEnd();
                List<Elements> items = JsonConvert.DeserializeObject<List<Elements>>(fileContent);
                foreach (var element in items)
                {
                    string[] row = { element.Name, element.Amount, element.Date};
                    var listViewItem = new ListViewItem(row);
                    listView1.Items.Add(listViewItem);
                }
            }
        }
    }
}
