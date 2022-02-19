using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LabNo2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Guardar(string fileName, string texto)
        {
            //Abrir el archivo: Write sobreescribe el archivo, Append agrega los datos al final del archivo
            FileStream stream = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            //Crear un objeto para escribir el archivo
            StreamWriter writer = new StreamWriter(stream);
            //Usar el objeto para escribir al archivo, WriteLine, escribe linea por linea
            //Write escribe todo en la misma linea. En este ejemplo se hará un dato por cada línea
            writer.WriteLine(texto);
            //Cerrar el archivo
            writer.Close();
        }

        //botón "Ir"
        private void buttonIr_Click(object sender, EventArgs e)
        {
            String link = "";

            if(comboBox1.Text != null)
            {
                link = comboBox1.Text;
            }
            else if(comboBox1.SelectedItem != null)
            {
                link = comboBox1.SelectedItem.ToString();
            }

            if(!link.Contains("."))
            {
                link = "https://www.google.com/search?q=" + link;
            }
            if(!link.Contains("https://"))
            {
                link = "https://" + link;
            }

            webBrowser1.Navigate(new Uri(link));

            comboBox1.Items.Add(link);//agrega datos al combobox
            Guardar("Historial de Navegación.txt", link);
        }

        //botones de "Navegar"

        //inicio
        private void inicioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoHome();
        }

        //ir átras
        private void irAtrásToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        //ir adelante
        private void irAdelanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        //salir
        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Leer(String fileName)
        {
            //Abrimos el archivo, en este caso lo abrimos para lectura
            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            //Un ciclo para leer el archivo hasta el final del archivo
            //Lo leído se va guardando en un control richTextBox
            while (reader.Peek() > -1)
            //Esta linea envía el texto leído a un control richTextBox, se puede cambiar para que
            //lo muestre en otro control por ejemplo un combobox
            {
                //richTextBox1.AppendText(reader.ReadLine());
                comboBox1.Items.Add(reader.ReadLine());
            }
            //Cerrar el archivo, esta linea es importante porque sino despues de correr varias veces el programa daría error de que el archivo quedó abierto muchas veces. Entonces es necesario cerrarlo despues de terminar de leerlo.
            reader.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //comboBox1.SelectedIndex = 0;
            //webBrowser1.GoHome();
            Leer("Historial de Navegación.txt");

        }
    }
}
