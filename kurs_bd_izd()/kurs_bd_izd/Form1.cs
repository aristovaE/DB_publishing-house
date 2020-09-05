using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace kurs_bd_izd
{
    public partial class Form1 : Form
    {    
        string s;   //название выводимого поля
        string connectStr = @"Data Source=(LocalDB)\v11.0;AttachDbFilename=F:\3 курс\Курсовая_БД_ИЗДАТЕЛЬСТВА(c#)\kurs_bd_izd()\kurs_bd_izd\bd_izd.mdf;Integrated Security=True;Connect Timeout=30";
        
        //string queryT_Izdanie = "SELECT * FROM Издание ORDER BY Id";
        string queryT_Proizvedenie = "SELECT * FROM Произведение ORDER BY Id";
        string queryT_Avtor = "SELECT * FROM Автор ORDER BY Id";
        string queryT_Redactor = "SELECT * FROM Редактор ORDER BY Id";
        string queryT_Hudozhnik = "SELECT * FROM Художник ORDER BY Id";
        string queryT_Tipografiya = "SELECT * FROM Типография ORDER BY Id";

        public Form1()
        {
            InitializeComponent();
            
        }
        private void UpdData(string table, string pole,DataGridView dgv,TextBox txtb)
        // метод изменения записей любой таблицы
        // q - запрос на получение данных из БД  
        {
            SqlConnection connection = new SqlConnection(connectStr);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "UPDATE " + table + " SET " + pole + " = @" + pole + "  WHERE Id = @Id";
            command.Parameters.AddWithValue("@Id", (int)dgv[0, dgv.CurrentRow.Index].Value); //добавляем значение параметра
            command.Parameters.AddWithValue("@" + pole + "", txtb.Text); //добавляем значение параметра

            try
            {
                command.ExecuteNonQuery(); //выполняем запрос
                MessageBox.Show("Запись обновлена!");
            }
            catch
            {
                MessageBox.Show("Обновить не удалось!");
            }
            connection.Close();
        }
        private void LoadData(string q, DataGridView dgv)
        // метод загрузки данных в таблицу Издание
        // q - запрос на получение данных из БД  
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(connectStr);
                myConnection.Open();    //Открываем соединение
                SqlCommand cmd = new SqlCommand(q, myConnection);
                // создание SQL команды с запросом
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // выполнение команды
                DataTable tb = new DataTable(); // создание таблицы
                da.Fill(tb);    // загрузка данных в таблицу
                dgv.DataSource = tb;  // привязка полученной таблицы к компоненту

                this.ActiveControl = dgv;   // активация компонента таблица
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.Columns[0].Visible = false; //??
                dgv.Columns[1].Visible = false; //??
                dgv.Columns[3].Visible = false; //??
                dgv.Columns[4].Visible = false; //??
                dgv.Columns[7].Visible = false; //??
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
                myConnection.Close(); // разрываем соединение с БД
            }
            catch
            {
                MessageBox.Show("Данные не найдены!");
            }
            

        }
        private void LoadDataN(string q, DataGridView dgv)
        // метод загрузки данных в таблицу с одним полем
        // q - запрос на получение данных из БД  
        {

            SqlConnection myConnection = new SqlConnection(connectStr);
            myConnection.Open();    //Открываем соединение
            SqlCommand cmd = new SqlCommand(q, myConnection);
            // создание SQL команды с запросом
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // выполнение команды
            DataTable tb = new DataTable(); // создание таблицы
            da.Fill(tb);    // загрузка данных в таблицу
            dgv.DataSource = tb;  // привязка полученной таблицы к компоненту

            this.ActiveControl = dgv;   // активация компонента таблица
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Columns[0].Visible = false; //??
            
            if (dgv.Rows.Count > 0)
            {
                dgv.Rows[0].Selected = true;
            }
            myConnection.Close(); // разрываем соединение с БД



        }

        private void LoadDataProizv(string q, DataGridView dgv)
        // метод загрузки данных в таблицу Произведение
        // q - запрос на получение данных из БД  
        {
            try
            {
                SqlConnection myConnection = new SqlConnection(connectStr);
                myConnection.Open();    //Открываем соединение
                SqlCommand cmd = new SqlCommand(q, myConnection);
                // создание SQL команды с запросом
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // выполнение команды
                DataTable tb = new DataTable(); // создание таблицы
                da.Fill(tb);    // загрузка данных в таблицу
                dgv.DataSource = tb;  // привязка полученной таблицы к компоненту

                this.ActiveControl = dgv;   // активация компонента таблица
                dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgv.Columns[0].Visible = false; //??
                dgv.Columns[2].Visible = false; //??
                if (dgv.Rows.Count > 0)
                {
                    dgv.Rows[0].Selected = true;
                }
                myConnection.Close(); // разрываем соединение с БД

            }
            catch
            {
                MessageBox.Show("Данные не найдены!");
            }

        }
       

        private void DelData(string table, DataGridView dgv) //Удаление записи из любой таблицы по клику в дгв
        
        {
            SqlConnection connection = new SqlConnection(connectStr);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandText = "DELETE FROM "+table+" WHERE (Id = @Id)";

            command.Parameters.AddWithValue("@Id", (int)dgv[0, dgv.CurrentRow.Index].Value); //добавляем значение параметра

            try
            {
                command.ExecuteNonQuery(); //выполняем запрос
                MessageBox.Show("Запись удалена");
            }
            catch
            {
                MessageBox.Show("Удалить не удалось!");
            }
            connection.Close();
        }

        private void LoadDataToCombobox(string q, ComboBox cmb,string pole)
        // метод загрузки данных в падающий список с любым запросом
        // q - запрос на получение данных из БД
        {
           
                SqlConnection myConnection = new SqlConnection(connectStr);
                // создание соединения с БД
                myConnection.Open();    // открываем соединение
                SqlCommand cmd = new SqlCommand(q, myConnection);
                // создание SQL команды с запросом
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                // выполнение команды
                DataTable tb = new DataTable(); // создание таблицы
                da.Fill(tb);        // загрузка данных в таблицу
                cmb.DataSource = tb;
                // привязка полученной таблицы к компоненту comboBox1
                cmb.DisplayMember = pole;
                cmb.ValueMember = "Id";   // фактические значения
                cmb.SelectedIndex = -1;
                myConnection.Close(); // разрываем соединение с БД
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            //Издание
            
            LoadDataToCombobox(queryT_Proizvedenie, comboBox1, "Название_произведения");
            LoadDataToCombobox(queryT_Redactor, comboBox2, "ФИО");
            LoadDataToCombobox(queryT_Hudozhnik, comboBox3, "ФИО");
            LoadDataToCombobox(queryT_Tipografiya, comboBox6, "Название");
            LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id

            
            //Автор
            LoadDataN(queryT_Avtor, dataGridView3);

            //Поиск
            LoadDataToCombobox(queryT_Avtor, comboBox5, "ФИО");
            LoadDataToCombobox(queryT_Redactor, comboBox7, "ФИО");
            LoadDataToCombobox(queryT_Hudozhnik, comboBox8, "ФИО");
            LoadDataToCombobox(queryT_Tipografiya, comboBox9, "Название");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e) //Переключение вкладок 1
        {
            
            if (tabControl1.SelectedIndex == 1)
            {
                //Произведение
                
                LoadDataToCombobox(queryT_Avtor,comboBox4 ,"ФИО");

                LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id" , dataGridView2);

            }
            if (tabControl1.SelectedIndex == 0)
            {
                
                //Издание
                
                LoadDataToCombobox(queryT_Proizvedenie, comboBox1, "Название");
                LoadDataToCombobox(queryT_Redactor, comboBox2, "ФИО");
                LoadDataToCombobox(queryT_Hudozhnik, comboBox3, "ФИО");
                LoadDataToCombobox(queryT_Tipografiya, comboBox6, "Название");
                
                LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id
            }
        }
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e) //Переключение вкладок 2
        {
            
            if (tabControl2.SelectedIndex == 1)
            {
                //Художник
                LoadDataN(queryT_Hudozhnik, dataGridView4);
            }
            if (tabControl2.SelectedIndex == 2)
            {
                //Редактор
                LoadDataN(queryT_Redactor, dataGridView5);
            }
            if (tabControl2.SelectedIndex == 3)
            {
                //Типография
                LoadDataN(queryT_Tipografiya, dataGridView6);
            }
            if (tabControl2.SelectedIndex == 0)
            {
                //Автор
                LoadDataN(queryT_Avtor, dataGridView3);
            }
        }

        
        
        
        private void button1_Click(object sender, EventArgs e) // Добавить Издание
        {
            if (textBox1.Text == "" || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox1.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                MessageBox.Show("Все поля должны быть заполнены!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Издание (Индекс_произведения,Тираж,Индекс_редактора,Индекс_художника,Дата_подписки_в_печать,Дата_выхода_из_печати,Индекс_типографии) VALUES(@Индекс_произведения,@Тираж,@Индекс_редактора,@Индекс_художника,@Дата_подписки_в_печать,@Дата_выхода_из_печати,@Индекс_типографии)";

                command.Parameters.AddWithValue("@Индекс_произведения", comboBox1.SelectedValue);
                command.Parameters.AddWithValue("@Тираж", Convert.ToInt32(textBox1.Text));
                command.Parameters.AddWithValue("@Индекс_редактора", comboBox2.SelectedValue);
                command.Parameters.AddWithValue("@Индекс_художника", comboBox3.SelectedValue);
                command.Parameters.AddWithValue("@Дата_подписки_в_печать", dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Дата_выхода_из_печати", dateTimePicker4.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Индекс_типографии", comboBox6.SelectedValue);
                command.ExecuteScalar();
                connection.Close();
                MessageBox.Show("Запись успешно добавлена!");
                LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id

                // установить курсор в поле textBox1
                textBox1.Focus();
            }
            
        }

        
        private void button13_Click(object sender, EventArgs e) //Добавить Автор
        {
            if (textBox5.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Автор (ФИО) VALUES(@ФИО)";
                
                command.Parameters.AddWithValue("@ФИО", textBox5.Text);

                command.ExecuteScalar();
                connection.Close();
                MessageBox.Show("Запись успешно добавлена!");
                LoadDataN(queryT_Avtor, dataGridView3);

                // установить курсор в поле textBox1
                textBox5.Clear();
                textBox5.Focus();
            }
        }

        private void button9_Click(object sender, EventArgs e) //Добавить Произведение
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox4.SelectedIndex == -1)
                MessageBox.Show("Все поля должны быть заполнены!");
            else
            {
                try
                {
                    SqlConnection connection = new SqlConnection(connectStr);
                    connection.Open();
                    SqlCommand command = connection.CreateCommand();
                    command.CommandText = "INSERT INTO Произведение (Название_произведения,Индекс_автора,Жанр,Число_страниц) VALUES(@Название_произведения,@Индекс_автора,@Жанр,@Число_страниц)";

                    command.Parameters.AddWithValue("@Название_произведения", textBox2.Text);
                    command.Parameters.AddWithValue("@Индекс_автора", comboBox4.SelectedValue); //замена ФИО на его индекс
                    command.Parameters.AddWithValue("@Жанр", textBox3.Text);
                    command.Parameters.AddWithValue("@Число_страниц", Convert.ToInt32(textBox4.Text));

                    command.ExecuteScalar();
                    connection.Close();
                    MessageBox.Show("Запись успешно добавлена!");
                    LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id", dataGridView2);


                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();

                    textBox2.Focus();
                }
                catch
                {
                    MessageBox.Show("!!!");
                }
            }
        }

        

        private void button17_Click(object sender, EventArgs e) //Добавить Художник
        {
            if (textBox6.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Художник (ФИО) VALUES(@ФИО)";
                command.Parameters.AddWithValue("@ФИО", textBox6.Text);

                command.ExecuteScalar();
                connection.Close();
               
                MessageBox.Show("Запись успешно добавлена!");
                LoadDataN(queryT_Hudozhnik, dataGridView4);

                textBox6.Clear();
                // установить курсор в поле textBox1
                textBox6.Focus();
            }
        }

        private void button21_Click(object sender, EventArgs e) //Добавить Редактор
        {
            if (textBox7.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Редактор (ФИО) VALUES(@ФИО)";
                command.Parameters.AddWithValue("@ФИО", textBox7.Text);

                command.ExecuteScalar();
                connection.Close();
                MessageBox.Show("Запись успешно добавлена!");
                LoadDataN(queryT_Redactor, dataGridView5);

                textBox7.Clear();
                // установить курсор в поле textBox1
                textBox7.Focus();
            }  
        }

        private void button25_Click(object sender, EventArgs e) //Добавить Типография
        {
            if (textBox8.Text == "")
                MessageBox.Show("Поле НАЗВАНИЕ должно быть заполнено!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO Типография (Название) VALUES(@Название)";

                command.Parameters.AddWithValue("@Название", textBox8.Text);

                command.ExecuteScalar();
                connection.Close();
                MessageBox.Show("Запись успешно добавлена!");
                LoadDataN(queryT_Tipografiya, dataGridView6);

                textBox8.Clear();
                // установить курсор в поле textBox1
                textBox8.Focus();
            }
        }

        
        private void button5_Click(object sender, EventArgs e) //Поиск
        {
            int i = 0,k=0;
            string query="";
           //select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id

            if (comboBox5.Text != "")
            {
                LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id WHERE(Индекс_автора LIKE N'%" + comboBox5.SelectedValue + "%')",dataGridView7);
                i ++;
                comboBox5.SelectedIndex = -1;
                
            }
            if (comboBox7.Text != "")
            {
                LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id WHERE(Индекс_редактора LIKE N'%" + comboBox7.SelectedValue + "%')",dataGridView7);
                i++;
                comboBox7.SelectedIndex = -1;
            }
            if (comboBox8.Text != "")
            {
                 LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id WHERE(Индекс_художника LIKE N'%" + comboBox8.SelectedValue + "%')",dataGridView7);
                i++;
                comboBox8.SelectedIndex = -1;
            }
            if (comboBox9.Text != "")
            {
                LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id WHERE(Индекс_типографии LIKE N'%" + comboBox9.SelectedValue + "%')", dataGridView7); ;
                i++;
                comboBox9.SelectedIndex = -1;
            }
            if (textBox9.Text != "")
            {
                LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id WHERE(Название_произведения LIKE N'%" + textBox9.Text + "%')", dataGridView7);
                i++;
                textBox9.Text = "";
            }
            if (textBox10.Text != "")
            {
                LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id WHERE(Жанр LIKE N'%" + textBox10.Text + "%')", dataGridView7);
                i++;
                textBox10.Text = "";
            }


            if (i == 0)
                MessageBox.Show("Произведений по данному запросу не обнаружено!");
            
        }

        private void button19_Click(object sender, EventArgs e) //Удалить Редактор
        {
            DelData("Редактор", dataGridView5);
            LoadDataN(queryT_Redactor, dataGridView5);
        }
        
        private void button11_Click(object sender, EventArgs e) //Удалить Автор
        {
            DelData("Автор", dataGridView3);
            LoadDataN(queryT_Avtor, dataGridView3);
                      
        }

       
        private void button23_Click(object sender, EventArgs e) //Удалить Типография
        {
            DelData("Типография", dataGridView6);
            LoadDataN(queryT_Tipografiya, dataGridView6);
        }

        private void button12_Click(object sender, EventArgs e) //Изменить Автор
        {
            if (textBox5.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                UpdData("Автор", "ФИО", dataGridView3, textBox5);
                LoadDataN(queryT_Avtor, dataGridView3);
            }
                 
        }

        private void button15_Click(object sender, EventArgs e) //Удалить Художник
        {
            DelData("Художник", dataGridView4);
            LoadDataN(queryT_Hudozhnik, dataGridView4);
        }

        private void button16_Click(object sender, EventArgs e) //Изменить Художник
        {
            if (textBox6.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                UpdData("Художник", "ФИО", dataGridView4, textBox6);
                LoadDataN(queryT_Hudozhnik, dataGridView4);
            }
        }

        private void button20_Click(object sender, EventArgs e) //Изменить Редактор
        {
            if (textBox7.Text == "")
                MessageBox.Show("Поле ФИО должно быть заполнено!");
            else
            {
                UpdData("Редактор", "ФИО", dataGridView5, textBox7);
                LoadDataN(queryT_Redactor, dataGridView5);
            }
        }

        private void button24_Click(object sender, EventArgs e) //Изменить Типография
        {
            if (textBox8.Text == "")
                MessageBox.Show("Поле НАЗВАНИЕ должно быть заполнено!");
            else
            {
                UpdData("Типография", "Название", dataGridView6, textBox8);
                LoadDataN(queryT_Tipografiya, dataGridView6);
            }
        }

        private void button3_Click(object sender, EventArgs e) //Удалить Издание
        {
            DelData("Издание", dataGridView1);
            LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id

        }

        private void button2_Click(object sender, EventArgs e) //Изменить Издание
        {
            if (textBox1.Text == "" || comboBox2.SelectedIndex == -1 || comboBox3.SelectedIndex == -1 || comboBox1.SelectedIndex == -1 || comboBox6.SelectedIndex == -1)
                MessageBox.Show("Для обновления все поля должны быть заполнены!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Издание SET Индекс_произведения = @Индекс_произведения, Тираж = @Тираж, Индекс_редактора = @Индекс_редактора, Индекс_художника = @Индекс_художника, Дата_подписки_в_печать = @Дата_подписки_в_печать, Дата_выхода_из_печати = @Дата_выхода_из_печати, Индекс_типографии = @Индекс_типографии  WHERE Id = @Id";
                command.Parameters.AddWithValue("@Id", (int)dataGridView1[0, dataGridView1.CurrentRow.Index].Value); //добавляем значение параметра
                command.Parameters.AddWithValue("@Индекс_произведения", comboBox1.SelectedValue);
                command.Parameters.AddWithValue("@Тираж", Convert.ToInt32(textBox1.Text));
                command.Parameters.AddWithValue("@Индекс_редактора", comboBox2.SelectedValue);
                command.Parameters.AddWithValue("@Индекс_художника", comboBox3.SelectedValue);
                command.Parameters.AddWithValue("@Дата_подписки_в_печать", dateTimePicker3.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Дата_выхода_из_печати",dateTimePicker4.Value.ToString("yyyy-MM-dd"));
                command.Parameters.AddWithValue("@Индекс_типографии", comboBox6.SelectedValue);
                try
                {
                    command.ExecuteNonQuery(); //выполняем запрос
                    MessageBox.Show("Запись обновлена!");
                }
                catch
                {
                    MessageBox.Show("Обновить не удалось!");
                }
                connection.Close();
                LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id", dataGridView1);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id

            }
        }

        private void button8_Click(object sender, EventArgs e) //Изменить Произведение!
        {
            if (textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox4.SelectedIndex == -1)
                MessageBox.Show("Все поля должны быть заполнены!");
            else
            {
                SqlConnection connection = new SqlConnection(connectStr);
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                command.CommandText = "UPDATE Произведение SET Название_произведения = @Название_произведения, Индекс_автора = @Индекс_автора, Жанр = @Жанр, Число_страниц = @Число_страниц  WHERE Id = @Id";
                command.Parameters.AddWithValue("@Название_произведения", textBox2.Text);
                command.Parameters.AddWithValue("@Индекс_автора", comboBox4.SelectedValue); //замена ФИО на его индекс
                command.Parameters.AddWithValue("@Жанр", textBox3.Text);
                command.Parameters.AddWithValue("@Число_страниц", Convert.ToInt32(textBox4.Text));

                try
                {
                    command.ExecuteNonQuery(); //выполняем запрос
                    MessageBox.Show("Запись обновлена!");
                }
                catch
                {
                    MessageBox.Show("Обновить не удалось!");
                }
                connection.Close();
                LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id", dataGridView2);
            }
        }

        private void button6_Click(object sender, EventArgs e)//Сброс44
        {
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox4.SelectedIndex = -1;

        }

        private void button4_Click(object sender, EventArgs e) //Сброс
        {
            textBox1.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox6.SelectedIndex = -1;
        }


        private void button10_Click(object sender, EventArgs e) //Сброс
        {
            textBox5.Clear();
        }

        private void button14_Click(object sender, EventArgs e) //Сброс
        {
            textBox6.Clear();
        }

        private void button18_Click(object sender, EventArgs e) //Сброс
        {
            textBox7.Clear();
        }

        private void button22_Click(object sender, EventArgs e) //Сброс
        {
            textBox8.Clear();
        }


        private void button26_Click(object sender, EventArgs e) //Поиск по дате
        {

            string poiskS,poiskDo; //Переменные типа DateTime
            //Проверка значений
            try
            {
                poiskS = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                poiskDo = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            }
            catch
            {
                MessageBox.Show("Введите корректные даты!");
                
                return;
            }
            LoadData("select Издание.*,Произведение.Название_произведения,Редактор.ФИО,Художник.ФИО,Типография.Название from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id WHERE Дата_подписки_в_печать < '" + dateTimePicker2.Value.ToString("yyyy-MM-dd") + "' AND Дата_выхода_из_печати > '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "'", dataGridView7);
                      

        }

        private void button7_Click(object sender, EventArgs e) //Удалить Произведение
        {
            DelData("Произведение", dataGridView2);
            LoadDataProizv("select Произведение.*,Автор.ФИО from Произведение left join Автор on Произведение.Индекс_автора=Автор.Id", dataGridView2);
        }

        private void button27_Click(object sender, EventArgs e) //Максимальный тираж
        {
           // LoadDataP("select Издание.Тираж,Произведение.Название_произведения from Издание left join Произведение on Издание.Индекс_произведения=Произведение.Id MAX('Тираж') AS `maximum` FROM Издание ORDER BY `maximum` DESC", dataGridView7);// left join Редактор on Издание.Индекс_редактора=Редактор.Id left join Художник on Издание.Индекс_художника=Художник.Id left join Типография on Издание.Индекс_типографии=Типография.Id
            //"SELECT * , MAX(`price`) AS `maximum` FROM `db`.`table` GROUP BY `category` ORDER BY `maximum` DESC";
        }
    }

}